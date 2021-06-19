using DataPackChecker.FileSystems;
using DataPackChecker.Shared.Data;
using DataPackChecker.Shared.Data.Resources.Dimensions;
using System.IO;
using System.Text.Json;

namespace DataPackChecker.Parsers.Dimensions {
    class DimensionParser : IParser {
        public void FindAndParse(IFileSystem files, string nsPath, Namespace ns) {
            var searchPath = Path.Join(nsPath, "dimension");
            if (!files.DirectoryExists(searchPath)) return;
            
            foreach (var resource in files.EnumerateFiles(searchPath, true)) {
                if (!resource.EndsWith(".json")) continue;
                var path = Path.GetDirectoryName(Path.GetRelativePath(searchPath, resource)).Replace('\\', '/');
                var name = Path.GetFileNameWithoutExtension(resource);
                var dimension = new Dimension(path, name);
                using Stream fs = files.OpenRead(resource);
                dimension.Content = JsonDocument.Parse(fs).RootElement;
                ns.DimensionData.Dimensions.Add(dimension);
            }
        }
    }
}
