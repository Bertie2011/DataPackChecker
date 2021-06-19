using DataPackChecker.FileSystems;
using DataPackChecker.Shared.Data;
using DataPackChecker.Shared.Data.Resources.WorldGen;
using System.IO;
using System.Text.Json;

namespace DataPackChecker.Parsers.WorldGen {
    class TemplatePoolParser : JsonParser {
        protected override string PathInNamespace => Path.Join("worldgen", "template_pool");

        protected override void CreateAndAdd(string path, string name, JsonElement json, Namespace ns) {
            ns.WorldGenData.TemplatePools.Add(new TemplatePool(path, name, json));
        }
    }
}
