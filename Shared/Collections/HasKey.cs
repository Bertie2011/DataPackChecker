using System;
using System.Collections.Generic;
using System.Text;

namespace DataPackChecker.Shared.Collections {
    public interface HasKey<T> {
        public T Key { get; }
    }
}
