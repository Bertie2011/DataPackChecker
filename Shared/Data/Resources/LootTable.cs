namespace DataPackChecker.Shared.Data.Resources {
    public class LootTable : JsonResource {
        public override string FilePath => $"loot_tables/{Identifier}.json";

        public LootTable(string path, string name) : base(path, name) {}
    }
}
