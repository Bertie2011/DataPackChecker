using System.Text.Json;

namespace DataPackChecker.Shared.Data.Resources {
    public class Advancement : JsonResource {
        public override string FilePath => $"advancements/{Identifier}.json";
        public Advancement(string path, string name, JsonElement content) : base(path, name, content) {}
    }
}
