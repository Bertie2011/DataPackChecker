namespace DataPackChecker.Shared.Data.Resources {
    public class Advancement : JsonResource {
        public override string FilePath => $"advancements/{Identifier}.json";
        public Advancement(string path, string name) : base(path, name) {}
    }
}
