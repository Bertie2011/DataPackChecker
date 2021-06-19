using DataPackChecker.FileSystems;
using DataPackChecker.Shared.Data;
using DataPackChecker.Shared.Data.Resources;
using System.IO;
using System.Text.Json;

namespace DataPackChecker.Parsers {
    class PredicateParser : JsonParser {
        protected override string PathInNamespace => "predicates";

        protected override void CreateAndAdd(string path, string name, JsonElement json, Namespace ns) {
            ns.Predicates.Add(new Predicate(path, name, json));
        }
    }
}
