using DataPackChecker.Shared.Data.Resources;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace DataPackChecker.Parsers {
    static public class FunctionParser {
        private static readonly Regex NamespacePathRegex = new Regex(@"[\\/]functions([\\/](?<path>.+?))?[\\/](?<name>[^\\/]+)\.mcfunction$");
        static public Function TryParse(string absPath, string nsPath) {
            var match = NamespacePathRegex.Match(absPath, nsPath.Length);
            if (!match.Success) return null;
            var function = new Function(match.Groups["path"].Value.Replace('\\', '/'), match.Groups["name"].Value);
            function.Commands = File.ReadAllLines(absPath).Select((c, i) => new Command(i + 1, c)).ToList();
            return function;
        }
    }
}
