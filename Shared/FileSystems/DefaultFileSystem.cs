using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace DataPackChecker.Shared.FileSystems {
    public class DefaultFileSystem : IFileSystem {
        public string BasePath { get; }
        public DefaultFileSystem(string path) {
            BasePath = Path.GetFullPath(path);
        }

        public bool FileExists(string file) {
            return File.Exists(EnsureAbsolute(file));
        }

        public bool DirectoryExists(string dir) {
            return Directory.Exists(EnsureAbsolute(dir));
        }

        public Stream OpenRead(string file) {
            return new FileStream(EnsureAbsolute(file), FileMode.Open, FileAccess.Read);
        }

        private string EnsureAbsolute(string file) {
            if (!Path.IsPathFullyQualified(file)) {
                return Path.Join(BasePath, file);
            } else {
                return file;
            }
        }

        public IEnumerable<string> EnumerateDirectories(string path, bool recurse = false) {
            return Directory.EnumerateDirectories(EnsureAbsolute(path), "*", new EnumerationOptions {
                RecurseSubdirectories = recurse
            }).Select(s => Path.GetRelativePath(BasePath, s));
        }

        public IEnumerable<string> EnumerateFiles(string path, bool recurse = false) {
            return Directory.EnumerateFiles(EnsureAbsolute(path), "*", new EnumerationOptions {
                RecurseSubdirectories = recurse
            }).Select(s => Path.GetRelativePath(BasePath, s));
        }

        public IEnumerable<string> ReadAllLines(string file) {
            return File.ReadAllLines(EnsureAbsolute(file));
        }
    }
}
