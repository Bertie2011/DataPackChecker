using System.Collections.Generic;
using System.IO;

namespace DataPackChecker.Shared.FileSystems {
    public interface IFileSystem {
        string BasePath { get; }
        bool FileExists(string file);
        Stream Open(string file);
        IEnumerable<string> EnumerateDirectories(string path, bool recurse = false);
        IEnumerable<string> EnumerateFiles(string path, bool recurse = false);
        bool DirectoryExists(string dir);
        IEnumerable<string> ReadAllLines(string resource);
    }
}
