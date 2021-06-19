using DataPackChecker.FileSystems;
using DataPackChecker.Shared.Data;
using DataPackChecker.Shared.Data.Resources.Dimensions;
using System.IO;
using System.Text.Json;

namespace DataPackChecker.Parsers.Dimensions {
    class DimensionTypeParser : JsonParser {
        protected override string PathInNamespace => "dimension_type";

        protected override void CreateAndAdd(string path, string name, JsonElement json, Namespace ns) {
            ns.DimensionData.DimensionTypes.Add(new DimensionType(path, name, json));
        }
    }
}
