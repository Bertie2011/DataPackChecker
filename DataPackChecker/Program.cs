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
                with.CaseInsensitiveEnumValues = true;
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

            if (options.RuleList) {
                ruleInfo.PrintList();
            } else if (!string.IsNullOrWhiteSpace(options.RuleInfo)) {
                ruleInfo.PrintOne(options.RuleInfo);
            } else if (options.ConfigHelp) {
                ConsoleHelper.WriteLine(Config.ExampleContents);
            } else {
                if (options.RequiresBaseAndWorld) {
                    options.EnsureBasePath();
                    options.EnsureWorld();
                }
                Console.Clear();
                options.EnsureDataPackPath();
                Console.Clear();
                options.EnsureConfigPath();
                Console.Clear();
                var fullPath = options.RequiresBaseAndWorld ? Path.Join(options.BasePath, "saves", options.World, "datapacks", options.DataPackPath) : options.DataPackPath;
                
                checker.Check(fullPath, options.ConfigPath);
                while (options.Life == Options.LifeTime.REPEAT) {
                    ConsoleHelper.WriteLine("\nPress any key to continue...", ConsoleColor.Gray);
                    Console.ReadKey();
                    Console.Clear();
                    checker.Check(fullPath, options.ConfigPath);
                }
            }

            if (options.Life == Options.LifeTime.AWAIT || options.Life == Options.LifeTime.REPEAT) {
                ConsoleHelper.WriteLine("\nPress any key to exit...", ConsoleColor.Gray);
                Console.ReadKey();
            }
        }
    }
}
