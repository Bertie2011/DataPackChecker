using System;
using System.Collections.Generic;
using System.Text;

namespace DataPackChecker {
    static class ConsoleHelper {
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
    }
}
