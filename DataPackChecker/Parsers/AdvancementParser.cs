using DataPackChecker.FileSystems;
using DataPackChecker.Shared.Data;
using DataPackChecker.Shared.Data.Resources;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace DataPackChecker.Parsers {
    class AdvancementParser : JsonParser {
        protected override string PathInNamespace => "advancements";

        protected override void CreateAndAdd(string path, string name, JsonElement json, Namespace ns) {
            ns.Advancements.Add(new Advancement(path, name, json));
        }
    }
}
