namespace DataPackChecker.Shared.Data.Resources {
    public class Predicate : JsonResource {
        public override string FilePath => $"predicates/{Identifier}.json";
        public Predicate(string path, string name) : base(path, name) {}
    }
}
