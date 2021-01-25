using System;
using System.Collections.Generic;
using System.Text;

namespace DataPackChecker.Shared.Data {
    /// <summary>
    /// A non-functional attribute indicating that the property will be filled by
    /// <see cref="DataPack.RebuildReferences"/>
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class AutoReferenceAttribute : Attribute {
    }
}
