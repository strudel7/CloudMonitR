using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using CloudMonitR;

namespace CloudMonitR {
    public class SignalRTraceListener : TraceListener {
        private HubClient _client;

        public SignalRTraceListener() {
            _client = HubClient.Start();
        }

        public override void Write(string message) {
            Console.Write(message);

            try {
                _client.Hub.Invoke("SendTraceMessageToGui", new {
                    body = message
                });
            }
            catch { /* might not be wired up by the time we call this */ }
        }

        public override void WriteLine(string message) {
            Console.WriteLine(message);
            try {
                _client.Hub.Invoke("SendTraceMessageToGui", new {
                    body = message
                });
            }
            catch { /* might not be wired up by the time we call this */ }
        }
    }
}
