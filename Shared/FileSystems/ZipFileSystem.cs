using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DataPackChecker.Shared.FileSystems {
    public class ZipFileSystem : IFileSystem {
        public string BasePath { get; }
        public ZipFileSystem(string basePath) {
            BasePath = basePath;
        }

        public bool FileExists(string file) {
            throw new NotImplementedException();
        }

        public Stream Open(string file) {
            throw new NotImplementedException();
        }

        public IEnumerable<string> EnumerateDirectories(string path) {
            throw new NotImplementedException();
        }

        public IEnumerable<string> EnumerateDirectories(string path, bool recurse = false) {
            throw new NotImplementedException();
        }

        public IEnumerable<string> EnumerateFiles(string path, bool recurse = false) {
            throw new NotImplementedException();
        }

        public bool DirectoryExists(string dir) {
            throw new NotImplementedException();
        }

        public IEnumerable<string> ReadAllLines(string resource) {
            throw new NotImplementedException();
        }
    }
}
