using DataPackChecker.FileSystems;
using DataPackChecker.Shared.Data;
using DataPackChecker.Shared.Data.Resources.WorldGen;
using System.IO;
using System.Text.Json;

namespace DataPackChecker.Parsers.WorldGen {
    class NoiseSettingsParser : JsonParser {
        protected override string PathInNamespace => Path.Join("worldgen", "noise_settings");

        protected override void CreateAndAdd(string path, string name, JsonElement json, Namespace ns) {
            ns.WorldGenData.NoiseSettings.Add(new NoiseSettings(path, name, json));
        }
    }
}
