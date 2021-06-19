using DataPackChecker.FileSystems;
using DataPackChecker.Shared.Data;
using DataPackChecker.Shared.Data.Resources.WorldGen;
using System.IO;
using System.Text.Json;

namespace DataPackChecker.Parsers.WorldGen {
    static class ProcessorListParser {
        static public void FindAndParse(IFileSystem files, string nsPath, Namespace ns) {
            var searchPath = Path.Join(nsPath, "worldgen", "processor_list");
            if (!files.DirectoryExists(searchPath)) return;
            foreach (var resource in files.EnumerateFiles(searchPath, true)) {
                if (!resource.EndsWith(".json")) continue;
                var path = Path.GetDirectoryName(Path.GetRelativePath(searchPath, resource)).Replace('\\', '/');
                var name = Path.GetFileNameWithoutExtension(resource);
                var worldGenElement = new ProcessorList(path, name);
                using Stream fs = files.OpenRead(resource);
                worldGenElement.Content = JsonDocument.Parse(fs).RootElement;
                ns.WorldGenData.ProcessorLists.Add(worldGenElement);
            }
        }
    }
}
