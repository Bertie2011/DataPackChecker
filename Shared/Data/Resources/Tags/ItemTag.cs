using System.Text.Json;

namespace DataPackChecker.Shared.Data.Resources.Tags {
    public class ItemTag : Tag {
        public override string FilePath => $"tags/items/{Identifier}.json";
        public override string TypeString => "Tag (Items)";
        public ItemTag(string path, string name, JsonElement content) : base(path, name, content) {}
    }
}
