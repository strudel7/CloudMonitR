﻿using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.ServiceRuntime;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using Microsoft.WindowsAzure.Storage.Table.DataServices;
using System;
using System.Collections.Generic;
using System.Data.Services.Client;
using System.Linq;
using System.Text;

namespace CloudMonitR {
    public class StorageWrapper {
        private CloudStorageAccount _storageAccount;
        private string _connectionStringName = "CloudMonitRConnectionString";
        private CloudTableClient _tableClient;
        private string _tableName = "performancecounteritem";
        private TableServiceContext _tableContext;

        public StorageWrapper() {
            _storageAccount = CloudStorageAccount.Parse(
                RoleEnvironment.GetConfigurationSettingValue(_connectionStringName)
                );
            _tableClient = _storageAccount.CreateCloudTableClient();
            _tableClient.GetTableReference(_tableName);
            _tableClient.GetTableReference(_tableName).CreateIfNotExists();
            _tableContext = _tableClient.GetTableServiceContext();
        }

        public void Add(PerformanceCounterItem item) {
            var existing = GetCounters(false);

            if(existing.Any(x => x.CategoryName == item.CategoryName &&
                x.InstanceName == item.InstanceName))
                return;

            _tableContext.AddObject(_tableName, item);
            try {
                _tableContext.SaveChanges(SaveChangesOptions.Batch);
            }
            catch {
                // sometimes this happens when the roles collide
            }
        }

        public List<PerformanceCounterItem> GetCounters(bool distinct = true) {
            var r = _tableContext.CreateQuery<PerformanceCounterItem>(_tableName).ToList();
            if(distinct)
                r = r.Distinct(new PerformanceCounterEqualityComparer()).ToList();
            return r;
        }

        public void DeleteCounter(string counter, string instance) {
            var x = GetCounters(false);
            x.ForEach((i) => {
                if(i.CounterName == counter && i.InstanceName == instance) {
                    _tableContext.DeleteObject(i);
                }
            });

            try {
                _tableContext.SaveChanges(SaveChangesOptions.Batch);
            }
            catch {
                // this happens when the records have already been deleted and a second worker tries to delete them
            }
        }
    }
}
