﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace DataPackChecker.Shared.DataPack.Resources {
    public class LootTable : Resource {
        public JsonElement Content { get; set; }
        public LootTable(string path, string name) : base(path, name) {}
    }
}
