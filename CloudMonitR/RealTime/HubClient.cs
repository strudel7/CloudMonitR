using Microsoft.WindowsAzure.ServiceRuntime;
using SignalR.Client.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
            ret.Hub = ret._connection.CreateProxy("cloudMonitR");
            ret._connection.Start().Wait();
            return ret;
        }
    }
}
