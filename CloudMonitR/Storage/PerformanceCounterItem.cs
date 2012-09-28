using Microsoft.WindowsAzure.StorageClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CloudMonitR {
    public class PerformanceCounterItem : TableServiceEntity {
        public PerformanceCounterItem() {
            this.PartitionKey = "default";
            this.RowKey = Guid.NewGuid().ToString();
            this.Timestamp = DateTime.Now;
        }

        public string CategoryName { get; set; }
        public string CounterName { get; set; }
        public string InstanceName { get; set; }
    }
}
