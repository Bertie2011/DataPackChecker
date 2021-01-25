using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Linq;
using DataPackChecker.Shared.Collections;
using System.IO;
using DataPackChecker.Shared.Data.Resources;
using DataPackChecker.Shared.Data.Resources.Tags;

namespace DataPackChecker.Shared.Data {
    public class DataPack {
        public JsonElement Meta { get; set; }
        public string Path { get; }
        public LookupList<string, Namespace> Namespaces { get; } = new LookupList<string, Namespace>();

        public DataPack(string path, JsonElement meta) {
            this.Meta = meta;
            Path = path;
        }

        /// <summary>
        /// Find functions by namespaced identifier.
        /// </summary>
        /// <param name="identifier">A namespaced identifier, possibly prefixed by # to indicate a tag.<br>(#){namespace}:{path}/{name}</br></param>
        /// <returns>All functions found by recursively parsing all tags.</returns>
        public List<Function> FindFunctions(string identifier) {
            HashSet<Function> result = new HashSet<Function>();
            Queue<string> queue = new Queue<string>();
            HashSet<string> visited = new HashSet<string>();
            queue.Enqueue(identifier);
            visited.Add(identifier);

            while (queue.Count > 0) {
                // Extract namespace name and identifier.
                var parts = queue.Dequeue().Split(':', 2);
                var isTag = parts[0].StartsWith('#');
                var nsName = parts.Length == 2 ? parts[0] : "minecraft";
                if (isTag) nsName = nsName.Substring(1);
                var id = parts.Length == 2 ? parts[1] : parts[0];
                if (parts.Length != 2 && isTag) id = id.Substring(1);

                // See if we can find the identifier and add it to the queue or result.
                if (!Namespaces.TryGetValue(nsName, out Namespace ns)) continue;
                if (isTag && ns.TagData.FunctionTags.TryGetValue(id, out FunctionTag ft)) {
                    foreach (var v in ft.Entries) {
                        if (visited.Contains(v.Identifier)) continue;
                        visited.Add(v.Identifier);
                        queue.Enqueue(v.Identifier);
                    }
                } else if (!isTag && ns.Functions.TryGetValue(id, out Function f)) {
                    result.Add(f);
                }
            }

            return result.ToList();
        }

        /// <summary>
        /// Rebuilds references marked with <see cref="AutoReferenceAttribute"/>.
        /// </summary>
        public void RebuildReferences() {
            foreach (var ns in Namespaces) {
                ns.DataPack = this;
                foreach (var f in ns.Functions) {
                    f.Namespace = ns;
                    f.References.Clear();
                    foreach (var c in f.CommandsFlat) {
                        c.Function = f;
                        if (c.ContentType != Command.Type.Command || c.CommandKey != "function") continue;
                        f.References.AddRange(FindFunctions(c.Arguments[0]));
                    }
                }
                foreach (var ft in ns.TagData.FunctionTags) {
                    ft.References.Clear();
                    ft.References.AddRange(FindFunctions('#' + ns.Name + ':' + ft.Identifier));
                }
            }
        }
    }
}
