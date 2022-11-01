using System;
using Newtonsoft.Json;

namespace XamarinEnterpriseApp.Xamarin.Core.Models
{
    public class GetReportStatesRequest
    {
        [JsonProperty("Istt_id")]
        public long IsttId { get; set; }

        public long SessionId { get; set; }
    }
}
