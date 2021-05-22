namespace DataPackChecker.Shared.Data.Resources.Tags {
    public class GameEventTag : Tag {
        public override string FilePath => $"tags/game_events/{Identifier}.json";
        public override string TypeString => "Tag (Game Events)";
        public GameEventTag(string path, string name) : base(path, name) {}
    }
}
