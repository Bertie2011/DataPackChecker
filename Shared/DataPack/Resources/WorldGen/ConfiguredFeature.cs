using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace DataPackChecker.Shared.DataPack.Resources.WorldGen {
    public class ConfiguredFeature : Resource {
        public JsonElement Content { get; set; }
        public ConfiguredFeature(string path, string name) : base(path, name) {}
    }
}
