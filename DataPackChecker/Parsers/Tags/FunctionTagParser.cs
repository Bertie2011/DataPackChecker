﻿using DataPackChecker.Shared.Data;
using DataPackChecker.Shared.Data.Resources.Tags;
using System.IO;
using System.Text.Json;

namespace DataPackChecker.Parsers.Tags {
    static class FunctionTagParser {
        static public void FindAndParse(string nsPath, Namespace ns) {
            var searchPath = Path.Join(nsPath, "tags", "functions");
            if (!Directory.Exists(searchPath)) return;
            foreach (var resource in Directory.EnumerateFiles(searchPath, "*", new EnumerationOptions {
                RecurseSubdirectories = true,
                ReturnSpecialDirectories = false
            })) {
                if (!resource.EndsWith(".json")) continue;
                var path = Path.GetDirectoryName(Path.GetRelativePath(searchPath, resource)).Replace('\\', '/');
                var name = Path.GetFileNameWithoutExtension(resource);
                var tag = new FunctionTag(path, name);
                using FileStream fs = new FileStream(resource, FileMode.Open);
                tag.Content = JsonDocument.Parse(fs).RootElement;
                ns.Tags.FunctionTags.Add(tag);
            }
        }
    }
}
