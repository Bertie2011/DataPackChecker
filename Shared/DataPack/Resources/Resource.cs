using System;
using System.Collections.Generic;
using System.Text;

namespace DataPackChecker.Shared.DataPack.Resources {
    public class Resource {
        public string Path { get; }
        public string Name { get; }

        public Resource(string path, string name) {
            Path = path;
            Name = name;
        }
    }
}
