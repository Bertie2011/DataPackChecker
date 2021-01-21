namespace DataPackChecker.Shared.Data.Resources.WorldGen {
    public class TemplatePool : JsonResource {
        public override string FilePath => $"worldgen/template_pool/{Identifier}.json";
        public TemplatePool(string path, string name) : base(path, name) {}
    }
}
