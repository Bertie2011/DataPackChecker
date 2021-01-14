using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace DataPackChecker.Shared.DataPack.Resources.WorldGen {
    public class ProcessorList : Resource {
        public JsonElement Content { get; set; }
        public ProcessorList(string path, string name) : base(path, name) {}
    }
}
