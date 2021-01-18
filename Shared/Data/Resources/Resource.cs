using System;
using System.Collections.Generic;
using System.Text;

namespace DataPackChecker.Shared.Data.Resources {
    public class Resource {
        public string Path { get; }
        public string Name { get; }
        virtual public string TypeString => GetType().Name;
        public string FullPath => string.IsNullOrWhiteSpace(Path) ? Name : Path + '/' + Name;

        public Resource(string path, string name) {
            Path = path;
            Name = name;
        }
    }
}
