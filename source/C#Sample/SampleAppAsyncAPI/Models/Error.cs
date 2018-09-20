using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.UniversalStore.Hardware.Analytics.SampleApp.Models
{
    [JsonObject]
    [Serializable]
    public class Error
    {
        [JsonProperty]
        public string Code;

        [JsonProperty]
        public string Message;

        [JsonProperty]
        public string TrackingId;
    }
}
