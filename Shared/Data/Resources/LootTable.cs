namespace DataPackChecker.Shared.Data.Resources {
    public class LootTable : JsonResource {
        public override string FilePath => $"loot_tables/{Identifier}.json";

        public override string TypeString => "Loot Table";

        public LootTable(string path, string name) : base(path, name) {}
    }
}
