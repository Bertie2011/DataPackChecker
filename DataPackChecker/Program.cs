using CommandLine;
using CommandLine.Text;
using DataPackChecker.Parsers;
using DataPackChecker.Shared;
using DataPackChecker.Shared.DataPack;
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

            //var ass = Assembly.LoadFrom("./Test.dll");
            //foreach (var type in ass.GetTypes()) {
            //    if (typeof(MyInterface).IsAssignableFrom(type)) {
            //        MyInterface obj = (MyInterface) type.GetConstructor(new Type[0]).Invoke(new object[0]);
            //        obj.DoSomething();
            //    }
            //}
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
            Console.WriteLine("Reading Data Pack...");
            DataPack pack = DataPackParser.From(options.DataPackPath);
            Console.WriteLine("Finished Reading Data Pack!");
            //TODO make rules specify version
            //TODO make rules have test method
            //TODO make Tag single class with type enum
        }
    }
}
