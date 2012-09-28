using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace CloudMonitR {
    public static class PerformanceCounterFactory {
        public static List<PerformanceCounterCategoryModel> GetPerformanceCounterCategories() {
            var counterCategories = new List<PerformanceCounterCategoryModel>();

            PerformanceCounterCategory.GetCategories().ToList().ForEach((i) =>
                counterCategories.Add(new PerformanceCounterCategoryModel { Name = i.CategoryName })
                );

            counterCategories = counterCategories.OrderBy(x => x.Name).ToList();

            counterCategories.ForEach((i) => {
                try {
                    var cat = new PerformanceCounterCategory(i.Name);
                    var instances = cat.GetInstanceNames();

                    if(instances.Length > 0) {
                        foreach(var instance in instances) {
                            i.Instances.Add(instance);
                        }
                    }
                }
                catch {
                    // sometimes this freaks out when an instance can't be examined
                }
            });

            return counterCategories;
        }

        public static List<string> GetCountersForCategory(string category, string instance) {
            var cat = new PerformanceCounterCategory(category);
            var counters = string.IsNullOrEmpty(instance)
                ? cat.GetCounters()
                : cat.GetCounters(instance);
            var ret = new List<string>();
            foreach(var counter in counters) {
                ret.Add(counter.CounterName);
            }
            return ret;
        }
    }
}
