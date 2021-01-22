namespace DataPackChecker.Shared.Data.Resources.Tags {
    public class BlockTag : Tag {
        public override string FilePath => $"tags/blocks/{Identifier}.json";
        public override string TypeString => "Tag (Blocks)";

        public BlockTag(string path, string name) : base(path, name) {}
    }
}
