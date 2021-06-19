using System.Text.Json;

namespace DataPackChecker.Shared.Data.Resources {
    public class ItemModifier : JsonResource {
        public override string FilePath => $"item_modifiers/{Identifier}.json";
        public ItemModifier(string path, string name, JsonElement content) : base(path, name, content) {}
    }
}
