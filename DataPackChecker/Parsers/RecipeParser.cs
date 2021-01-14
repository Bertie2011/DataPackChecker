using DataPackChecker.Shared.DataPack.Resources;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace DataPackChecker.Parsers {
    static class RecipeParser {
        private static readonly Regex NAMESPACE_PATH_REGEX = new Regex(@"[\\/]recipes([\\/](?<path>.+?))?[\\/](?<name>\w+)\.json$");
        static public Recipe TryParse(string absPath, string nsPath) {
            var match = NAMESPACE_PATH_REGEX.Match(absPath, nsPath.Length);
            if (!match.Success) return null;
            var recipe = new Recipe(match.Groups["path"].Value.Replace('\\', '/'), match.Groups["name"].Value);
            using FileStream fs = new FileStream(absPath, FileMode.Open);
            recipe.Content = JsonDocument.Parse(fs).RootElement;
            return recipe;
        }
    }
}
