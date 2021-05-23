namespace DataPackChecker.Shared.Data.Resources.WorldGen {
    public class ConfiguredSurfaceBuilder : JsonResource {
        public override string FilePath => $"worldgen/configured_surface_builder/{Identifier}.json";
        public override string TypeString => "Configured Surface Builder";
        public ConfiguredSurfaceBuilder(string path, string name) : base(path, name) {}
    }
}
