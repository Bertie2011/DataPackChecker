﻿namespace DataPackChecker.Shared.Data.Resources.WorldGen {
    public class ConfiguredCarver : JsonResource {
        public override string FilePath => $"worldgen/configured_carver/{Identifier}.json";
        public override string TypeString => "Configured Carver";
        public ConfiguredCarver(string path, string name) : base(path, name) {}
    }
}
