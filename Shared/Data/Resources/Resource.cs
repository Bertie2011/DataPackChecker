using DataPackChecker.Shared.Collections;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataPackChecker.Shared.Data.Resources {
    public abstract class Resource : HasKey<string> {
        [AutoReference]
        public Namespace Namespace { get; set; }
        public string IdentifierPath { get; }
        public string Name { get; }
        virtual public string TypeString => GetType().Name;
        /// <summary>
        /// Equal to "IdentifierPath/Name"
        /// </summary>
        public string Identifier => string.IsNullOrWhiteSpace(IdentifierPath) ? Name : IdentifierPath + '/' + Name;
        /// <summary>
        /// The identifier used within Minecraft to target this resource.
        /// Most often equal to "Namespace:Identifier"
        /// </summary>
        virtual public string NamespacedIdentifier => Namespace.Name + ':' + Identifier;
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
            return NamespacedIdentifier;
        }
    }
}
