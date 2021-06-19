using DataPackChecker.FileSystems;
using DataPackChecker.Shared.Data;
using DataPackChecker.Shared.Data.Resources;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace DataPackChecker.Parsers {
    class LootTableParser : IParser {
        public void FindAndParse(IFileSystem files, string nsPath, Namespace ns) {
            var searchPath = Path.Join(nsPath, "loot_tables");
            if (!files.DirectoryExists(searchPath)) return;
            foreach (var resource in files.EnumerateFiles(searchPath, true)) {
                if (!resource.EndsWith(".json")) continue;
                var path = Path.GetDirectoryName(Path.GetRelativePath(searchPath, resource)).Replace('\\', '/');
                var name = Path.GetFileNameWithoutExtension(resource);
                var lootTable = new LootTable(path, name);
                using Stream fs = files.OpenRead(resource);
                lootTable.Content = JsonDocument.Parse(fs).RootElement;
                ns.LootTables.Add(lootTable);
            }
        }
    }
}
