﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DataPackChecker.Shared.DataPack {
    public class Function {
        public string Path { get; }
        public string Name { get; }
        public List<Command> Commands { get; set; }

        public Function(string path, string name) {
            Path = path;
            Name = name;
        }
    }
}
