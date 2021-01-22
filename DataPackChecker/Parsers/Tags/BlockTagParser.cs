using DataPackChecker.Shared.Data;
using DataPackChecker.Shared.Data.Resources.Tags;
using System.IO;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace DataPackChecker.Parsers.Tags {
    static class BlockTagParser {
        static public void FindAndParse(string nsPath, Namespace ns) {
            var searchPath = Path.Join(nsPath, "tags", "blocks");
            if (!Directory.Exists(searchPath)) return;
            foreach (var resource in Directory.EnumerateFiles(searchPath, "*", new EnumerationOptions {
                RecurseSubdirectories = true,
                ReturnSpecialDirectories = false
            })) {
                if (!resource.EndsWith(".json")) continue;
                var path = Path.GetDirectoryName(Path.GetRelativePath(searchPath, resource)).Replace('\\', '/');
                var name = Path.GetFileNameWithoutExtension(resource);
                var tag = new BlockTag(path, name);
                using FileStream fs = new FileStream(resource, FileMode.Open);
                tag.Content = JsonDocument.Parse(fs).RootElement;
                ns.Tags.BlockTags.Add(tag);
            }
        }
    }
}
