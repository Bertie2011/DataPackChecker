using System;
using System.Collections.Generic;
using System.Text;

namespace DataPackChecker.Shared.Data.Resources.Tags {
    public class TagEntry {
        public string Identifier { get; }
        public bool Required { get; }

        public TagEntry(string identifier, bool required) {
            Identifier = identifier;
            Required = required;
        }
    }
}
