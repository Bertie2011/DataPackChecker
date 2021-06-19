using DataPackChecker.FileSystems;
using DataPackChecker.Shared.Data;
using DataPackChecker.Shared.Data.Resources.Dimensions;
using System.IO;
using System.Text.Json;

namespace DataPackChecker.Parsers {
    abstract class JsonParser : IParser {
        protected abstract string PathInNamespace { get; }

        public void FindAndParse(IFileSystem files, string nsPath, Namespace ns) {
            var searchPath = Path.Join(nsPath, PathInNamespace);
            if (!files.DirectoryExists(searchPath)) return;

            foreach (var resource in files.EnumerateFiles(searchPath, true)) {
                if (!resource.EndsWith(".json")) continue;
                var path = Path.GetDirectoryName(Path.GetRelativePath(searchPath, resource)).Replace('\\', '/');
                var name = Path.GetFileNameWithoutExtension(resource);
                using Stream fs = files.OpenRead(resource);
                var json = JsonDocument.Parse(fs).RootElement;
                CreateAndAdd(path, name, json, ns);
            }
        }

        protected abstract void CreateAndAdd(string path, string name, JsonElement json, Namespace ns);
    }
}
