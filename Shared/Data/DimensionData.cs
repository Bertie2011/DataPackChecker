using DataPackChecker.Shared.Collections;
using DataPackChecker.Shared.Data.Resources.Dimensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataPackChecker.Shared.Data {
    public class DimensionData {
        public LookupList<string, Dimension> Dimensions { get; } = new LookupList<string, Dimension>();
        public LookupList<string, DimensionType> DimensionTypes { get; } = new LookupList<string, DimensionType>();
    }
}
