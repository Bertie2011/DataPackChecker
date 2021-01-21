namespace DataPackChecker.Shared.Data.Resources {
    public class Structure : Resource {
        public override string FilePath => $"structures/{Identifier}.nbt";
        public Structure(string path, string name) : base(path, name) {}
    }
}
