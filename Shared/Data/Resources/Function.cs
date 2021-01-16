﻿using System.Collections.Generic;

namespace DataPackChecker.Shared.Data.Resources {
    public class Function : Resource {
        public List<Command> Commands { get; set; } = new List<Command>();

        public Function(string path, string name) : base(path, name) { }
    }
}