using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace DataPackChecker.Shared.Data.Resources.Tags {
    public abstract class Tag : JsonResource {
        public override JsonElement Content {
            get => base.Content;
            set {
                base.Content = value;
                UpdateEntries();
            }
        }

        /// <summary>
        /// A convenient list of entries in the tag. The Content property can be parsed too, but because
        /// an entry can also be an object it is not as straight forward.
        /// </summary>
        public List<(string Identifier, bool Required)> Entries { get; } = new List<(string Identifier, bool Required)>();

        public Tag(string path, string name) : base(path, name) {}

        private void UpdateEntries() {
            Entries.Clear();
            foreach (var entry in Content.GetProperty("values").EnumerateArray()) {
                if (entry.ValueKind == JsonValueKind.String) {
                    Entries.Add((entry.GetString(), true));
                } else if (entry.ValueKind == JsonValueKind.Object) {
                    Entries.Add((entry.GetProperty("id").GetString(), entry.GetProperty("required").GetBoolean()));
                } else {
                    throw new InvalidDataException($"Could not read {entry} in tag {Identifier}");
                }
            }
        }
    }
}
