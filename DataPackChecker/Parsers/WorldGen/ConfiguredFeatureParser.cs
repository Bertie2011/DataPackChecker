using DataPackChecker.Shared.Data.Resources.WorldGen;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace DataPackChecker.Parsers.WorldGen {
    static class ConfiguredFeatureParser {
        private static readonly Regex NamespacePathRegex = new Regex(@"[\\/]worldgen[\\/]configured_feature([\\/](?<path>.+?))?[\\/](?<name>[^\\/]+)\.json$");
        static public ConfiguredFeature TryParse(string absPath, string nsPath) {
            var match = NamespacePathRegex.Match(absPath, nsPath.Length);
            if (!match.Success) return null;
            var configuredFeature = new ConfiguredFeature(match.Groups["path"].Value.Replace('\\', '/'), match.Groups["name"].Value);
            using FileStream fs = new FileStream(absPath, FileMode.Open);
            configuredFeature.Content = JsonDocument.Parse(fs).RootElement;
            return configuredFeature;
        }
    }
}
