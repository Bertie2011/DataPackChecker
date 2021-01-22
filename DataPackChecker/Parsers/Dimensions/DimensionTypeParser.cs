using DataPackChecker.Shared.Data;
using DataPackChecker.Shared.Data.Resources.Dimensions;
using System.IO;
using System.Text.Json;

namespace DataPackChecker.Parsers.Dimensions {
    static class DimensionTypeParser {
        static public void FindAndParse(string nsPath, Namespace ns) {
            var searchPath = Path.Join(nsPath, "dimension_type");
            if (!Directory.Exists(searchPath)) return;
            foreach (var resource in Directory.EnumerateFiles(searchPath, "*", new EnumerationOptions {
                RecurseSubdirectories = true,
                ReturnSpecialDirectories = false
            })) {
                if (!resource.EndsWith(".json")) continue;
                var path = Path.GetDirectoryName(Path.GetRelativePath(searchPath, resource)).Replace('\\', '/');
                var name = Path.GetFileNameWithoutExtension(resource);
                var dimensionType = new DimensionType(path, name);
                using FileStream fs = new FileStream(resource, FileMode.Open);
                dimensionType.Content = JsonDocument.Parse(fs).RootElement;
                ns.DimensionData.DimensionTypes.Add(dimensionType);
            }
        }
    }
}
