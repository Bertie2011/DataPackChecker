using System.Text.Json;

namespace DataPackChecker.Shared.Data.Resources.WorldGen {
    public class Biome : JsonResource {
        public override string FilePath => $"worldgen/biome/{Identifier}.json";
        public Biome(string path, string name, JsonElement content) : base(path, name, content) {}
    }
}
