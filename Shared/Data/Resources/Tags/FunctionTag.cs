using System.Collections.Generic;

namespace DataPackChecker.Shared.Data.Resources.Tags {
    public class FunctionTag : Tag {
        public override string FilePath => $"tags/functions/{Identifier}.json";
        public override string TypeString => "Tag (Functions)";

        /// <summary>
        /// A list of all functions referenced by this tag.
        /// This is recursive for any tags listed in other tags.
        /// </summary>
        [AutoReference]
        public List<Function> References { get; } = new List<Function>();

        public FunctionTag(string path, string name) : base(path, name) {}
    }
}
