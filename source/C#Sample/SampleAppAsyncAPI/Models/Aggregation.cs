using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.UniversalStore.Hardware.Analytics.SampleApp.Models
{
    public class Aggregation
    {
        [JsonProperty(PropertyName = "aggregatedColumns", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> AggregatedColumns;

        [JsonProperty(PropertyName = "condition", NullValueHandling = NullValueHandling.Ignore)]
        public Condition Condition;
    }
}
