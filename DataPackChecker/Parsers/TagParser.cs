using DataPackChecker.Shared.Data.Resources;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace DataPackChecker.Parsers {
    static class TagParser {
        private static readonly Regex NAMESPACE_PATH_REGEX = new Regex(@"[\\/]tags[\\/](?<type>[^\\/]+)([\\/](?<path>.+?))?[\\/](?<name>[^\\/]+)\.json$");
        static public Tag TryParse(string absPath, string nsPath) {
            var match = NAMESPACE_PATH_REGEX.Match(absPath, nsPath.Length);
            if (!match.Success) return null;
            var type = Tag.TryGetType(match.Groups["type"].Value);
            if (type == null) return null;
            var tag = new Tag(match.Groups["path"].Value.Replace('\\', '/'), match.Groups["name"].Value, type.Value);
            using FileStream fs = new FileStream(absPath, FileMode.Open);
            tag.Content = JsonDocument.Parse(fs).RootElement;
            return tag;
        }
    }
}
