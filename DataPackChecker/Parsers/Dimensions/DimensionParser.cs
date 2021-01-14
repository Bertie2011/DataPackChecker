using DataPackChecker.Shared.DataPack.Resources.Dimensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace DataPackChecker.Parsers.Dimensions {
    static class DimensionParser {
        private static readonly Regex NAMESPACE_PATH_REGEX = new Regex(@"[\\/]dimension([\\/](?<path>.+?))?[\\/](?<name>[^\\/]+)\.json$");
        static public Dimension TryParse(string absPath, string nsPath) {
            var match = NAMESPACE_PATH_REGEX.Match(absPath, nsPath.Length);
            if (!match.Success) return null;
            var dimension = new Dimension(match.Groups["path"].Value.Replace('\\', '/'), match.Groups["name"].Value);
            using FileStream fs = new FileStream(absPath, FileMode.Open);
            dimension.Content = JsonDocument.Parse(fs).RootElement;
            return dimension;
        }
    }
}
