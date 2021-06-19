using DataPackChecker.FileSystems;
using DataPackChecker.Shared.Data;
using DataPackChecker.Shared.Data.Resources.Tags;
using System.IO;
using System.Text.Json;

namespace DataPackChecker.Parsers.Tags {
    class EntityTagParser : JsonParser {
        protected override string PathInNamespace => Path.Join("tags", "entity_types");

        protected override void CreateAndAdd(string path, string name, JsonElement json, Namespace ns) {
            ns.TagData.EntityTags.Add(new EntityTag(path, name, json));
        }
    }
}
