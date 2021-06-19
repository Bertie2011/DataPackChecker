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
    class ItemModifierParser : JsonParser {
        protected override string PathInNamespace => "item_modifiers";

        protected override void CreateAndAdd(string path, string name, JsonElement json, Namespace ns) {
            ns.ItemModifiers.Add(new ItemModifier(path, name, json));
        }
    }
}
