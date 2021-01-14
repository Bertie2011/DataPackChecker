﻿using DataPackChecker.Shared.DataPack.Resources;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace DataPackChecker.Parsers {
    static public class FunctionParser {
        private static readonly Regex NAMESPACE_PATH_REGEX = new Regex(@"[\\/]functions([\\/](?<path>.+?))?[\\/](?<name>\w+)\.mcfunction$");
        static public Function TryParse(string absPath, string nsPath) {
            var match = NAMESPACE_PATH_REGEX.Match(absPath, nsPath.Length);
            if (!match.Success) return null;
            var function = new Function(match.Groups["path"].Value.Replace('\\', '/'), match.Groups["name"].Value);
            function.Commands = File.ReadAllLines(absPath).Select(c => new Command(c)).ToList();
            return function;
        }
    }
}