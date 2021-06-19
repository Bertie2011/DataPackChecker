using DataPackChecker.FileSystems;
using DataPackChecker.Shared.Data;
using DataPackChecker.Shared.Data.Resources.WorldGen;
using System.IO;
using System.Text.Json;

namespace DataPackChecker.Parsers.WorldGen {
    class ProcessorListParser : JsonParser {
        protected override string PathInNamespace => Path.Join("worldgen", "processor_list");

        protected override void CreateAndAdd(string path, string name, JsonElement json, Namespace ns) {
            ns.WorldGenData.ProcessorLists.Add(new ProcessorList(path, name, json));
        }
    }
}
