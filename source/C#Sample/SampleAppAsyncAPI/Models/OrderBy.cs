using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.UniversalStore.Hardware.Analytics.SampleApp.Models
{
    public class OrderBy
    {
        [JsonProperty(PropertyName = "attribute", NullValueHandling = NullValueHandling.Ignore)]
        public string Attribute { get; set; }

        [JsonProperty(PropertyName = "sort", NullValueHandling = NullValueHandling.Ignore)]
        public string Sort { get; set; }
    }
}
