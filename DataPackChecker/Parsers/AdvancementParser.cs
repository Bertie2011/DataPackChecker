using DataPackChecker.Shared.DataPack.Resources;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace DataPackChecker.Parsers {
    static class AdvancementParser {
        private static readonly Regex NAMESPACE_PATH_REGEX = new Regex(@"[\\/]advancements([\\/](?<path>.+?))?[\\/](?<name>\w+)\.json$");
        static public Advancement TryParse(string absPath, string nsPath) {
            var match = NAMESPACE_PATH_REGEX.Match(absPath, nsPath.Length);
            if (!match.Success) return null;
            var advancement = new Advancement(match.Groups["path"].Value.Replace('\\', '/'), match.Groups["name"].Value);
            using FileStream fs = new FileStream(absPath, FileMode.Open);
            advancement.Content = JsonDocument.Parse(fs).RootElement;
            return advancement;
        }
    }
}
