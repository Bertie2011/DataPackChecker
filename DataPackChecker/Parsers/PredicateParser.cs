using DataPackChecker.Shared.Data;
using DataPackChecker.Shared.Data.Resources;
using System.IO;
using System.Text.Json;

namespace DataPackChecker.Parsers {
    static class PredicateParser {
        static public void FindAndParse(string nsPath, Namespace ns) {
            var searchPath = Path.Join(nsPath, "predicates");
            if (!Directory.Exists(searchPath)) return;
            foreach (var resource in Directory.EnumerateFiles(searchPath, "*", new EnumerationOptions {
                RecurseSubdirectories = true,
                ReturnSpecialDirectories = false
            })) {
                if (!resource.EndsWith(".json")) continue;
                var path = Path.GetDirectoryName(Path.GetRelativePath(searchPath, resource)).Replace('\\', '/');
                var name = Path.GetFileNameWithoutExtension(resource);
                var predicate = new Predicate(path, name);
                using FileStream fs = new FileStream(resource, FileMode.Open);
                predicate.Content = JsonDocument.Parse(fs).RootElement;
                ns.Predicates.Add(predicate);
            }
        }
    }
}
