using System.Collections.Generic;
using System.IO;

namespace DataPackChecker.FileSystems {
    /// <summary>
    /// PATH CONVENTIONS:
    /// - Paths are relative to the BasePath
    /// - Paths do not start with a slash.
    /// - Path separators are equal to <see cref="Path.DirectorySeparatorChar"/>
    /// </summary>
    public interface IFileSystem {
        string BasePath { get; }
        bool FileExists(string file);
        Stream OpenRead(string file);
        IEnumerable<string> EnumerateDirectories(string path, bool recurse = false);
        IEnumerable<string> EnumerateFiles(string path, bool recurse = false);
        bool DirectoryExists(string dir);
        IEnumerable<string> ReadAllLines(string file);
    }
}
