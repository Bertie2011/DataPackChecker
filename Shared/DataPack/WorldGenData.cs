using DataPackChecker.Shared.DataPack.Resources.WorldGen;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataPackChecker.Shared.DataPack {
    public class WorldGenData {
        public List<Biome> Biomes { get; } = new List<Biome>();
        public List<ConfiguredCarver> ConfiguredCarvers { get; } = new List<ConfiguredCarver>();
        public List<ConfiguredFeature> ConfiguredFeatures { get; } = new List<ConfiguredFeature>();
        public List<ConfiguredStructureFeature> ConfiguredStructureFeatures { get; } = new List<ConfiguredStructureFeature>();
        public List<ConfiguredSurfaceBuilder> ConfiguredSurfaceBuilders { get; } = new List<ConfiguredSurfaceBuilder>();
        public List<NoiseSettings> NoiseSettings { get; } = new List<NoiseSettings>();
        public List<ProcessorList> ProcessorLists { get; } = new List<ProcessorList>();
        public List<TemplatePool> TemplatePools { get; } = new List<TemplatePool>();
    }
}
