using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace DataPackChecker.Shared.DataPack.Resources {
    public class Advancement : Resource {
        public JsonElement Content { get; set; }
        public Advancement(string path, string name) : base(path, name) {}
    }
}
