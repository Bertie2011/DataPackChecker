﻿using DataPackChecker.FileSystems;
using DataPackChecker.Shared.Data;
using DataPackChecker.Shared.Data.Resources.Tags;
using System.IO;
using System.Text.Json;

namespace DataPackChecker.Parsers.Tags {
    class EntityTagParser : IParser {
        public void FindAndParse(IFileSystem files, string nsPath, Namespace ns) {
            var searchPath = Path.Join(nsPath, "tags", "entity_types");
            if (!files.DirectoryExists(searchPath)) return;
            foreach (var resource in files.EnumerateFiles(searchPath, true)) {
                if (!resource.EndsWith(".json")) continue;
                var path = Path.GetDirectoryName(Path.GetRelativePath(searchPath, resource)).Replace('\\', '/');
                var name = Path.GetFileNameWithoutExtension(resource);
                var tag = new EntityTag(path, name);
                using Stream fs = files.OpenRead(resource);
                tag.Content = JsonDocument.Parse(fs).RootElement;
                ns.TagData.EntityTags.Add(tag);
            }
        }
    }
}
