using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Linq;
using DataPackChecker.Shared.Collections;
using System.IO;
using DataPackChecker.Shared.Data.Resources;

namespace DataPackChecker.Shared.Data {
    public class DataPack {
        public JsonElement Meta { get; set; }
        public string Path { get; }
        public LookupList<string, Namespace> Namespaces { get; } = new LookupList<string, Namespace>();

        public DataPack(string path, JsonElement meta) {
            this.Meta = meta;
            Path = path;
        }

        public void RebuildReferences() {
            foreach (var ns in Namespaces) {
                foreach (var f in ns.Functions) {
                    HashSet<string> references = new HashSet<string>();
                    foreach (var c in f.CommandsFlat) {
                        if (c.ContentType != Command.Type.Command || c.CommandKey != "function") continue;
                        string[] parts = c.Arguments[0].Split(':', 2);
                        var refNs = parts.Length == 2 ? parts[0] : "minecraft";
                        var refF = parts.Length == 2 ? parts[1] : parts[0];
                        var reference = refNs + ':' + refF;
                        if (!references.Add(reference)) continue;
                        f.References.Add(Namespaces[refNs].Functions[refF]);
                    }
                }
            }
        }
    }
}
