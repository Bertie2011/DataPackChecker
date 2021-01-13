using DataPackChecker.Shared.DataPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Linq;
using System.Text.RegularExpressions;

namespace DataPackChecker {
    class DataPackParser {
        private static readonly Regex FUNCTION_REGEX = new Regex(@"[\\/]functions([\\/](?<path>.+?))?[\\/](?<name>\w+)\.mcfunction$");

        // Root directory
        public static DataPack From(string path) {
            path = Path.TrimEndingDirectorySeparator(path);
            if (!Directory.Exists(path)) throw new ArgumentException("Specified data pack does not exist.");

            DataPack pack = new DataPack(ParseMcMeta(path));
            pack.Namespaces = ParseNamespaces(path);
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
        private static List<Namespace> ParseNamespaces(string path) {
            List<Namespace> result = new List<Namespace>();
            var dataPath = Path.Join(path, "data");
            foreach (var namespacePath in Directory.EnumerateDirectories(dataPath)) {
                var namespaceObj = new Namespace(Path.GetFileName(namespacePath));
                namespaceObj.Functions = ParseFunctions(namespacePath);
                result.Add(namespaceObj);
            }
            return result;

        }

        // Namespace directory
        private static List<Function> ParseFunctions(string path) {
            List<Function> result = new List<Function>();
            foreach (var function in Directory.EnumerateFiles(path, "*", new EnumerationOptions {
                RecurseSubdirectories = true,
                ReturnSpecialDirectories = false
            })) {
                var match = FUNCTION_REGEX.Match(function, path.Length);
                if (!match.Success) continue;
                var functionObj = new Function(match.Groups["path"].Value.Replace('\\', '/'), match.Groups["name"].Value);
                result.Add(functionObj);
                functionObj.Commands = File.ReadAllLines(function).Select(c => new Command(c)).ToList();
            }
            return result;
        }
    }
}
