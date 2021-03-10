using CommandLine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace DataPackChecker {
    class Options {

        public enum LifeTime {
            once, await, repeat
        }

        private const char BasePathArg = 'b';
        [Option('f', "arguments-file", HelpText = "Read arguments from a file.", SetName = "Args File")]
        public string ArgsPath { get; set; }
        [Option('t', "life-time", HelpText = "Indicates what happens after finishing. Once = exit, Await = wait for keyboard input, Repeat = wait for keyboard input and repeat. Unless performing a check, repeat equals await.", Default = LifeTime.once)]
        public LifeTime Life { get; set; }
        [Option(BasePathArg, "base-path", HelpText = "Base path for the minecraft folder. Ignored if data pack path is absolute or starts with a dot. An educated guess will be made if not ignored and missing.", SetName = "Check")]
        public string BasePath { get; set; }
        [Option('w', "world", HelpText = "World name. Ignored if data pack path is absolute or starts with a dot. You will be prompted to select a world if not ignored and missing.", SetName = "Check")]
        public string World { get; set; }
        [Option('d', "data-pack", HelpText = "Path to root folder of data pack, with data folder and pack.mcmeta file inside. You will be prompted to select a data pack if missing.", SetName = "Check")]
        public string DataPackPath { get; set; }
        [Option('c', "config", HelpText = "Path to configuration file that specifies the rules. You will be prompted to select a config file in the current working directory or the datapacks folder of the world save if missing.", SetName = "Check")]
        public string ConfigPath { get; set; }
        [Option("config-help", HelpText = "Shows example contents of a configuration file.", SetName = "Config Help")]
        public bool ConfigHelp { get; set; }
        [Option('l', "rule-list", HelpText = "List all the rules.", SetName = "Rule List")]
        public bool RuleList { get; set; }
        [Option('i', "rule-info", HelpText = "Get more information about a rule like a description, configuration and examples.", SetName = "Rule Info")]
        public string RuleInfo { get; set; }

        public bool RequiresBaseAndWorld => string.IsNullOrWhiteSpace(DataPackPath) || (!Path.IsPathFullyQualified(DataPackPath) && !DataPackPath.StartsWith("."));

        public void EnsureBasePath() {
            if (!string.IsNullOrWhiteSpace(BasePath)) return;
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows)) BasePath = Path.Join(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData, Environment.SpecialFolderOption.DoNotVerify), ".minecraft");
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux)) BasePath = Path.Join(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile, Environment.SpecialFolderOption.DoNotVerify), ".minecraft");
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX)) BasePath = Path.Join(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile, Environment.SpecialFolderOption.DoNotVerify), "Library", "Application Support", "minecraft");

            ConsoleHelper.WriteLine($"No base path specified (use -{BasePathArg}), defaulting to:");
            ConsoleHelper.WriteLine(BasePath);
        }

        public void EnsureWorld() {
            if (!string.IsNullOrWhiteSpace(World)) return;
            var savesPath = Path.Join(BasePath, "saves");
            var dirs = Directory.EnumerateDirectories(savesPath).Select(d => Path.GetRelativePath(savesPath, d));
            if (dirs.Count() == 0) throw new Exception($"{savesPath} did not contain any subfolders!");
            World = ConsoleHelper.PickOne("Select a world save:", dirs);
        }

        public void EnsureDataPackPath() {
            if (!string.IsNullOrWhiteSpace(DataPackPath)) return;
            var datapacksPath = Path.Join(BasePath, "saves", World, "datapacks");
            var dirs = Directory.EnumerateDirectories(datapacksPath).Select(d => Path.GetRelativePath(datapacksPath, d));
            if (dirs.Count() == 0) throw new Exception($"{datapacksPath} did not contain any subfolders!");
            DataPackPath = ConsoleHelper.PickOne("Select a data pack:", dirs);
        }

        public void EnsureConfigPath() {
            if (!string.IsNullOrWhiteSpace(ConfigPath)) return;
            var datapacksPath = RequiresBaseAndWorld ? Path.Join(BasePath, "saves", World, "datapacks") : Path.GetDirectoryName(DataPackPath);
            var dpLabel = "In datapacks folder";
            var currentLabel = "In current folder";
            var dirs = Directory.EnumerateFiles(".", "*.json").Select(d => $"{currentLabel}: {Path.GetRelativePath(".", d)}");
            if (Path.GetFullPath(datapacksPath) != Path.GetFullPath(".")) {
                dirs = dirs.Concat(Directory.EnumerateFiles(datapacksPath, "*.json").Select(d => $"{dpLabel}: {Path.GetRelativePath(datapacksPath, d)}"));
            }
            if (dirs.Count() == 0) throw new Exception($"{datapacksPath} did not contain any subfolders!");
            var result = ConsoleHelper.PickOne("Select a configuration file:", dirs).Split(": ", 2);
            if (result[0] == dpLabel) ConfigPath = Path.Join(datapacksPath, result[1]);
            else if (result[0] == currentLabel) ConfigPath = Path.Join(".", result[1]);
        }
    }
}
