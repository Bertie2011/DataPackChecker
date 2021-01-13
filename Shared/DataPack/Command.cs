using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace DataPackChecker.Shared.DataPack {
    public class Command {
        private static readonly char[] COMMAND_BRACKET_OPEN = new char[] { '[', '{' };
        private static readonly Dictionary<char, char> COMMAND_BRACKET_CLOSE = new Dictionary<char, char> {
            { '[', ']' }, { '{', '}' }
        };
        private static readonly char[] COMMAND_QUOTES = new char[] { '"', '\'' };

        public enum Type {
            Command, Comment, Whitespace
        }

        public Type ContentType { get; }
        public string Raw { get; }
        public string CommandKey { get; }

        public List<string> Arguments { get; } = new List<string>();

        public Command(string data) {
            Raw = data;
            if (string.IsNullOrWhiteSpace(data)) {
                ContentType = Type.Whitespace;
            } else if (data.StartsWith("#")) {
                ContentType = Type.Comment;
            } else {
                ContentType = Type.Command;
                int split = data.IndexOf(' ');
                CommandKey = data.Substring(0, split == -1 ? data.Length : split);
                if (split == -1) return;

                StringBuilder arg = new StringBuilder();
                Stack<char> brackets = new Stack<char>();
                char? quote = null;
                bool escape = false;
                foreach (char c in data.Substring(split + 1)) {
                    if (c == ' ' && brackets.Count == 0 && quote == null && !escape) {
                        Arguments.Add(arg.ToString());
                        arg.Clear();
                        continue;
                    } else {
                        arg.Append(c);
                    }
                    
                    if (escape) {
                        escape = false;
                    } else if (c == '\\') {
                        escape = true;
                    } else if (quote == null && COMMAND_BRACKET_OPEN.Any(b => b == c)) {
                        brackets.Push(c);
                    } else if (quote == null && brackets.Count != 0 && c == COMMAND_BRACKET_CLOSE[brackets.Peek()]) {
                        brackets.Pop();
                    } else if (COMMAND_QUOTES.Any(q => q == c)) {
                        quote = quote == null ? c : (char?)null;
                    }
                }

                if (arg.Length > 0) Arguments.Add(arg.ToString());
            }
        }
    }
}
