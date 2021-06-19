using DataPackChecker.FileSystems;
using DataPackChecker.Shared.Data;
using DataPackChecker.Shared.Data.Resources.Tags;
using System.IO;
using System.Text.Json;

namespace DataPackChecker.Parsers.Tags {
    class FunctionTagParser : JsonParser {
        protected override string PathInNamespace => Path.Join("tags", "functions");

        protected override void CreateAndAdd(string path, string name, JsonElement json, Namespace ns) {
            ns.TagData.FunctionTags.Add(new FunctionTag(path, name, json));
        }
    }
}
