using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using SignalR;
using SignalR.Client.Hubs;

namespace CloudMonitR {
    public class CloudMonitREngine {
        internal List<UniversalPerformanceReader> Readers { get; set; }

        private CloudMonitREngine()
        {
        }
        
        public void ReadValue() {
            Readers.ForEach((x) => x.ReadValue());
        }

        public static CloudMonitREngine Setup() {
            CloudMonitREngine engine = new CloudMonitREngine();
            engine.Readers = new List<UniversalPerformanceReader>();

            Trace.WriteLine("Connecting to Hub", "Information");
            
            var client = HubClient.Start();

            Trace.WriteLine("Connected to Hub", "Information");

            var categories = PerformanceCounterFactory.GetPerformanceCounterCategories();

            var storage = new StorageWrapper();

            client.Hub.Invoke("connectWorker");

            client.Hub.On("onGetPerformanceCounterCategories", () => {
                client.Hub.Invoke("sendPerformanceCounterCategoriesToDashboard", categories);
            });

            client.Hub.On<string, string>("onGetPerformanceCounters",
                (category, instance) => {
                    var counters = PerformanceCounterFactory.GetCountersForCategory(category, instance);
                    client.Hub.Invoke("SendPerformanceCountersToDashboard", counters);
                });

            client.Hub.On<string, string>("onDeleteCounter",
                (counter, instance) => {
                    storage.DeleteCounter(counter, instance);
                    try {
                        var r = engine.Readers.First(x => x.CounterInstance == instance 
                            && x.CounterName == counter);

                        engine.Readers.Remove(r);
                        r.Dispose();
                    }
                    catch {
                        // this happens when storage has already deleted the item but subsequent workers haven't caught up
                    }
                });

            client.Hub.On<string, string, string>("onAddCounterToDashboard",
                (category, instance, counter) => {
                    var itm = new PerformanceCounterItem {
                        CategoryName = category,
                        InstanceName = instance,
                        CounterName = counter
                    };
                    var rdr = new UniversalPerformanceReader(itm);

                    rdr.ValueReceived += (s, e) => {
                        client.Hub.Invoke("sendDataToChart", new {
                            name = e.Name,
                            value = e.Value,
                            instanceId = e.InstanceId,
                            counterInstance = e.CounterInstance
                        });
                    };

                    storage.Add(itm);

                    engine.Readers.Add(rdr);
                    rdr.ReadValue();
                });

            storage.GetCounters().ForEach((x) => {
                var rdr = new UniversalPerformanceReader(x);
                rdr.ValueReceived += (s, e) => {
                    client.Hub.Invoke("sendDataToChart", new {
                        name = e.Name,
                        value = e.Value,
                        instanceId = e.InstanceId,
                        counterInstance = e.CounterInstance
                    });
                };
                engine.Readers.Add(rdr);
            });

            return engine;
        }
    }
}
