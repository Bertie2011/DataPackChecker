namespace DataPackChecker.Shared.Data.Resources.Tags {
    public class FluidTag : Tag {
        public override string FilePath => $"tags/fluids/{Identifier}.json";
        public override string TypeString => "Tag (Fluids)";
        public FluidTag(string path, string name) : base(path, name) {}
    }
}
