using DataPackChecker.Shared.Data;
using DataPackChecker.Shared.Data.Resources;
using DataPackChecker.Shared.FileSystems;
using System.IO;
using System.Text.Json;

namespace DataPackChecker.Parsers {
    static class RecipeParser {
        static public void FindAndParse(IFileSystem files, string nsPath, Namespace ns) {
            var searchPath = Path.Join(nsPath, "recipes");
            if (!files.DirectoryExists(searchPath)) return;
            foreach (var resource in files.EnumerateFiles(searchPath, true)) {
                if (!resource.EndsWith(".json")) continue;
                var path = Path.GetDirectoryName(Path.GetRelativePath(searchPath, resource)).Replace('\\', '/');
                var name = Path.GetFileNameWithoutExtension(resource);
                var recipe = new Recipe(path, name);
                using Stream fs = files.OpenRead(resource);
                recipe.Content = JsonDocument.Parse(fs).RootElement;
                ns.Recipes.Add(recipe);
            }
        }
    }
}
