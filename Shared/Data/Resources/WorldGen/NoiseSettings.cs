﻿namespace DataPackChecker.Shared.Data.Resources.WorldGen {
    public class NoiseSettings : JsonResource {
        public override string FilePath => $"worldgen/noise_settings/{Identifier}.json";
        public override string TypeString => "Noise Settings";
        public NoiseSettings(string path, string name) : base(path, name) {}
    }
}
