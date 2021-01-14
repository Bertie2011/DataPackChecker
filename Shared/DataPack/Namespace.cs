using DataPackChecker.Shared.DataPack.Resources;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataPackChecker.Shared.DataPack {
    public class Namespace {
        public List<Function> Functions { get; } = new List<Function>();
        public List<Tag> Tags { get; } = new List<Tag>();
        public List<Advancement> Advancements { get; } = new List<Advancement>();
        public List<LootTable> LootTables { get; } = new List<LootTable>();
        public List<Predicate> Predicates { get; } = new List<Predicate>();
        public List<Recipe> Recipes { get; } = new List<Recipe>();
        public List<Structure> Structures { get; } = new List<Structure>();
        public DimensionData DimensionData { get; } = new DimensionData();
        public WorldGenData WorldGenData { get; } = new WorldGenData();
        public string Name { get; }

        public Namespace(string name) {
            Name = name;
        }
    }
}
