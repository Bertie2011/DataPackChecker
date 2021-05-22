﻿using DataPackChecker.Shared.Data;
using DataPackChecker.Shared.Data.Resources.Tags;
using DataPackChecker.Shared.FileSystems;
using System.IO;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace DataPackChecker.Parsers.Tags {
    static class GameEventTagParser {
        static public void FindAndParse(IFileSystem files, string nsPath, Namespace ns) {
            var searchPath = Path.Join(nsPath, "tags", "game_events");
            if (!files.DirectoryExists(searchPath)) return;
            foreach (var resource in files.EnumerateFiles(searchPath, true)) {
                if (!resource.EndsWith(".json")) continue;
                var path = Path.GetDirectoryName(Path.GetRelativePath(searchPath, resource)).Replace('\\', '/');
                var name = Path.GetFileNameWithoutExtension(resource);
                var tag = new GameEventTag(path, name);
                using Stream fs = files.OpenRead(resource);
                tag.Content = JsonDocument.Parse(fs).RootElement;
                ns.TagData.GameEventTags.Add(tag);
            }
        }
    }
}