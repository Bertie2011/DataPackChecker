using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Linq;

namespace DataPackChecker.Shared.Data {
    public class DataPack {
        public JsonElement Meta { get; set; }
        public List<Namespace> Namespaces { get; } = new List<Namespace>();

        public DataPack(JsonElement meta) {
            this.Meta = meta;
        }

        public void RebuildReferences() {
            foreach (var ns in Namespaces) {
                foreach (var f in ns.Functions) {
                    foreach (var c in f.CommandsFlat) {
                        if (c.CommandKey != "function") return;
                        //TODO rebuild references.
                    }
                }
            }
        }
    }
}
