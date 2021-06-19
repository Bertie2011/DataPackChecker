using DataPackChecker.FileSystems;
using DataPackChecker.Shared.Data;
using DataPackChecker.Shared.Data.Resources;
using System.IO;
using System.Linq;

namespace DataPackChecker.Parsers {
    public class FunctionParser : IParser {
        public void FindAndParse(IFileSystem files, string nsPath, Namespace ns) {
            var searchPath = Path.Join(nsPath, "functions");
            if (!files.DirectoryExists(searchPath)) return;
            foreach (var resource in files.EnumerateFiles(searchPath, true)) {
                if (!resource.EndsWith(".mcfunction")) continue;
                var path = Path.GetDirectoryName(Path.GetRelativePath(searchPath, resource)).Replace('\\', '/');
                var name = Path.GetFileNameWithoutExtension(resource);
                var function = new Function(path, name);
                function.Commands = files.ReadAllLines(resource).Select((c, i) => new Command(i + 1, c)).ToList();
                ns.Functions.Add(function);
            }           
        }
    }
}
