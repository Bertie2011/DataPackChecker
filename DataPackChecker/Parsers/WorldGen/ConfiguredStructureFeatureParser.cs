using DataPackChecker.Shared.Data.Resources.WorldGen;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace DataPackChecker.Parsers.WorldGen {
    static class ConfiguredStructureFeatureParser {
        private static readonly Regex NamespacePathRegex = new Regex(@"[\\/]worldgen[\\/]configured_structure_feature([\\/](?<path>.+?))?[\\/](?<name>[^\\/]+)\.json$");
        static public ConfiguredStructureFeature TryParse(string absPath, string nsPath) {
            var match = NamespacePathRegex.Match(absPath, nsPath.Length);
            if (!match.Success) return null;
            var configuredStructureFeature = new ConfiguredStructureFeature(match.Groups["path"].Value.Replace('\\', '/'), match.Groups["name"].Value);
            using FileStream fs = new FileStream(absPath, FileMode.Open);
            configuredStructureFeature.Content = JsonDocument.Parse(fs).RootElement;
            return configuredStructureFeature;
        }
    }
}
