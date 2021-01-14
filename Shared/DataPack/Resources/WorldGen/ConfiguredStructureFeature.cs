using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace DataPackChecker.Shared.DataPack.Resources.WorldGen {
    public class ConfiguredStructureFeature : Resource {
        public JsonElement Content { get; set; }
        public ConfiguredStructureFeature(string path, string name) : base(path, name) {}
    }
}
