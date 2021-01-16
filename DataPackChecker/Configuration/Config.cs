using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace DataPackChecker.Configuration {
    class Config {
        static public string ExampleContents => @"[
    {
        ""rule"": ""com.namespace.RuleClass (= identifier)"",
        ""config"": {
            ""first"": ""If a rule has no config, you can omit the config key."",
            ""second"": ""The JSON structure is rule specific, use the rule info option for more information.""
        }
    }
]";

        public List<ConfigEntry> Entries { get; } = new List<ConfigEntry>();

        static public (Config Config, List<Exception> Errors) From(string path) {
            Config config = new Config();
            List<Exception> errors = new List<Exception>();

            try {
                config.Entries.AddRange(JsonSerializer.Deserialize<List<ConfigEntry>>(File.ReadAllText(path), new JsonSerializerOptions {
                    PropertyNameCaseInsensitive = true
                }));
                foreach (var entry in config.Entries) {
                    if (string.IsNullOrWhiteSpace(entry.Rule)) errors.Add(new InvalidDataException("Rule found without \"rule\" key!"));
                }
            } catch (Exception e) {
                errors.Add(e);
            }

            return (config, errors);
        }
    }
}
