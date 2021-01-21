﻿using DataPackChecker.Shared.Data.Resources.WorldGen;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace DataPackChecker.Parsers.WorldGen {
    static class BiomeParser {
        private static readonly Regex NamespacePathRegex = new Regex(@"[\\/]worldgen[\\/]biome([\\/](?<path>.+?))?[\\/](?<name>[^\\/]+)\.json$");
        static public Biome TryParse(string absPath, string nsPath) {
            var match = NamespacePathRegex.Match(absPath, nsPath.Length);
            if (!match.Success) return null;
            var biome = new Biome(match.Groups["path"].Value.Replace('\\', '/'), match.Groups["name"].Value);
            using FileStream fs = new FileStream(absPath, FileMode.Open);
            biome.Content = JsonDocument.Parse(fs).RootElement;
            return biome;
        }
    }
}
