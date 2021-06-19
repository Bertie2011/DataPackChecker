using DataPackChecker.FileSystems;
using DataPackChecker.Shared.Data;
using DataPackChecker.Shared.Data.Resources.Dimensions;
using System.IO;
using System.Text.Json;

namespace DataPackChecker.Parsers.Dimensions {
    class DimensionParser : JsonParser {
        protected override string PathInNamespace => "dimension";

        protected override void CreateAndAdd(string path, string name, JsonElement json, Namespace ns) {
            ns.DimensionData.Dimensions.Add(new Dimension(path, name, json));
        }
    }
}
