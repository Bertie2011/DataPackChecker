using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace DataPackChecker.Shared.DataPack.Resources {
    public class Tag : Resource {
        public enum Type {
            Blocks, Entities, Fluids, Functions, Items
        }
        public Type ContentType { get; }
        public JsonElement Content { get; set; }

        public Tag(string path, string name, Type contentType) : base(path, name) {
            ContentType = contentType;
        }
    }
}
