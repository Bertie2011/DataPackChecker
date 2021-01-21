namespace DataPackChecker.Shared.Data.Resources {
    public class Recipe : JsonResource {
        public override string FilePath => $"recipes/{Identifier}.json";
        public Recipe(string path, string name) : base(path, name) {}
    }
}
