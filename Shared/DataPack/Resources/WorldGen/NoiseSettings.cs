using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace DataPackChecker.Shared.DataPack.Resources.WorldGen {
    public class NoiseSettings : Resource {
        public JsonElement Content { get; set; }
        public NoiseSettings(string path, string name) : base(path, name) {}
    }
}
