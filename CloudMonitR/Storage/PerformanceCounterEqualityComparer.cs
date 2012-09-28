using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CloudMonitR {
    public class PerformanceCounterEqualityComparer : IEqualityComparer<PerformanceCounterItem> {
        public bool Equals(PerformanceCounterItem x, PerformanceCounterItem y) {
            return x.CounterName == y.CounterName 
                && x.CategoryName == y.CategoryName
                && x.InstanceName == y.InstanceName;
        }

        public int GetHashCode(PerformanceCounterItem obj) {
            return (obj.CounterName + ":" + obj.CategoryName + ":" + obj.InstanceName).GetHashCode();
        }
    }
}
