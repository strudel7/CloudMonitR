using SignalR.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CloudMonitR.Web.Hubs {
    [HubName("cloudMonitR")]
    public class CloudMonitRHub : Hub {
        private const string DASHBOARD = "dashboard";
        private const string WORKER = "worker";

        public void Start() {
            base.Groups.Add(Context.ConnectionId, DASHBOARD);
        }

        public void ConnectWorker() {
            base.Groups.Add(Context.ConnectionId, WORKER);
            Clients[DASHBOARD].onWorkerReady();
        }

        public void SendDataToChart(dynamic data) {
            Clients[DASHBOARD].onChartDataReceived(data);
        }

        public void SendTraceMessageToGui(dynamic message) {
            Clients[DASHBOARD].onTraceMessageReceived(new {
                body = string.Format("{0} {1} {2}",
                            DateTime.Now.ToShortDateString(),
                            DateTime.Now.ToLongTimeString(),
                            message.body)
            });
        }

        public void GetPerformanceCounterCategories() {
            Clients[WORKER].onGetPerformanceCounterCategories();
        }

        public void SendPerformanceCounterCategoriesToDashboard(List<dynamic> categories) {
            Clients[DASHBOARD].onSendPerformanceCounterCategoriesToDashboard(categories);
        }

        public void GetPerformanceCounters(string category, string instance) {
            Clients[WORKER].onGetPerformanceCounters(category, instance);
        }

        public void SendPerformanceCountersToDashboard(List<string> counters) {
            Clients[DASHBOARD].onSendPerformanceCountersToDashboard(counters);
        }

        public void AddCounterToDashboard(string category, string instance, string counter) {
            Clients[WORKER].onAddCounterToDashboard(category, instance, counter);
        }

        public void DeleteCounter(string counter, string instance) {
            Clients[WORKER].onDeleteCounter(counter, instance);
        }
    }
}