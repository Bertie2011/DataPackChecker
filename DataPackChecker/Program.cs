using CommandLine;
using CommandLine.Text;
using DataPackChecker.Configuration;
using DataPackChecker.Parsers;
using DataPackChecker.Rules;
using DataPackChecker.Shared;
using DataPackChecker.Shared.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace DataPackChecker {
    class Program {
        const string VERSION = "Data Pack Checker 3.0.0";

        static void Main(string[] args) {
            var parser = new Parser(with => {
                with.EnableDashDash = true;
            });
            var parserResult = parser.ParseArguments<Options>(args);
            parserResult.WithParsed<Options>(options => Run(parserResult, options))
              .WithNotParsed(errs => DisplayErrors(parserResult, errs));
        }

        static void DisplayErrors<T>(ParserResult<T> result, IEnumerable<Error> errs) {
            if (errs.IsVersion()) {
                DisplayVersion();
            } else {
                DisplayHelp(result);
            }
        }

        static void DisplayHelp<T>(ParserResult<T> result) {
            var helpText = HelpText.AutoBuild(result, h => {
                h.Heading = VERSION;
                h.Copyright = "Made by Bertie2011 / ThrownException / Bertiecrafter";
                return HelpText.DefaultParsingErrorsHandler(result, h);
            }, e => e);
            Console.WriteLine(helpText);
        }

        static void DisplayVersion() {
            Console.WriteLine(VERSION);
        }

        private static void Run<T>(ParserResult<T> result, Options options) {
            var (rules, errors) = RuleRegistry.FromDirectory(Path.Join(".", "Rules"));
            var ruleInfo = new RuleInfoPrinter(rules);
            var checker = new Checker(rules);

            if (errors.Count > 0) {
                ConsoleHelper.WriteLine("The following errors occurred while trying to read the rule files (.dll):\n", ConsoleColor.Red);
                foreach (var error in errors) ConsoleHelper.WriteError(error);
                return;
            }

            if (!string.IsNullOrWhiteSpace(options.DataPackPath) && !string.IsNullOrWhiteSpace(options.ConfigPath)) {
                checker.Check(options.DataPackPath, options.ConfigPath);
            } else if (options.RuleList) {
                ruleInfo.PrintList();
            } else if (!string.IsNullOrWhiteSpace(options.RuleInfo)) {
                ruleInfo.PrintOne(options.RuleInfo);
            } else if (options.ConfigHelp) {
                ConsoleHelper.WriteLine(Config.ExampleContents);
            } else {
                DisplayHelp(result);
            }

            if (options.KeepOpen) {
                ConsoleHelper.WriteLine("\nPress any key to exit...", ConsoleColor.Gray);
                Console.ReadKey();
            }
        }
    }
}
