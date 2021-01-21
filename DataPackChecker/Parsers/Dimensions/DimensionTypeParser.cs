using DataPackChecker.Shared.Data.Resources.Dimensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace DataPackChecker.Parsers.Dimensions {
    static class DimensionTypeParser {
        private static readonly Regex NamespacePathRegex = new Regex(@"[\\/]dimension_type([\\/](?<path>.+?))?[\\/](?<name>[^\\/]+)\.json$");
        static public DimensionType TryParse(string absPath, string nsPath) {
            var match = NamespacePathRegex.Match(absPath, nsPath.Length);
            if (!match.Success) return null;
            var dimensionType = new DimensionType(match.Groups["path"].Value.Replace('\\', '/'), match.Groups["name"].Value);
            using FileStream fs = new FileStream(absPath, FileMode.Open);
            dimensionType.Content = JsonDocument.Parse(fs).RootElement;
            return dimensionType;
        }
    }
}
