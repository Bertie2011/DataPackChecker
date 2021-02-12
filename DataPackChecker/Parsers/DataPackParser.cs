using DataPackChecker.Shared.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Linq;
using System.Text.RegularExpressions;
using DataPackChecker.Shared.Data.Resources;
using DataPackChecker.Parsers.Dimensions;
using DataPackChecker.Parsers.WorldGen;
using DataPackChecker.Parsers.Tags;
using System.Threading;

namespace DataPackChecker.Parsers {
    static class DataPackParser {
        // Root directory
        public static (DataPack DataPack, List<Exception> Errors) From(string path) {
            path = Path.TrimEndingDirectorySeparator(path);
            List<Exception> errors = new List<Exception>();
            if (!Directory.Exists(path)) {
                errors.Add(new ArgumentException("Specified data pack does not exist."));
                return (null, errors);
            }

            var mcmeta = ParseMcMeta(path, errors);
            if (errors.Count > 0) return (null, errors);
            DataPack pack = new DataPack(path, mcmeta);
            ParseNamespaces(path, pack, errors);
            if (errors.Count > 0) return (pack, errors);
            pack.RebuildReferences();
            return (pack, errors);
        }

        // Root directory
        private static JsonElement ParseMcMeta(string path, List<Exception> errors) {
            var mcmeta = Path.Join(path, "pack.mcmeta");
            if (!File.Exists(mcmeta)) {
                errors.Add(new ArgumentException("pack.mcmeta in data pack does not exist."));
                return new JsonElement();
            }

            try {
                JsonDocument mcmetaContent;
                using (FileStream input = new FileStream(mcmeta, FileMode.Open)) {
                    mcmetaContent = JsonDocument.Parse(input);
                }
                return mcmetaContent.RootElement;
            } catch (Exception e) {
                errors.Add(e);
                return new JsonElement();
            }
        }

        // Root directory
        private static void ParseNamespaces(string path, DataPack pack, List<Exception> errors) {
            var dataPath = Path.Join(path, "data");
            List<(string Location, Action Action)> actions = new List<(string Location, Action Action)>();
            foreach (var namespacePath in Directory.EnumerateDirectories(dataPath)) {
                var namespaceObj = new Namespace(Path.GetFileName(namespacePath));
                ParseResources(namespacePath, namespaceObj, actions);
                pack.Namespaces.Add(namespaceObj);
            }

            // Actually run the actions.
            var counter = new CountdownEvent(actions.Count);
            foreach (var entry in actions) ThreadPool.QueueUserWorkItem(s => {
                try {
                    entry.Action();
                } catch (Exception e) {
                    errors.Add(new InvalidDataException("Exception while parsing " + entry.Location, e));
                }
                counter.Signal();
            });
            counter.Wait();
        }

        // Namespace directory
        private static void ParseResources(string path, Namespace ns, List<(string, Action)> runLater) {
            runLater.Add((path, () => DimensionParser.FindAndParse(path, ns)));
            runLater.Add((path, () => DimensionTypeParser.FindAndParse(path, ns)));
            runLater.Add((path, () => BlockTagParser.FindAndParse(path, ns)));
            runLater.Add((path, () => EntityTagParser.FindAndParse(path, ns)));
            runLater.Add((path, () => FluidTagParser.FindAndParse(path, ns)));
            runLater.Add((path, () => FunctionTagParser.FindAndParse(path, ns)));
            runLater.Add((path, () => ItemsTagParser.FindAndParse(path, ns)));
            runLater.Add((path, () => BiomeParser.FindAndParse(path, ns)));
            runLater.Add((path, () => ConfiguredCarverParser.FindAndParse(path, ns)));
            runLater.Add((path, () => ConfiguredFeatureParser.FindAndParse(path, ns)));
            runLater.Add((path, () => ConfiguredStructureFeatureParser.FindAndParse(path, ns)));
            runLater.Add((path, () => ConfiguredSurfaceBuilderParser.FindAndParse(path, ns)));
            runLater.Add((path, () => NoiseSettingsParser.FindAndParse(path, ns)));
            runLater.Add((path, () => ProcessorListParser.FindAndParse(path, ns)));
            runLater.Add((path, () => TemplatePoolParser.FindAndParse(path, ns)));
            runLater.Add((path, () => AdvancementParser.FindAndParse(path, ns)));
            runLater.Add((path, () => FunctionParser.FindAndParse(path, ns)));
            runLater.Add((path, () => LootTableParser.FindAndParse(path, ns)));
            runLater.Add((path, () => PredicateParser.FindAndParse(path, ns)));
            runLater.Add((path, () => RecipeParser.FindAndParse(path, ns)));
            runLater.Add((path, () => StructureParser.FindAndParse(path, ns)));
        }
    }
}
