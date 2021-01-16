using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using DataPackChecker.Shared;

namespace DataPackChecker.Rules {
    class RuleInfoPrinter {
        private RuleRegistry Registry { get; }

        public RuleInfoPrinter(RuleRegistry registry) {
            Registry = registry;
        }
        public void PrintList() {
            ConsoleHelper.WriteLine("The following rules are available in the loaded Rule files (./Rules/*.dll):\n");
            foreach (var rule in Registry.Rules) {
                ConsoleHelper.WriteLine(rule.GetType().FullName);
                ConsoleHelper.Write("\t");
                ConsoleHelper.WriteLine(rule.Title, ConsoleColor.Gray);
            }
        }
        public void PrintOne(string identifier) {
            CheckerRule r = Registry.Rules.Find(r => r.GetType().FullName == identifier);
            if (r == null) {
                ConsoleHelper.WriteLine("Could not find rule with that identifier!", ConsoleColor.Red);
                return;
            }
            ConsoleHelper.WriteLine("Identifier:");
            ConsoleHelper.Write("\t");
            ConsoleHelper.WriteLine(r.GetType().FullName, ConsoleColor.Gray);
            ConsoleHelper.WriteLine("\nTitle:");
            ConsoleHelper.Write("\t");
            ConsoleHelper.WriteLine(r.Title, ConsoleColor.Gray);
            ConsoleHelper.WriteLine("\nDescription:");
            ConsoleHelper.Write("\t");
            ConsoleHelper.WriteLine(r.Description, ConsoleColor.Gray);
            ConsoleHelper.WriteLine("\nExamples:");
            ConsoleHelper.Write("\t");
            ConsoleHelper.WriteLine(r.GoodExample, ConsoleColor.Green);
            ConsoleHelper.Write("\t");
            ConsoleHelper.WriteLine(r.BadExample, ConsoleColor.Red);
            if (r.ConfigExample != null) {
                ConsoleHelper.WriteLine("\nConfiguration Example:");
                ConsoleHelper.WriteLine(r.ConfigExample, ConsoleColor.Gray);
            } else {
                ConsoleHelper.WriteLine("\nThis rule does not require configuration.");
            }
        }
    }
}
