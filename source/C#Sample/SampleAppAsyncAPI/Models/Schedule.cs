using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.UniversalStore.Hardware.Analytics.SampleApp.Models
{
    public class Schedule
    {
        [JsonProperty(PropertyName = "startTime")]
        public string StartTime { get; set; }

        [JsonProperty(PropertyName = "recurrenceInterval")]
        public long RecurrenceInterval { get; set; }

        [JsonProperty(PropertyName = "recurrence")]
        public int Recurrence { get; set; }
    }
}
