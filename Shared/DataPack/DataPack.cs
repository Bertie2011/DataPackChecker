using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace DataPackChecker.Shared.DataPack {
    public class DataPack {
        public JsonElement Meta { get; set; }
        public List<Namespace> Namespaces { get; set; }

        public DataPack(JsonElement meta) {
            this.Meta = meta;
        }
    }
}
