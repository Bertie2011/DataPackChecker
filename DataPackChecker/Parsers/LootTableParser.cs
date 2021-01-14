using DataPackChecker.Shared.DataPack.Resources;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace DataPackChecker.Parsers {
    static class LootTableParser {
        private static readonly Regex NAMESPACE_PATH_REGEX = new Regex(@"[\\/]loot_tables([\\/](?<path>.+?))?[\\/](?<name>\w+)\.json$");
        static public LootTable TryParse(string absPath, string nsPath) {
            var match = NAMESPACE_PATH_REGEX.Match(absPath, nsPath.Length);
            if (!match.Success) return null;
            var lootTable = new LootTable(match.Groups["path"].Value.Replace('\\', '/'), match.Groups["name"].Value);
            using FileStream fs = new FileStream(absPath, FileMode.Open);
            lootTable.Content = JsonDocument.Parse(fs).RootElement;
            return lootTable;
        }
    }
}
