using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace DataPackChecker.Shared.Data.Resources {
    public abstract class JsonResource : Resource {
        virtual public JsonElement Content { get; set; }
        public JsonResource(string path, string name) : base(path, name) {
        }
    }
}
