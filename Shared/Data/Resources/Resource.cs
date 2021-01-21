using DataPackChecker.Shared.Collections;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataPackChecker.Shared.Data.Resources {
    public abstract class Resource : HasKey<string> {
        public string IdentifierPath { get; }
        public string Name { get; }
        virtual public string TypeString => GetType().Name;
        public string Identifier => string.IsNullOrWhiteSpace(IdentifierPath) ? Name : IdentifierPath + '/' + Name;
        /// <summary>
        /// The file path relative to the namespace folder.
        /// </summary>
        abstract public string FilePath { get; }
        /// <summary>
        /// Same as Identifier
        /// </summary>
        public string Key => Identifier;

        public Resource(string path, string name) {
            IdentifierPath = path;
            Name = name;
        }
        public override string ToString() {
            return Identifier;
        }
    }
}
