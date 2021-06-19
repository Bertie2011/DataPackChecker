using System.Text.Json;

namespace DataPackChecker.Shared.Data.Resources {
    public class Predicate : JsonResource {
        public override string FilePath => $"predicates/{Identifier}.json";
        public Predicate(string path, string name, JsonElement content) : base(path, name, content) {}
    }
}
