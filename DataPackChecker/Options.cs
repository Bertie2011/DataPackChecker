using CommandLine;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataPackChecker {
    class Options {
        [Option('d', "data-pack", HelpText = "Path to root folder of data pack, with data folder and pack.mcmeta file inside.", SetName = "Check")]
        public string DataPackPath { get; set; }
        [Option('l', "rule-list", HelpText = "List all the rules.", SetName = "Rule List")]
        public bool RuleList { get; set; }
        [Option('i', "rule-info", HelpText = "Get more information about a rule like a description, configuration and examples.", SetName = "Rule Info")]
        public string RuleInfo { get; set; }
    }
}
