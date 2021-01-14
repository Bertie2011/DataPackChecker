using DataPackChecker.Shared.DataPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Linq;
using System.Text.RegularExpressions;
using DataPackChecker.Shared.DataPack.Resources;

namespace DataPackChecker.Parsers {
    static class DataPackParser {
        // Root directory
        public static DataPack From(string path) {
            path = Path.TrimEndingDirectorySeparator(path);
            if (!Directory.Exists(path)) throw new ArgumentException("Specified data pack does not exist.");

            DataPack pack = new DataPack(ParseMcMeta(path));
            ParseNamespaces(path, pack);
            Console.WriteLine(pack.Namespaces[0].Advancements[0].Content);
            return pack;
        }

        // Root directory
        private static JsonElement ParseMcMeta(string path) {
            var mcmeta = Path.Join(path, "pack.mcmeta");
            if (!File.Exists(mcmeta)) throw new ArgumentException("pack.mcmeta in data pack does not exist.");

            JsonDocument mcmetaContent;
            using (FileStream input = new FileStream(mcmeta, FileMode.Open)) {
                mcmetaContent = JsonDocument.Parse(input);
            }
            return mcmetaContent.RootElement;
        }

        // Root directory
        private static void ParseNamespaces(string path, DataPack pack) {
            var dataPath = Path.Join(path, "data");
            foreach (var namespacePath in Directory.EnumerateDirectories(dataPath)) {
                var namespaceObj = new Namespace(Path.GetFileName(namespacePath));
                ParseResources(namespacePath, namespaceObj);
                pack.Namespaces.Add(namespaceObj);
            }
        }

        // Namespace directory
        private static void ParseResources(string path, Namespace ns) {
            foreach (var resource in Directory.EnumerateFiles(path, "*", new EnumerationOptions {
                RecurseSubdirectories = true,
                ReturnSpecialDirectories = false
            })) {
                var function = FunctionParser.TryParse(resource, path);
                if (function != null) { ns.Functions.Add(function); continue; }
                var tag = TagParser.TryParse(resource, path);
                if (tag != null) { ns.Tags.Add(tag); continue; }
                var advancement = AdvancementParser.TryParse(resource, path);
                if (advancement != null) { ns.Advancements.Add(advancement); continue; }
                var lootTable = LootTableParser.TryParse(resource, path);
                if (lootTable != null) { ns.LootTables.Add(lootTable); continue; }
                var predicate = PredicateParser.TryParse(resource, path);
                if (predicate != null) { ns.Predicates.Add(predicate); continue; }
                var recipe = RecipeParser.TryParse(resource, path);
                if (recipe != null) { ns.Recipes.Add(recipe); continue; }
                var structure = StructureParser.TryParse(resource, path);
                if (structure != null) { ns.Structures.Add(structure); continue; }
            }
        }
    }
}
