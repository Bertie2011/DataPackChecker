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
using System.Linq;
using System.Reflection;

namespace DataPackChecker {
    class Program {
        static void Main(string[] args) {
            var parser = new Parser(with => {
                with.EnableDashDash = true;
                with.AutoVersion = false;
            });

            var parserResult = parser.ParseArguments<Options>(args);
            parserResult.WithParsed(options => Run(parserResult, options))
              .WithNotParsed(errs => DisplayHelp(parserResult));
        }

        private static void DisplayHelp<T>(ParserResult<T> result) {
            var helpText = HelpText.AutoBuild(result, h => {
                h.Heading = "Data Pack Checker";
                h.Copyright = "Made by Bertie2011 / ThrownException / Bertiecrafter";
                return HelpText.DefaultParsingErrorsHandler(result, h);
            }, e => e);
            Console.WriteLine(helpText);
        }

        private static void FromFile(string path) {
            try {
                Main(ConsoleHelper.CreateArgs(File.ReadAllLines(path)[0]));
            } catch (Exception e) {
                ConsoleHelper.WriteError(e);
            }
        }

        private static void Run<T>(ParserResult<T> result, Options options) {
            if (!string.IsNullOrWhiteSpace(options.ArgsPath)) {
                FromFile(options.ArgsPath);
                return;
            }

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
