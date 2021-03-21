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
using DataPackChecker.Shared.FileSystems;

namespace DataPackChecker.Parsers {
    static class DataPackParser {
        // Root directory
        public static (DataPack DataPack, List<Exception> Errors) From(string path) {
            path = Path.TrimEndingDirectorySeparator(path);
            List<Exception> errors = new List<Exception>();
            IFileSystem files;
            if (Directory.Exists(path)) {
                files = new DefaultFileSystem(path);
            } else if (path.EndsWith(".zip") && File.Exists(path)) {
                files = new ZipFileSystem(path);
            } else {
                errors.Add(new ArgumentException("Specified data pack does not exist."));
                return (null, errors);
            }

            var mcmeta = ParseMcMeta(files, errors);
            if (errors.Count > 0) return (null, errors);
            DataPack pack = new DataPack(files, mcmeta);
            ParseNamespaces(pack, errors);
            if (errors.Count > 0) return (pack, errors);
            pack.RebuildReferences();
            return (pack, errors);
        }

        // Root directory
        private static JsonElement ParseMcMeta(IFileSystem files, List<Exception> errors) {
            var mcmeta = "pack.mcmeta";
            if (!files.FileExists(mcmeta)) {
                errors.Add(new ArgumentException("pack.mcmeta in data pack does not exist."));
                return new JsonElement();
            }

            try {
                JsonDocument mcmetaContent;
                using (Stream input = files.Open(mcmeta)) {
                    mcmetaContent = JsonDocument.Parse(input);
                }
                return mcmetaContent.RootElement;
            } catch (Exception e) {
                errors.Add(e);
                return new JsonElement();
            }
        }

        // Root directory
        private static void ParseNamespaces(DataPack pack, List<Exception> errors) {
            List<(string Location, Action Action)> actions = new List<(string Location, Action Action)>();
            foreach (var namespacePath in pack.Files.EnumerateDirectories("data")) {
                var namespaceObj = new Namespace(Path.GetFileName(namespacePath));
                ParseResources(pack.Files, namespacePath, namespaceObj, actions);
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

        // namespacePath = relative path to namespace folder, with the data pack as root.
        private static void ParseResources(IFileSystem files, string namespacePath, Namespace ns, List<(string, Action)> runLater) {
            runLater.Add((namespacePath, () => DimensionParser.FindAndParse(files, namespacePath, ns)));
            runLater.Add((namespacePath, () => DimensionTypeParser.FindAndParse(files, namespacePath, ns)));
            runLater.Add((namespacePath, () => BlockTagParser.FindAndParse(files, namespacePath, ns)));
            runLater.Add((namespacePath, () => EntityTagParser.FindAndParse(files, namespacePath, ns)));
            runLater.Add((namespacePath, () => FluidTagParser.FindAndParse(files, namespacePath, ns)));
            runLater.Add((namespacePath, () => FunctionTagParser.FindAndParse(files, namespacePath, ns)));
            runLater.Add((namespacePath, () => ItemsTagParser.FindAndParse(files, namespacePath, ns)));
            runLater.Add((namespacePath, () => BiomeParser.FindAndParse(files, namespacePath, ns)));
            runLater.Add((namespacePath, () => ConfiguredCarverParser.FindAndParse(files, namespacePath, ns)));
            runLater.Add((namespacePath, () => ConfiguredFeatureParser.FindAndParse(files, namespacePath, ns)));
            runLater.Add((namespacePath, () => ConfiguredStructureFeatureParser.FindAndParse(files, namespacePath, ns)));
            runLater.Add((namespacePath, () => ConfiguredSurfaceBuilderParser.FindAndParse(files, namespacePath, ns)));
            runLater.Add((namespacePath, () => NoiseSettingsParser.FindAndParse(files, namespacePath, ns)));
            runLater.Add((namespacePath, () => ProcessorListParser.FindAndParse(files, namespacePath, ns)));
            runLater.Add((namespacePath, () => TemplatePoolParser.FindAndParse(files, namespacePath, ns)));
            runLater.Add((namespacePath, () => AdvancementParser.FindAndParse(files, namespacePath, ns)));
            runLater.Add((namespacePath, () => FunctionParser.FindAndParse(files, namespacePath, ns)));
            runLater.Add((namespacePath, () => LootTableParser.FindAndParse(files, namespacePath, ns)));
            runLater.Add((namespacePath, () => PredicateParser.FindAndParse(files, namespacePath, ns)));
            runLater.Add((namespacePath, () => RecipeParser.FindAndParse(files, namespacePath, ns)));
            runLater.Add((namespacePath, () => StructureParser.FindAndParse(files, namespacePath, ns)));
        }
    }
}
