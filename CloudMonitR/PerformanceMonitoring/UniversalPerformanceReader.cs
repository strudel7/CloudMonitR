using Microsoft.WindowsAzure.ServiceRuntime;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;

namespace CloudMonitR {
    public class UniversalPerformanceReader : IDisposable{
        PerformanceCounterItem _item;
        PerformanceCounter _counter;

        public string CounterName { get; set; }
        public string CounterInstance { get; set; }

        public UniversalPerformanceReader(PerformanceCounterItem item) {
            _item = item;

            this.CounterName = item.CounterName;
            this.CounterInstance = item.InstanceName;

            if(string.IsNullOrEmpty(_item.InstanceName))
                _counter = new PerformanceCounter {
                    CategoryName = _item.CategoryName,
                    CounterName = _item.CounterName
                };
            else
                _counter = new PerformanceCounter {
                    CategoryName = _item.CategoryName,
                    CounterName = _item.CounterName,
                    InstanceName = _item.InstanceName
                };
        }

        public event EventHandler<ValueReceivedEventArgs> ValueReceived;

        public void ReadValue() {
            var instNm = RoleEnvironment.CurrentRoleInstance.Id;

            try {
                var value = _counter.NextValue();

                var indxPart = instNm.Split(new[] { '_' }).Last();
                instNm = int.Parse(indxPart).ToString();

                if(ValueReceived != null)
                    ValueReceived(this,
                        new ValueReceivedEventArgs(value,
                            _item.CounterName,
                            instNm,
                            _item.InstanceName
                            ));
            }
            catch {
                Trace.WriteLine(string.Format("{0}, {1}, {2} on {3} could not be read",
                    _item.CategoryName, _item.InstanceName, _item.CounterName, instNm
                    ), "Error");
            }
        }

        public void Dispose() {
            _counter.Dispose();
        }
    }
}
