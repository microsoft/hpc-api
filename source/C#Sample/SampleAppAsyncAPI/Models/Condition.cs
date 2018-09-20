using Newtonsoft.Json;
using System;
using System.Text;

namespace Microsoft.UniversalStore.Hardware.Analytics.SampleApp.Models
{
    public class Condition
    {
        [JsonProperty(PropertyName = "and", NullValueHandling = NullValueHandling.Ignore)]
        public Expression[] And { get; set; }

        [JsonProperty(PropertyName = "or", NullValueHandling = NullValueHandling.Ignore)]
        public Expression[] Or { get; set; }
    }
}
