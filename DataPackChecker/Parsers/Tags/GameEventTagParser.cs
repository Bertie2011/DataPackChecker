using DataPackChecker.FileSystems;
using DataPackChecker.Shared.Data;
using DataPackChecker.Shared.Data.Resources.Tags;
using System.IO;
using System.Text.Json;

namespace DataPackChecker.Parsers.Tags {
    class GameEventTagParser : JsonParser {
        protected override string PathInNamespace => Path.Join("tags", "game_events");

        protected override void CreateAndAdd(string path, string name, JsonElement json, Namespace ns) {
            ns.TagData.GameEventTags.Add(new GameEventTag(path, name, json));
        }
    }
}
