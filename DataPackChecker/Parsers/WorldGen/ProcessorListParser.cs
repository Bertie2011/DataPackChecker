using DataPackChecker.Shared.DataPack.Resources.WorldGen;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace DataPackChecker.Parsers.WorldGen {
    static class ProcessorListParser {
        private static readonly Regex NAMESPACE_PATH_REGEX = new Regex(@"[\\/]worldgen[\\/]processor_list([\\/](?<path>.+?))?[\\/](?<name>[^\\/]+)\.json$");
        static public ProcessorList TryParse(string absPath, string nsPath) {
            var match = NAMESPACE_PATH_REGEX.Match(absPath, nsPath.Length);
            if (!match.Success) return null;
            var processorList = new ProcessorList(match.Groups["path"].Value.Replace('\\', '/'), match.Groups["name"].Value);
            using FileStream fs = new FileStream(absPath, FileMode.Open);
            processorList.Content = JsonDocument.Parse(fs).RootElement;
            return processorList;
        }
    }
}
