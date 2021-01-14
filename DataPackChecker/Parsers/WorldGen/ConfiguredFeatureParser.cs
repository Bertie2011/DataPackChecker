using DataPackChecker.Shared.DataPack.Resources.WorldGen;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace DataPackChecker.Parsers.WorldGen {
    static class ConfiguredFeatureParser {
        private static readonly Regex NAMESPACE_PATH_REGEX = new Regex(@"[\\/]worldgen[\\/]configured_feature([\\/](?<path>.+?))?[\\/](?<name>[^\\/]+)\.json$");
        static public ConfiguredFeature TryParse(string absPath, string nsPath) {
            var match = NAMESPACE_PATH_REGEX.Match(absPath, nsPath.Length);
            if (!match.Success) return null;
            var configuredFeature = new ConfiguredFeature(match.Groups["path"].Value.Replace('\\', '/'), match.Groups["name"].Value);
            using FileStream fs = new FileStream(absPath, FileMode.Open);
            configuredFeature.Content = JsonDocument.Parse(fs).RootElement;
            return configuredFeature;
        }
    }
}
