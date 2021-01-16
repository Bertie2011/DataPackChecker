using DataPackChecker.Shared.Data.Resources.Dimensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataPackChecker.Shared.Data {
    public class DimensionData {
        public List<Dimension> Dimensions { get; } = new List<Dimension>();
        public List<DimensionType> DimensionTypes { get; } = new List<DimensionType>();
    }
}
