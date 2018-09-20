using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.UniversalStore.Hardware.Analytics.SampleApp.Models
{
    class ReportTemplateResponse
    {
        [JsonProperty(PropertyName = "templateId")]
        public int TemplateId { get; set; }
    }
}
