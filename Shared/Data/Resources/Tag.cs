using System;

namespace DataPackChecker.Shared.Data.Resources {
    public class Tag : JsonResource {
        public enum Type {
            Blocks, Entities, Fluids, Functions, Items
        }
        public Type ContentType { get; }

        public Tag(string path, string name, Type contentType) : base(path, name) {
            ContentType = contentType;
        }

        public override string GetTypeString() {
            return $"{Enum.GetName(typeof(Tag.Type), ContentType)} Tag";
        }
    }
}
