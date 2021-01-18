using System.Text.Json;

namespace DataPackChecker.Configuration {
    class ConfigEntry {
        public string Rule { get; set; }
        public JsonElement? Config { get; set; }
    }
}
