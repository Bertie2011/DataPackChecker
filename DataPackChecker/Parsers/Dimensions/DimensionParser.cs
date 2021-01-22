using DataPackChecker.Shared.Data;
using DataPackChecker.Shared.Data.Resources.Dimensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace DataPackChecker.Parsers.Dimensions {
    static class DimensionParser {
        static public void FindAndParse(string nsPath, Namespace ns) {
            var searchPath = Path.Join(nsPath, "dimension");
            if (!Directory.Exists(searchPath)) return;
            foreach (var resource in Directory.EnumerateFiles(searchPath, "*", new EnumerationOptions {
                RecurseSubdirectories = true,
                ReturnSpecialDirectories = false
            })) {
                if (!resource.EndsWith(".json")) continue;
                var path = Path.GetDirectoryName(Path.GetRelativePath(searchPath, resource)).Replace('\\', '/');
                var name = Path.GetFileNameWithoutExtension(resource);
                var dimension = new Dimension(path, name);
                using FileStream fs = new FileStream(resource, FileMode.Open);
                dimension.Content = JsonDocument.Parse(fs).RootElement;
                ns.DimensionData.Dimensions.Add(dimension);
            }
        }
    }
}
