using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Microsoft.UniversalStore.Hardware.Analytics.SampleApp.Models
{
    public class ReportTemplate
    {
        [JsonProperty(PropertyName = "projection")]
        public List<string> Projection { get; set; }

        [DefaultValue(null)]
        [JsonProperty(PropertyName = "limit", NullValueHandling = NullValueHandling.Ignore)]
        public int Limit { get; set; }

        [JsonProperty(PropertyName = "view")]
        public string View { get; set; }

        [JsonProperty(PropertyName = "condition", NullValueHandling = NullValueHandling.Ignore)]
        public Condition Condition { get; set; }

        [JsonProperty(PropertyName = "dateRange")]
        public DateRange DatePeriod { get; set; }

        [JsonProperty(PropertyName = "aggregation", NullValueHandling = NullValueHandling.Ignore)]
        public Aggregation Aggregation { get; set; }

        [JsonProperty(PropertyName = "orderby", NullValueHandling = NullValueHandling.Ignore)]
        public List<OrderBy> OrderBy { get; set; }
    }
}
