using System.Text.Json;

namespace DataPackChecker.Shared.Data.Resources.Dimensions {
    public class Dimension : JsonResource {
        public override string FilePath => $"dimension/{Identifier}.json";

        public Dimension(string path, string name, JsonElement content) : base(path, name, content) {}
    }
}
