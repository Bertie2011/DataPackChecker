using DataPackChecker.FileSystems;
using DataPackChecker.Shared.Data;
using DataPackChecker.Shared.Data.Resources.Tags;
using System.IO;
using System.Text.Json;

namespace DataPackChecker.Parsers.Tags {
    class BlockTagParser : JsonParser {
        protected override string PathInNamespace => Path.Join("tags", "blocks");

        protected override void CreateAndAdd(string path, string name, JsonElement json, Namespace ns) {
            ns.TagData.BlockTags.Add(new BlockTag(path, name, json));
        }
    }
}
