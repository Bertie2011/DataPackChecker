using DataPackChecker.Shared.Data;
using DataPackChecker.Shared.Data.Resources.WorldGen;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace DataPackChecker.Parsers.WorldGen {
    static class TemplatePoolParser {
        static public void FindAndParse(string nsPath, Namespace ns) {
            var searchPath = Path.Join(nsPath, "worldgen", "template_pool");
            if (!Directory.Exists(searchPath)) return;
            foreach (var resource in Directory.EnumerateFiles(searchPath, "*", new EnumerationOptions {
                RecurseSubdirectories = true,
                ReturnSpecialDirectories = false
            })) {
                if (!resource.EndsWith(".json")) continue;
                var path = Path.GetDirectoryName(Path.GetRelativePath(searchPath, resource)).Replace('\\', '/');
                var name = Path.GetFileNameWithoutExtension(resource);
                var worldGenElement = new TemplatePool(path, name);
                using FileStream fs = new FileStream(resource, FileMode.Open);
                worldGenElement.Content = JsonDocument.Parse(fs).RootElement;
                ns.WorldGenData.TemplatePools.Add(worldGenElement);
            }
        }
    }
}
