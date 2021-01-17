using CommandLine;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataPackChecker {
    class Options {
        [Option('o', "keep-open", HelpText = "Keep process running after finishing.")]
        public bool KeepOpen { get; set; }
        [Option('d', "data-pack", HelpText = "Path to root folder of data pack, with data folder and pack.mcmeta file inside. Requires -c, --config.", SetName = "Check")]
        public string DataPackPath { get; set; }
        [Option('c', "config", HelpText = "Path to configuration file that specifies the rules. Requires -d, --data-pack.", SetName = "Check")]
        public string ConfigPath { get; set; }
        [Option("config-help", HelpText = "Shows example contents of a configuration file.", SetName = "Config Help")]
        public bool ConfigHelp { get; set; }
        [Option('l', "rule-list", HelpText = "List all the rules.", SetName = "Rule List")]
        public bool RuleList { get; set; }
        [Option('i', "rule-info", HelpText = "Get more information about a rule like a description, configuration and examples.", SetName = "Rule Info")]
        public string RuleInfo { get; set; }
    }
}
