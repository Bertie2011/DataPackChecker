using DataPackChecker.Shared.Collections;
using DataPackChecker.Shared.Data.Resources.WorldGen;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataPackChecker.Shared.Data {
    public class WorldGenData {
        public LookupList<string, Biome> Biomes { get; } = new LookupList<string, Biome>();
        public LookupList<string, ConfiguredCarver> ConfiguredCarvers { get; } = new LookupList<string, ConfiguredCarver>();
        public LookupList<string, ConfiguredFeature> ConfiguredFeatures { get; } = new LookupList<string, ConfiguredFeature>();
        public LookupList<string, ConfiguredStructureFeature> ConfiguredStructureFeatures { get; } = new LookupList<string, ConfiguredStructureFeature>();
        public LookupList<string, ConfiguredSurfaceBuilder> ConfiguredSurfaceBuilders { get; } = new LookupList<string, ConfiguredSurfaceBuilder>();
        public LookupList<string, NoiseSettings> NoiseSettings { get; } = new LookupList<string, NoiseSettings>();
        public LookupList<string, ProcessorList> ProcessorLists { get; } = new LookupList<string, ProcessorList>();
        public LookupList<string, TemplatePool> TemplatePools { get; } = new LookupList<string, TemplatePool>();
    }
}
