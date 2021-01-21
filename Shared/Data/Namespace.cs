using DataPackChecker.Shared.Collections;
using DataPackChecker.Shared.Data.Resources;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataPackChecker.Shared.Data {
    public class Namespace : HasKey<string> {
        public LookupList<string, Function> Functions { get; } = new LookupList<string, Function>();
        public LookupList<string, Tag> Tags { get; } = new LookupList<string, Tag>();
        public LookupList<string, Advancement> Advancements { get; } = new LookupList<string, Advancement>();
        public LookupList<string, LootTable> LootTables { get; } = new LookupList<string, LootTable>();
        public LookupList<string, Predicate> Predicates { get; } = new LookupList<string, Predicate>();
        public LookupList<string, Recipe> Recipes { get; } = new LookupList<string, Recipe>();
        public LookupList<string, Structure> Structures { get; } = new LookupList<string, Structure>();
        public DimensionData DimensionData { get; } = new DimensionData();
        public WorldGenData WorldGenData { get; } = new WorldGenData();
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
