using System.Text.Json;

namespace DataPackChecker.Shared.Data.Resources.WorldGen {
    public class ConfiguredStructureFeature : JsonResource {
        public override string FilePath => $"worldgen/configured_structure_feature/{Identifier}.json";
        public override string TypeString => "Configured Structure Feature";
        public ConfiguredStructureFeature(string path, string name, JsonElement content) : base(path, name, content) {}
    }
}
