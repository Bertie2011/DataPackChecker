using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace DataPackChecker.Shared.Data.Resources {
    public class Command {
        private static readonly char[] CommandBracketOpen = new char[] { '[', '{' };
        private static readonly Dictionary<char, char> CommandBracketClose = new Dictionary<char, char> {
            { '[', ']' }, { '{', '}' }
        };
        private static readonly char[] CommandQuotes = new char[] { '"', '\'' };

        public enum Type {
            Command, Comment, Whitespace
        }

        public Type ContentType { get; }

        /// <summary>
        /// The line number of this command. The first line has line number 1.
        /// </summary>
        public int Line { get; }

        /// <summary>
        /// The raw command string.
        /// The string ends when a new command is started (e.g. after "run" in /execute) and everything else will be inside NextCommand.
        /// </summary>
        public string Raw { get; set; }

        /// <summary>
        /// The first "word" of the command.
        /// </summary>
        public string CommandKey { get; set; }

        /// <summary>
        /// All space separated arguments of the command, excluding the CommandKey
        /// </summary>
        public List<string> Arguments { get; } = new List<string>();

        /// <summary>
        /// Some commands can have another command embedded (like /execute).
        /// A second command embedded in this command will be placed in this variable.
        /// Note that the next command might also have a next command of its own,
        /// you might want to use the Flat property instead.
        /// </summary>
        public Command NextCommand { get; set; }

        /// <summary>
        /// Returns this command and all next commands (recursive).
        /// </summary>
        public List<Command> Flat {
            get {
                var current = this;
                List<Command> result = new List<Command>();
                while (current != null) {
                    result.Add(current);
                    current = current.NextCommand;
                }
                return result;
            }
        }

        public Command(int line, string data, int startIndex = 0) {
            Line = line;
            ContentType = DetermineType(data);
            if (ContentType == Type.Command) {
                ParseCommand(data, startIndex);
            } else if (ContentType == Type.Comment) {
                Raw = data.Substring(Math.Max(1, startIndex)).Trim();
            }
        }

        private Type DetermineType(string command) {
            if (string.IsNullOrWhiteSpace(command)) {
                return Type.Whitespace;
            } else if (command.StartsWith("#")) {
                return Type.Comment;
            } else {
                return Type.Command;
            }
        }

        private void ParseCommand(string data, int startIndex) {
            StringBuilder raw = new StringBuilder();
            StringBuilder arg = new StringBuilder();
            Stack<char> brackets = new Stack<char>();
            char? quote = null;
            bool escape = false;
            for (int i = startIndex; i < data.Length; i++) {
                char c = data[i];

                // Either process the created argument or record the next character.
                if (c == ' ' && brackets.Count == 0 && quote == null && !escape) {
                    bool shouldContinue = ProcessArgument(data, i + 1, arg.ToString());
                    arg.Clear();
                    if (shouldContinue) {
                        raw.Append(c);
                        continue;
                    } else break;
                } else {
                    raw.Append(c);
                    arg.Append(c);
                }

                // Change modifiers
                if (escape) {
                    escape = false;
                } else if (c == '\\') {
                    escape = true;
                } else if (quote == null && CommandBracketOpen.Any(b => b == c)) {
                    brackets.Push(c);
                } else if (quote == null && brackets.Count != 0 && c == CommandBracketClose[brackets.Peek()]) {
                    brackets.Pop();
                } else if (CommandQuotes.Any(q => q == c)) {
                    quote = quote == null ? c : (char?)null;
                }
            }

            if (arg.Length > 0) Arguments.Add(arg.ToString());
            Raw = raw.ToString();
        }

        private bool ProcessArgument(string data, int nextIndex, string argument) {
            if (CommandKey == null) {
                CommandKey = argument;
                return true;
            } else {
                Arguments.Add(argument);
                if (CommandKey == "execute" && argument == "run" && nextIndex < data.Length) {
                    NextCommand = new Command(Line, data, nextIndex);
                    return false;
                } else return true;
            }
        }

        public override string ToString() {
            return Raw;
        }
    }
}
