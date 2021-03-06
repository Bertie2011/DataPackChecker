﻿using DataPackChecker.Shared.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using DataPackChecker.Parsers.Dimensions;
using DataPackChecker.Parsers.WorldGen;
using DataPackChecker.Parsers.Tags;
using System.Threading;
using DataPackChecker.FileSystems;
using System.Reflection;
using System.Linq;

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
            DataPack pack = new DataPack(mcmeta);
            ParseNamespaces(files, pack, errors);
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
                using (Stream input = files.OpenRead(mcmeta)) {
                    mcmetaContent = JsonDocument.Parse(input);
                }
                return mcmetaContent.RootElement;
            } catch (Exception e) {
                errors.Add(e);
                return new JsonElement();
            }
        }

        // Root directory
        private static void ParseNamespaces(IFileSystem files, DataPack pack, List<Exception> errors) {
            List<(string Location, Action Action)> actions = new List<(string Location, Action Action)>();
            foreach (var namespacePath in files.EnumerateDirectories("data")) {
                var namespaceObj = new Namespace(Path.GetFileName(namespacePath));
                ParseResources(files, namespacePath, namespaceObj, actions);
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
            foreach (var parserType in Assembly.GetExecutingAssembly().GetTypes().Where(t => t.IsClass && !t.IsAbstract && typeof(IParser).IsAssignableFrom(t))) {
                var parser = (IParser)parserType.GetConstructor(Type.EmptyTypes).Invoke(null);
                runLater.Add((namespacePath, () => parser.FindAndParse(files, namespacePath, ns)));
            }
        }
    }
}
