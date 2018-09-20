using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.UniversalStore.Hardware.Analytics.SampleApp.Models
{
    public class DateRange
    {
        [JsonConverter(typeof(StringEnumConverter))]
        [JsonProperty(PropertyName = "reportPeriod")]
        public ReportPeriod ReportPeriod { get; set; }

    }
    public enum ReportPeriod
    {
        Yesterday,
        Last3Days,
        Last7Days,
        Last10Days,
        Last15Days,
        Last30Days,
        Last60Days,
        Last90Days
    }
}
