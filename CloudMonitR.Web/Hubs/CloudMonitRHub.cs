using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
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
            Clients.Group(DASHBOARD).onWorkerReady();
        }

        public void SendDataToChart(dynamic data) {
            Clients.Group(DASHBOARD).onChartDataReceived(data);
        }

        public void SendTraceMessageToGui(dynamic message) {
            Clients.Group(DASHBOARD).onTraceMessageReceived(new {
                body = string.Format("{0} {1} {2}",
                            DateTime.Now.ToShortDateString(),
                            DateTime.Now.ToLongTimeString(),
                            message.body)
            });
        }

        public void GetPerformanceCounterCategories() {
            Clients.Group(WORKER).onGetPerformanceCounterCategories();
        }

        public void SendPerformanceCounterCategoriesToDashboard(List<dynamic> categories) {
            Clients.Group(DASHBOARD).onSendPerformanceCounterCategoriesToDashboard(categories);
        }

        public void GetPerformanceCounters(string category, string instance) {
            Clients.Group(WORKER).onGetPerformanceCounters(category, instance);
        }

        public void SendPerformanceCountersToDashboard(List<string> counters) {
            Clients.Group(DASHBOARD).onSendPerformanceCountersToDashboard(counters);
        }

        public void AddCounterToDashboard(string category, string instance, string counter) {
            Clients.Group(WORKER).onAddCounterToDashboard(category, instance, counter);
        }

        public void DeleteCounter(string counter, string instance) {
            Clients.Group(WORKER).onDeleteCounter(counter, instance);
        }
    }
}