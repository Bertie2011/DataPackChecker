using DataPackChecker.Shared.Data.Resources;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace DataPackChecker.Parsers {
    static class StructureParser {
        private static readonly Regex NamespacePathRegex = new Regex(@"[\\/]structures([\\/](?<path>.+?))?[\\/](?<name>[^\\/]+)\.nbt$");
        static public Structure TryParse(string absPath, string nsPath) {
            var match = NamespacePathRegex.Match(absPath, nsPath.Length);
            if (!match.Success) return null;
            return new Structure(match.Groups["path"].Value.Replace('\\', '/'), match.Groups["name"].Value);
        }
    }
}
