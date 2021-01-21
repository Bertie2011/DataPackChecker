namespace DataPackChecker.Shared.Data.Resources.WorldGen {
    public class NoiseSettings : JsonResource {
        public override string FilePath => $"worldgen/noise_settings/{Identifier}.json";
        public NoiseSettings(string path, string name) : base(path, name) {}
    }
}
