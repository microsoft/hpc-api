using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.UniversalStore.Hardware.Analytics.SampleApp.Models
{
    public class Expression
    {
        [JsonProperty(PropertyName = "attribute", NullValueHandling = NullValueHandling.Ignore)]
        public string Attribute { get; set; }

        [JsonProperty(PropertyName = "value", NullValueHandling = NullValueHandling.Ignore)]
        public string Value { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        [JsonProperty(PropertyName = "operator", NullValueHandling = NullValueHandling.Ignore)]
        public Operator Operator { get; set; }

        [JsonProperty(PropertyName = "condition", NullValueHandling = NullValueHandling.Ignore)]
        public Condition Condition { get; set; }
    }

    public enum Operator
    {
        NONE,
        EQ,
        NEQ,
        LT,
        GT,
        LTE,
        GTE,
        LIKE,
        IN
    }
}
