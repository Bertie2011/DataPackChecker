namespace DataPackChecker.Shared.Data.Resources.Dimensions {
    public class Dimension : JsonResource {
        public override string FilePath => $"dimension/{Identifier}.json";

        public Dimension(string path, string name) : base(path, name) {}
    }
}
