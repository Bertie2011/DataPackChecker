using DataPackChecker.Shared.Data;
using DataPackChecker.Shared.Data.Resources;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace DataPackChecker.Parsers {
    static public class FunctionParser {
        static public void FindAndParse(string nsPath, Namespace ns) {
            var searchPath = Path.Join(nsPath, "functions");
            if (!Directory.Exists(searchPath)) return;
            foreach (var resource in Directory.EnumerateFiles(searchPath, "*", new EnumerationOptions {
                RecurseSubdirectories = true,
                ReturnSpecialDirectories = false
            })) {
                if (!resource.EndsWith(".mcfunction")) continue;
                var path = Path.GetDirectoryName(Path.GetRelativePath(searchPath, resource)).Replace('\\', '/');
                var name = Path.GetFileNameWithoutExtension(resource);
                var function = new Function(path, name);
                function.Commands = File.ReadAllLines(resource).Select((c, i) => new Command(i + 1, c)).ToList();
                ns.Functions.Add(function);
            }           
        }
    }
}
