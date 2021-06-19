using System.Text.Json;

namespace DataPackChecker.Shared.Data.Resources.WorldGen {
    public class TemplatePool : JsonResource {
        public override string FilePath => $"worldgen/template_pool/{Identifier}.json";
        public override string TypeString => "Template Pool";
        public TemplatePool(string path, string name, JsonElement content) : base(path, name, content) {}
    }
}
