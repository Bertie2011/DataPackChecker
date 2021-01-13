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
        private static readonly Regex FUNCTION_REGEX = new Regex(@"[\\/]data[\\/](?<namespace>\w+)[\\/]functions([\\/](?<path>.+?))?[\\/](?<name>\w+)\.mcfunction$");

        public static DataPack From(string path) {
            path = Path.TrimEndingDirectorySeparator(path);
            if (!Directory.Exists(path)) throw new ArgumentException("Specified data pack does not exist.");

            DataPack pack = new DataPack(ParseMcMeta(path));
            pack.Functions = ParseFunctions(path);
            return pack;
        }

        private static JsonElement ParseMcMeta(string path) {
            var mcmeta = Path.Join(path, "pack.mcmeta");
            if (!File.Exists(mcmeta)) throw new ArgumentException("pack.mcmeta in data pack does not exist.");

            JsonDocument mcmetaContent;
            using (FileStream input = new FileStream(mcmeta, FileMode.Open)) {
                mcmetaContent = JsonDocument.Parse(input);
            }
            return mcmetaContent.RootElement;
        }

        private static List<Function> ParseFunctions(string path) {
            List<Function> result = new List<Function>();
            foreach (var function in Directory.EnumerateFiles(path, "*", new EnumerationOptions {
                RecurseSubdirectories = true,
                ReturnSpecialDirectories = false
            })) {
                var match = FUNCTION_REGEX.Match(function, path.Length);
                if (!match.Success) continue;
                result.Add(new Function(match.Groups["namespace"].Value, match.Groups["path"].Value, match.Groups["name"].Value));
            }
            return result;
        }
    }
}
