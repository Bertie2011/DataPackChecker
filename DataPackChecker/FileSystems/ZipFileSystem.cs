using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading;

namespace DataPackChecker.FileSystems {
    public class ZipFileSystem : IFileSystem {
        public string BasePath { get; }
        private ZipArchive Archive { get; }

        public ZipFileSystem(string basePath) {
            BasePath = basePath;
            Archive = new ZipArchive(new MemoryStream(File.ReadAllBytes(basePath)), ZipArchiveMode.Read);
        }

        public bool FileExists(string file) {
            file = ZipPath(file);
            return Archive.Entries.Any(e => e.FullName == file);
        }

        public Stream OpenRead(string file) {
            return new ZipStream(BasePath, ZipPath(file));
        }

        public IEnumerable<string> EnumerateDirectories(string path, bool recurse = false) {
            HashSet<string> dirs = new HashSet<string>();
            HashSet<string> visited = new HashSet<string>();
            path = Path.TrimEndingDirectorySeparator(ZipPath(path));
            foreach (var entry in EnumerateFiles(path, true)) {
                for (var dir = Path.GetDirectoryName(entry); dir != null; dir = Path.GetDirectoryName(dir)) {
                    if (recurse || Path.GetDirectoryName(dir) == path) dirs.Add(dir);
                    if (!visited.Add(dir)) break;
                }
            }
            return dirs.Select(d => SystemPath(d));
        }

        public IEnumerable<string> EnumerateFiles(string path, bool recurse = false) {
            path = Path.TrimEndingDirectorySeparator(ZipPath(path));
            var pathSlash = path + '/';
            return Archive.Entries.Where(e => e.FullName.StartsWith(pathSlash)
                    && (recurse || Path.GetDirectoryName(e.FullName) == path))
                .Select(e => SystemPath(e.FullName));
        }

        public bool DirectoryExists(string dir) {
            if (dir == string.Empty) return true;
            dir = ZipPath(dir);
            dir = Path.EndsInDirectorySeparator(dir) ? dir : dir + '/';
            return Archive.Entries.Any(e => e.FullName.StartsWith(dir));
        }

        public IEnumerable<string> ReadAllLines(string file) {
            file = ZipPath(file);
            using Stream s = Archive.GetEntry(file).Open();
            using StreamReader sr = new StreamReader(s);
            return sr.ReadToEnd().Split(new string[] { "\r", "\n", "\r\n" }, StringSplitOptions.None);
        }

        private string SystemPath(string path) {
            return path.Replace('/', Path.DirectorySeparatorChar);
        }

        private string ZipPath(string path) {
            return path.Replace(Path.DirectorySeparatorChar, '/');
        }
    }
}
