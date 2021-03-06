﻿using System.Text.Json;

namespace DataPackChecker.Shared.Data.Resources.Tags {
    public class EntityTag : Tag {
        public override string FilePath => $"tags/entity_types/{Identifier}.json";
        public override string TypeString => "Tag (Entities)";
        public EntityTag(string path, string name, JsonElement content) : base(path, name, content) {}
    }
}
