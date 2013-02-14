using Microsoft.WindowsAzure.ServiceRuntime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNet.SignalR.Client.Hubs;

namespace CloudMonitR {
    internal class HubClient {
        HubConnection _connection;
        public IHubProxy Hub { get; set; }

        private HubClient() {
        }

        public static HubClient Start() {
            var url = RoleEnvironment.GetConfigurationSettingValue("HubURL");
            var ret = new HubClient();
            ret._connection = new HubConnection(url);
            ret.Hub = ret._connection.CreateHubProxy("cloudMonitR");
            ret._connection.Start().Wait();
            return ret;
        }
    }
}
