using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.UniversalStore.Hardware.Analytics.SampleApp.Configurations
{
    class HostConfig
    {
        public string AzureADTokenServiceUrl { get; set; }
        public string AsyncAPIServiceBaseUrl { get; set; }
        public string TenantId { get; set; }
        public string StorageTenantId { get; set; }
    }
}
