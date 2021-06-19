using DataPackChecker.FileSystems;
using DataPackChecker.Shared.Data;
using DataPackChecker.Shared.Data.Resources.WorldGen;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace DataPackChecker.Parsers.WorldGen {
    class ConfiguredCarverParser : JsonParser {
        protected override string PathInNamespace => Path.Join("worldgen", "configured_carver");

        protected override void CreateAndAdd(string path, string name, JsonElement json, Namespace ns) {
            ns.WorldGenData.ConfiguredCarvers.Add(new ConfiguredCarver(path, name, json));
        }
    }
}
