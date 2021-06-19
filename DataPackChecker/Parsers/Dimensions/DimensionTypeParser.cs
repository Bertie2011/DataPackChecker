using DataPackChecker.FileSystems;
using DataPackChecker.Shared.Data;
using DataPackChecker.Shared.Data.Resources.Dimensions;
using System.IO;
using System.Text.Json;

namespace DataPackChecker.Parsers.Dimensions {
    static class DimensionTypeParser {
        static public void FindAndParse(IFileSystem files, string nsPath, Namespace ns) {
            var searchPath = Path.Join(nsPath, "dimension_type");
            if (!files.DirectoryExists(searchPath)) return;
            foreach (var resource in files.EnumerateFiles(searchPath, true)) {
                if (!resource.EndsWith(".json")) continue;
                var path = Path.GetDirectoryName(Path.GetRelativePath(searchPath, resource)).Replace('\\', '/');
                var name = Path.GetFileNameWithoutExtension(resource);
                var dimensionType = new DimensionType(path, name);
                using Stream fs = files.OpenRead(resource);
                dimensionType.Content = JsonDocument.Parse(fs).RootElement;
                ns.DimensionData.DimensionTypes.Add(dimensionType);
            }
        }
    }
}
