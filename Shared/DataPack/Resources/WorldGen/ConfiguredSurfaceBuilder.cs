using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace DataPackChecker.Shared.DataPack.Resources.WorldGen {
    public class ConfiguredSurfaceBuilder : Resource {
        public JsonElement Content { get; set; }
        public ConfiguredSurfaceBuilder(string path, string name) : base(path, name) {}
    }
}
