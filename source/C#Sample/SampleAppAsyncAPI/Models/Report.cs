using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.UniversalStore.Hardware.Analytics.SampleApp.Models
{
    public class Report
    {
        [JsonProperty(PropertyName = "templateId")]
        public int TemplateId { get; set; }

        [JsonProperty(PropertyName = "schedule")]
        public Schedule Schedule { get; set; }

        [JsonProperty(PropertyName = "callbackUrl", NullValueHandling = NullValueHandling.Ignore)]
        public string CallbackUrl { get; set; }
    }
}
