using System;
using System.Collections.Generic;
using System.Text;

namespace DataPackChecker.Shared.DataPack {
    public class Namespace {
        public List<Function> Functions { get; set; }
        public string Name { get; }

        public Namespace(string name) {
            Name = name;
        }
    }
}
