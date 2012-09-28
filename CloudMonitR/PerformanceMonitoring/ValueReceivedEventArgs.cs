using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CloudMonitR {
    public class ValueReceivedEventArgs : EventArgs {
        public ValueReceivedEventArgs(float value, string name, string instanceId, string counterInstance) {
            this.Value = value;
            this.Name = name;
            this.InstanceId = instanceId;
            this.CounterInstance = counterInstance;
        }

        public float Value { get; private set; }
        public string Name { get; set; }
        public string InstanceId { get; set; }
        public string CounterInstance { get; set; }
    }
}
