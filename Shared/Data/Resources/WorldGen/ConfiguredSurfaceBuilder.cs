namespace DataPackChecker.Shared.Data.Resources.WorldGen {
    public class ConfiguredSurfaceBuilder : JsonResource {
        public override string FilePath => $"worldgen/configured_surface_builder/{Identifier}.json";
        public ConfiguredSurfaceBuilder(string path, string name) : base(path, name) {}
    }
}
