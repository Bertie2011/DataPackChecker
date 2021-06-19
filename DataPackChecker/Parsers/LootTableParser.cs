using DataPackChecker.Shared.Data;
using DataPackChecker.Shared.Data.Resources;
using System.Text.Json;

namespace DataPackChecker.Parsers {
    class LootTableParser : JsonParser {
        protected override string PathInNamespace => "loot_tables";

        protected override void CreateAndAdd(string path, string name, JsonElement json, Namespace ns) {
            ns.LootTables.Add(new LootTable(path, name, json));
        }
    }
}
