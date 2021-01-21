namespace DataPackChecker.Shared.Data.Resources.Dimensions {
    public class DimensionType : JsonResource {
        public override string FilePath => $"dimension_type/{Identifier}.json";
        public DimensionType(string path, string name) : base(path, name) {}
    }
}
