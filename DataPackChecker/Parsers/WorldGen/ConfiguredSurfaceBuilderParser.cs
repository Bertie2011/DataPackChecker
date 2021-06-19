using DataPackChecker.FileSystems;
using DataPackChecker.Shared.Data;
using DataPackChecker.Shared.Data.Resources.WorldGen;
using System.IO;
using System.Text.Json;

namespace DataPackChecker.Parsers.WorldGen {
    class ConfiguredSurfaceBuilderParser : JsonParser {
        protected override string PathInNamespace => Path.Join("worldgen", "configured_surface_builder");

        protected override void CreateAndAdd(string path, string name, JsonElement json, Namespace ns) {
            ns.WorldGenData.ConfiguredSurfaceBuilders.Add(new ConfiguredSurfaceBuilder(path, name, json));
        }
    }
}
