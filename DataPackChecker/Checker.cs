using DataPackChecker.Configuration;
using DataPackChecker.Parsers;
using DataPackChecker.Rules;
using DataPackChecker.Shared;
using DataPackChecker.Shared.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

namespace DataPackChecker {
    class Checker {
        private RuleRegistry RuleRegistry { get; set; }
        public Checker(RuleRegistry rules) {
            RuleRegistry = rules;
        }

        public void Check(string dataPackPath, string configPath) {
            ConsoleHelper.WriteLine("Reading Data Pack...");
            DataPack pack;
            try {
                pack = DataPackParser.From(dataPackPath);
            } catch (Exception e) {
                ConsoleHelper.WriteError(e);
                return;
            }
            ConsoleHelper.WriteLine("Finished Reading Data Pack!");
            ConsoleHelper.WriteLine("Reading Config...");
            var (config, confErrors) = Config.From(configPath);
            confErrors.AddRange(config.Entries
                .Where(e => !RuleRegistry.Rules.Any(r => r.GetType().FullName == e.Rule))
                .Select(e => new InvalidDataException("Rule not found: " + e.Rule)));
            if (confErrors.Count > 0) {
                ConsoleHelper.WriteLine("The following errors occurred while trying to read the config file:\n", ConsoleColor.Red);
                foreach (var error in confErrors) ConsoleHelper.WriteError(error);
                return;
            }
            ConsoleHelper.WriteLine("Finished Reading Config!");
            Check(pack, config);
        }

        public void Check(DataPack dataPack, Config config) {
            var counter = new CountdownEvent(config.Entries.Count);
            foreach (var entry in config.Entries) {
                var rule = RuleRegistry.Rules.Find(r => r.GetType().FullName == entry.Rule);
                var output = new Output(rule);
                ThreadPool.QueueUserWorkItem(s => {
                    try {
                        rule.Run(dataPack, entry.Config, output);
                    } catch (Exception e) {
                        output.Error(e);
                    }
                    output.Print();
                    counter.Signal();
                });
            }
            counter.Wait();
        }
    }
}
