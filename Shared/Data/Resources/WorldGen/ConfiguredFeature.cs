namespace DataPackChecker.Shared.Data.Resources.WorldGen {
    public class ConfiguredFeature : JsonResource {
        public override string FilePath => $"worldgen/configured_feature/{Identifier}.json";
        public ConfiguredFeature(string path, string name) : base(path, name) {}
    }
}
