using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.UniversalStore.Hardware.Analytics.SampleApp.Models
{
    class ReportResponse
    {
        [JsonProperty(PropertyName = "reportId")]
        public int ReportId { get; set; }
    }
}
