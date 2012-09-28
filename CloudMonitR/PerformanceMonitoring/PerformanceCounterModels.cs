using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CloudMonitR {

    public class PerformanceCounterCategoryModel {
        public PerformanceCounterCategoryModel() {
            this.Counters = new List<PerformanceCounterModel>();
            this.Instances = new List<string>();
        }
        public string Name { get; set; }
        public List<string> Instances { get; set; }
        public List<PerformanceCounterModel> Counters { get; set; }
    }

    public class PerformanceCounterModel {
        public string Name { get; set; }
    }

}
