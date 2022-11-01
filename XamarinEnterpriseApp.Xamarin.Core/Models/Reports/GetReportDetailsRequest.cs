using System;
using Newtonsoft.Json;

namespace XamarinEnterpriseApp.Xamarin.Core.Models
{
    public class GetReportDetailsRequest
    {
        [JsonProperty("Session_id")]
        public long SessionId { get; set; }

        [JsonProperty("Ushd_id")]
        public long UshdId { get; set; }

        [JsonProperty("Ithd_id")]
        public long IthdId { get; set; }
    }
}
