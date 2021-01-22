using System;

namespace DataPackChecker.Shared.Data.Resources.Tags {
    public abstract class Tag : JsonResource {
        public Tag(string path, string name) : base(path, name) {}
    }
}
