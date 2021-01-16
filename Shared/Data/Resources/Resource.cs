using System;
using System.Collections.Generic;
using System.Text;

namespace DataPackChecker.Shared.Data.Resources {
    public class Resource {
        public string Path { get; }
        public string Name { get; }

        public Resource(string path, string name) {
            Path = path;
            Name = name;
        }

        virtual public string GetTypeString() {
            return GetType().Name;
        }
    }
}
