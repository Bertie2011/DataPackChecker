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
            ConsoleHelper.WriteLine(r.GetType().FullName, ConsoleColor.Gray);
            ConsoleHelper.WriteLine("\nTitle:");
            ConsoleHelper.WriteLine(r.Title, ConsoleColor.Gray);
            ConsoleHelper.WriteLine("\nDescription:");
            ConsoleHelper.WriteLine(r.Description, ConsoleColor.Gray);
            ConsoleHelper.WriteLine("\nExamples:");
            foreach (var example in r.GoodExamples) {
                ConsoleHelper.WriteLine(example, ConsoleColor.Green);
                ConsoleHelper.Write("\n");
            }
            foreach (var example in r.BadExamples) {
                ConsoleHelper.WriteLine(example, ConsoleColor.Red);
                ConsoleHelper.Write("\n");
            }
            if (r.ConfigExamples.Count > 0) {
                ConsoleHelper.WriteLine("Configuration Examples:");
                foreach (var example in r.ConfigExamples) {
                    ConsoleHelper.WriteLine(example, ConsoleColor.Gray);
                    ConsoleHelper.Write("\n");
                }
            } else {
                ConsoleHelper.WriteLine("This rule does not require configuration.");
            }
        }
    }
}
