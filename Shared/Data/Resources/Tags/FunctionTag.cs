namespace DataPackChecker.Shared.Data.Resources.Tags {
    public class FunctionTag : Tag {
        public override string FilePath => $"tags/functions/{Identifier}.json";
        public override string TypeString => "Tag (Functions)";
        public FunctionTag(string path, string name) : base(path, name) {}
    }
}
