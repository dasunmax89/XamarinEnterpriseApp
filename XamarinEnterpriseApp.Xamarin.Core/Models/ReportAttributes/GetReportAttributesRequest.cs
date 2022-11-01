using System;
using Newtonsoft.Json;

namespace XamarinEnterpriseApp.Xamarin.Core.Models
{
    public class GetReportAttributesRequest
    {
        [JsonProperty("ithd_id")]
        public long Ithd_Id { get; set; }

        [JsonProperty("prgr_id")]
        public long Prgr_Id { get; set; }

        public long SessionId { get; set; }
    }
}
