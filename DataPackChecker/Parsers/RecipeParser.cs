using DataPackChecker.FileSystems;
using DataPackChecker.Shared.Data;
using DataPackChecker.Shared.Data.Resources;
using System.IO;
using System.Text.Json;

namespace DataPackChecker.Parsers {
    class RecipeParser : JsonParser {
        protected override string PathInNamespace => "recipes";

        protected override void CreateAndAdd(string path, string name, JsonElement json, Namespace ns) {
            ns.Recipes.Add(new Recipe(path, name, json));
        }
    }
}
