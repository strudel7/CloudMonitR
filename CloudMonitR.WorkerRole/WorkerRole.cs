using Microsoft.WindowsAzure.ServiceRuntime;
using System;
using System.Diagnostics;
using System.Net;
using System.Threading;

namespace CloudMonitR.WorkerRole {
    public class WorkerRole : RoleEntryPoint {

        CloudMonitREngine _engine;

        public override void Run() {
            Trace.WriteLine("CloudMonitR.WorkerRole entry point called", "Information");

            while(true) {
                Trace.WriteLine("Obtaining performance counter data from readers", "Information");

                _engine.ReadValue();

                Thread.Sleep(5000);

                Trace.WriteLine("All performance counter readers polled.", "Information");
            }
        }

        public override bool OnStart() {
            ServicePointManager.DefaultConnectionLimit = 12;

            _engine = CloudMonitREngine.Setup();

            return base.OnStart();
        }
    }
}
