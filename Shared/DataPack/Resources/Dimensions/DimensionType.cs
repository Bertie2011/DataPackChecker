using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace DataPackChecker.Shared.DataPack.Resources.Dimensions {
    public class DimensionType : Resource {
        public JsonElement Content { get; set; }
        public DimensionType(string path, string name) : base(path, name) {}
    }
}
