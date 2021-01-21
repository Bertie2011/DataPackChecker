namespace DataPackChecker.Shared.Data.Resources.WorldGen {
    public class Biome : JsonResource {
        public override string FilePath => $"worldgen/biome/{Identifier}.json";
        public Biome(string path, string name) : base(path, name) {}
    }
}
