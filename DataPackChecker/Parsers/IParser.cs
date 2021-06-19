using DataPackChecker.FileSystems;
using DataPackChecker.Shared.Data;

namespace DataPackChecker.Parsers {
    interface IParser {
        void FindAndParse(IFileSystem files, string nsPath, Namespace ns);
    }
}
