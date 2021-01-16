﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace DataPackChecker.Shared.Data.Resources {
    public class JsonResource : Resource {
        public JsonElement Content { get; set; }
        public JsonResource(string path, string name) : base(path, name) {
        }
    }
}