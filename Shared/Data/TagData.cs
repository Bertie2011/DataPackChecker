using DataPackChecker.Shared.Collections;
using DataPackChecker.Shared.Data.Resources.Dimensions;
using DataPackChecker.Shared.Data.Resources.Tags;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataPackChecker.Shared.Data {
    public class TagData {
        public LookupList<string, BlockTag> BlockTags { get; } = new LookupList<string, BlockTag>();
        public LookupList<string, EntityTag> EntityTags { get; } = new LookupList<string, EntityTag>();
        public LookupList<string, FluidTag> FluidTags { get; } = new LookupList<string, FluidTag>();
        public LookupList<string, FunctionTag> FunctionTags { get; } = new LookupList<string, FunctionTag>();
        public LookupList<string, ItemTag> ItemTags { get; } = new LookupList<string, ItemTag>();
        public LookupList<string, GameEventTag> GameEventTags { get; } = new LookupList<string, GameEventTag>();
        public IEnumerable<Tag> AllTags => new List<Tag>()
            .Concat(BlockTags)
            .Concat(EntityTags)
            .Concat(FluidTags)
            .Concat(FunctionTags)
            .Concat(ItemTags)
            .Concat(GameEventTags);
    }
}
