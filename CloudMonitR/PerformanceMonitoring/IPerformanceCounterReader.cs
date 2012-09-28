using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CloudMonitR.WorkerRole {
    public interface IPerformanceCounterReader {
        event EventHandler<ValueReceivedEventArgs> ValueReceived;
        void ReadValue();
    }
}
