using DataPackChecker.Shared.Data;
using DataPackChecker.Shared.Data.Resources;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DataPackChecker.Shared {
    public class Output {
        private class JsonElementConverter : JsonConverter<JsonElement> {
            public override JsonElement Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException();
            public override void Write(Utf8JsonWriter writer, JsonElement value, JsonSerializerOptions options) => Write(writer, value, options, false);
            public void Write(Utf8JsonWriter writer, JsonElement value, JsonSerializerOptions options, bool tiny, string propertyName = null) {
                if (propertyName != null) writer.WritePropertyName(propertyName);
                if (value.ValueKind == JsonValueKind.Array) {
                    writer.WriteStartArray();
                    if (tiny) writer.WriteStringValue("...");
                    else foreach (var element in value.EnumerateArray()) Write(writer, element, options, true);
                    writer.WriteEndArray();
                } else if (value.ValueKind == JsonValueKind.Object) {
                    writer.WriteStartObject();
                    if (tiny) writer.WriteString("...", "...");
                    else foreach (var property in value.EnumerateObject()) Write(writer, property.Value, options, true, property.Name);
                    writer.WriteEndObject();
                } else if (value.ValueKind == JsonValueKind.False) writer.WriteBooleanValue(false);
                else if (value.ValueKind == JsonValueKind.True) writer.WriteBooleanValue(true);
                else if (value.ValueKind == JsonValueKind.Number) writer.WriteNumberValue(value.GetDouble());
                else if (value.ValueKind == JsonValueKind.String) writer.WriteStringValue(value.GetString());
                else if (value.ValueKind == JsonValueKind.Null) writer.WriteNullValue();
            }
        }

        private static JsonSerializerOptions JsonOptions { get; } = new JsonSerializerOptions {
            WriteIndented = false,
            Converters = { new JsonElementConverter() }
        };

        private CheckerRule Rule { get; }
        private List<(string Loc, string Msg)> Errors { get; } = new List<(string Loc, string Msg)>();

        public Output(CheckerRule rule) {
            Rule = rule;
        }

        /// <summary>
        /// This method does not provide the creator with enough information, please use a more specific method.<br/><br/>
        /// Output header format:<br/>
        /// Namespace {ns.Name}
        /// </summary>
        public void Error(Namespace ns, string message) {
            Errors.Add(($"Namespace {ns.Name}", message));
        }

        /// <summary>
        /// Output header format:<br/>
        /// {resource type} {ns.Name}:{r.Path}/{r.Name}
        /// </summary>
        public void Error(Namespace ns, Resource r, string message) {
            Errors.Add(($"{GetResourceIdentifier(ns, r)}", message));
        }

        /// <summary>
        /// Output header format:<br/>
        /// {type} Tag {ns.Name}:{t.Path}/{t.Name} - {item}
        /// </summary>
        public void Error(Namespace ns, Tag t, string item, string message) {
            Errors.Add(($"{GetResourceIdentifier(ns, t)} - {item}", message));
        }

        /// <summary>
        /// Output header format:<br/>
        /// {resource type} {ns.Name}:{r.Path}/{r.Name} - {element}
        /// </summary>
        public void Error(Namespace ns, JsonResource r, JsonElement element, string message) {
            var json = JsonSerializer.Serialize(element, JsonOptions);
            Errors.Add(($"{GetResourceIdentifier(ns, r)} - {json}", message));
        }

        /// <summary>
        /// This method does not provide the creator with enough information, please use a more specific method.
        /// </summary>
        public void Error(Exception e) {
            StringBuilder msg = new StringBuilder();
            int indent = 0;
            while (e != null) {
                for (int i = 0; i < indent; i++) msg.Append('\t');
                msg.Append(e.Message);
                if (e.InnerException != null) msg.Append('\n');
                indent++;
                e = e.InnerException;
            }
            Errors.Add(($"Exception while running", msg.ToString()));
        }

        /// <summary>
        /// Output header format:<br/>
        /// Function {ns.Name}:{f.Path}/{f.Name} - Line {c.Line}: {c.Raw}...
        /// </summary>
        public void Error(Namespace ns, Function f, Command c, string message) {
            Errors.Add(($"{GetResourceIdentifier(ns, f)} - Line {c.Line}: {c.Raw.Substring(0, Math.Min(30, c.Raw.Length))}...", message));
        }

        public string GetResourcePath(Namespace ns, Resource r) {
            return ns.Name + ':' + r.FullPath;
        }

        /// <summary>
        /// Returns: {r.GetTypeString()} {GetResourcePath(ns, r)}
        /// </summary>
        public string GetResourceIdentifier(Namespace ns, Resource r) {
            return $"{r.TypeString} {GetResourcePath(ns, r)}";
        }

        public void Print() {
            Console.ForegroundColor = Errors.Count > 0 ? ConsoleColor.Red : ConsoleColor.Green;
            Console.Write(Rule.Title);
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write(" - ");
            Console.WriteLine(Rule.GetType().FullName);
            foreach (var error in Errors) {
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(error.Loc);
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine(error.Msg);
            }
            if (Errors.Count > 0) Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
