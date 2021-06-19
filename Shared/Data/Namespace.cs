using DataPackChecker.Shared.Collections;
using DataPackChecker.Shared.Data.Resources;
using DataPackChecker.Shared.Data.Resources.Tags;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataPackChecker.Shared.Data {
    public class Namespace : HasKey<string> {
        [AutoReference]
        public DataPack DataPack { get; set; }
        public LookupList<string, Function> Functions { get; } = new LookupList<string, Function>();
        public LookupList<string, Advancement> Advancements { get; } = new LookupList<string, Advancement>();
        public LookupList<string, LootTable> LootTables { get; } = new LookupList<string, LootTable>();
        public LookupList<string, Predicate> Predicates { get; } = new LookupList<string, Predicate>();
        public LookupList<string, Recipe> Recipes { get; } = new LookupList<string, Recipe>();
        public LookupList<string, Structure> Structures { get; } = new LookupList<string, Structure>();
        public TagData TagData { get; } = new TagData();
        public DimensionData DimensionData { get; } = new DimensionData();
        public WorldGenData WorldGenData { get; } = new WorldGenData();
        public LookupList<string, ItemModifier> ItemModifiers { get; } = new LookupList<string, ItemModifier>();

        public IEnumerable<Resource> AllResources => new List<Resource>()
            .Concat(Advancements)
            .Concat(DimensionData.Dimensions)
            .Concat(DimensionData.DimensionTypes)
            .Concat(Functions)
            .Concat(LootTables)
            .Concat(Predicates)
            .Concat(Recipes)
            .Concat(Structures)
            .Concat(TagData.AllTags)
            .Concat(WorldGenData.Biomes)
            .Concat(WorldGenData.ConfiguredCarvers)
            .Concat(WorldGenData.ConfiguredFeatures)
            .Concat(WorldGenData.ConfiguredStructureFeatures)
            .Concat(WorldGenData.ConfiguredSurfaceBuilders)
            .Concat(WorldGenData.NoiseSettings)
            .Concat(WorldGenData.ProcessorLists)
            .Concat(WorldGenData.TemplatePools)
            .Concat(ItemModifiers);

        public string Name { get; }
        /// <summary>
        /// Relative to the datapack path. (data/{name})
        /// </summary>
        public string FolderPath => $"data/{Name}";
        /// <summary>
        /// Same as Name
        /// </summary>
        public string Key => Name;

        public Namespace(string name) {
            Name = name;
        }

        public override string ToString() {
            return Name;
        }
    }
}
