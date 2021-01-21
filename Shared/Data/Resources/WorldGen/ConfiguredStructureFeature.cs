namespace DataPackChecker.Shared.Data.Resources.WorldGen {
    public class ConfiguredStructureFeature : JsonResource {
        public override string FilePath => $"worldgen/configured_structure_feature/{Identifier}.json";
        public ConfiguredStructureFeature(string path, string name) : base(path, name) {}
    }
}
