using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.UniversalStore.Hardware.Analytics.SampleApp.Models
{
    public class APIResponse<T>
    {
        [JsonProperty("data")]
        public T Data { get; set; }

        [JsonProperty("errors")]
        public IList<Error> Errors { get; set; }
    }
}
