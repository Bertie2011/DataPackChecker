using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataPackChecker {
    static class ConsoleHelper {
        private static readonly char[] Quotes = new char[] { '"', '\'' };
        private static readonly char[] Escapable = new char[] { '"', '\'' };
        static public void WriteLine(string text, ConsoleColor color = ConsoleColor.White) {
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ForegroundColor = ConsoleColor.White;
        }

        static public void Write(string text, ConsoleColor color = ConsoleColor.White) {
            Console.ForegroundColor = color;
            Console.Write(text);
            Console.ForegroundColor = ConsoleColor.White;
        }

        static public void WriteError(Exception e, ConsoleColor color = ConsoleColor.Red, int depth = 0) {
            if (depth == 0) Console.ForegroundColor = color;
            for (int i = 0; i < depth; i++) {
                Console.Write('\t');
            }
            Console.Error.WriteLine(e.Message);
            if (e.InnerException != null) WriteError(e.InnerException, color, depth + 1);
            if (depth == 0) Console.ForegroundColor = ConsoleColor.White;
        }

        static public string[] CreateArgs(string line) {
            StringBuilder arg = new StringBuilder();
            char? quote = null;
            bool escape = false;
            List<string> args = new List<string>();
            for (int i = 0; i < line.Length; i++) {
                char c = line[i];

                // Either process the created argument or record the next character.
                if (c == ' ' && quote == null && !escape) {
                    args.Add(arg.ToString());
                    arg.Clear();
                } else if (escape || !Quotes.Any(q => q == c) && c != '\\') {
                    if (escape && !Escapable.Any(e => e == c)) arg.Append('\\');
                    arg.Append(c);
                }

                // Change modifiers
                if (escape) {
                    escape = false;
                } else if (c == '\\') {
                    escape = true;
                } else if (Quotes.Any(q => q == c)) {
                    quote = quote == null ? c : (char?)null;
                }
            }

            if (arg.Length > 0) args.Add(arg.ToString());
            return args.ToArray();
        }
    }
}
