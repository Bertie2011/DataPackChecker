using DataPackChecker.FileSystems;
using DataPackChecker.Shared.Data;
using DataPackChecker.Shared.Data.Resources.WorldGen;
using System.IO;
using System.Text.Json;

namespace DataPackChecker.Parsers.WorldGen {
    class ConfiguredStructureFeatureParser : JsonParser {
        protected override string PathInNamespace => Path.Join("worldgen", "configured_structure_feature");

        protected override void CreateAndAdd(string path, string name, JsonElement json, Namespace ns) {
            ns.WorldGenData.ConfiguredStructureFeatures.Add(new ConfiguredStructureFeature(path, name, json));
        }
    }
}
