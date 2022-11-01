using System;
using Newtonsoft.Json;

namespace XamarinEnterpriseApp.Xamarin.Core.Models
{
    public class GetReportOwnersRequest
    {
        [JsonProperty("arhd_id")]
        public long ArhdId { get; set; }

        [JsonProperty("cahd_id")]
        public long CahdId { get; set; }

        public long SessionId { get;  set; }
    }
}
