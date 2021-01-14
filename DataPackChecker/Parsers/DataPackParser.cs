using DataPackChecker.Shared.DataPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Linq;
using System.Text.RegularExpressions;
using DataPackChecker.Shared.DataPack.Resources;
using DataPackChecker.Parsers.Dimensions;
using DataPackChecker.Parsers.WorldGen;

namespace DataPackChecker.Parsers {
    static class DataPackParser {
        // Root directory
        public static DataPack From(string path) {
            path = Path.TrimEndingDirectorySeparator(path);
            if (!Directory.Exists(path)) throw new ArgumentException("Specified data pack does not exist.");

            DataPack pack = new DataPack(ParseMcMeta(path));
            ParseNamespaces(path, pack);
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
                var dimension = DimensionParser.TryParse(resource, path);
                if (dimension != null) { ns.DimensionData.Dimensions.Add(dimension); continue; }
                var dimensionType = DimensionTypeParser.TryParse(resource, path);
                if (dimensionType != null) { ns.DimensionData.DimensionTypes.Add(dimensionType); continue; }
                var biome = BiomeParser.TryParse(resource, path);
                if (biome != null) { ns.WorldGenData.Biomes.Add(biome); continue; }
                var configuredCarver = ConfiguredCarverParser.TryParse(resource, path);
                if (configuredCarver != null) { ns.WorldGenData.ConfiguredCarvers.Add(configuredCarver); continue; }
                var configuredFeature = ConfiguredFeatureParser.TryParse(resource, path);
                if (configuredFeature != null) { ns.WorldGenData.ConfiguredFeatures.Add(configuredFeature); continue; }
                var configuredStructureFeature = ConfiguredStructureFeatureParser.TryParse(resource, path);
                if (configuredStructureFeature != null) { ns.WorldGenData.ConfiguredStructureFeatures.Add(configuredStructureFeature); continue; }
                var configuredSurfaceBuilder = ConfiguredSurfaceBuilderParser.TryParse(resource, path);
                if (configuredSurfaceBuilder != null) { ns.WorldGenData.ConfiguredSurfaceBuilders.Add(configuredSurfaceBuilder); continue; }
                var noiseSettings = NoiseSettingsParser.TryParse(resource, path);
                if (noiseSettings != null) { ns.WorldGenData.NoiseSettings.Add(noiseSettings); continue; }
                var processorList = ProcessorListParser.TryParse(resource, path);
                if (processorList != null) { ns.WorldGenData.ProcessorLists.Add(processorList); continue; }
                var templatePool = TemplatePoolParser.TryParse(resource, path);
                if (templatePool != null) { ns.WorldGenData.TemplatePools.Add(templatePool); continue; }
            }
        }
    }
}
