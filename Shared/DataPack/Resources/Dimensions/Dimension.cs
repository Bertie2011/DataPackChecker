using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace DataPackChecker.Shared.DataPack.Resources.Dimensions {
    public class Dimension : Resource {
        public JsonElement Content { get; set; }
        public Dimension(string path, string name) : base(path, name) {}
    }
}
