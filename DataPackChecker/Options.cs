using CommandLine;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataPackChecker {
    class Options {
        [Option('d', "datapack", HelpText = "Path to root folder of data pack, with data folder and pack.mcmeta file inside.", Required = true, SetName = "Check")]
        public string DataPackPath { get; set; }
    }
}
