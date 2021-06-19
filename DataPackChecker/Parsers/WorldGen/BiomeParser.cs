using DataPackChecker.FileSystems;
using DataPackChecker.Shared.Data;
using DataPackChecker.Shared.Data.Resources.WorldGen;
using System.IO;
using System.Text.Json;

namespace DataPackChecker.Parsers.WorldGen {
    class BiomeParser : JsonParser {
        protected override string PathInNamespace => Path.Join("worldgen", "biome");

        protected override void CreateAndAdd(string path, string name, JsonElement json, Namespace ns) {
            ns.WorldGenData.Biomes.Add(new Biome(path, name, json));
        }
    }
}
