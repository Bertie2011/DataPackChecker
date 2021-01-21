using System;

namespace DataPackChecker.Shared.Data.Resources {
    public class Tag : JsonResource {
        public enum Type {
            Blocks, Entities, Fluids, Functions, Items
        }
        public Type ContentType { get; }
        public override string TypeString => $"{Enum.GetName(typeof(Tag.Type), ContentType)} Tag";
        public override string FilePath => $"tags/{TypeToString(ContentType)}/{Identifier}.json";

        public Tag(string path, string name, Type contentType) : base(path, name) {
            ContentType = contentType;
        }

        static public Type? TryGetType(string folderName) {
            switch (folderName) {
                case "blocks": return Type.Blocks;
                case "entity_types": return Type.Entities;
                case "fluids": return Type.Fluids;
                case "functions": return Type.Functions;
                case "items": return Type.Items;
                default: return null;
            }
        }

        static public string TypeToString(Type type) {
            switch (type) {
                case Type.Blocks: return "blocks";
                case Type.Entities: return "entity_types";
                case Type.Fluids: return "fluids";
                case Type.Functions: return "functions";
                case Type.Items: return "items";
                default: return null;
            }
        }
    }
}
