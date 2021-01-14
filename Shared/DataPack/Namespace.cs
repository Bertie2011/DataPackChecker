using DataPackChecker.Shared.DataPack.Resources;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataPackChecker.Shared.DataPack {
    public class Namespace {
        public List<Function> Functions { get; set; } = new List<Function>();
        public List<Tag> Tags { get; set; } = new List<Tag>();
        public string Name { get; }

        public Namespace(string name) {
            Name = name;
        }
    }
}
