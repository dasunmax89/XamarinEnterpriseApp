using System;
using Newtonsoft.Json;

namespace XamarinEnterpriseApp.Xamarin.Core.Models
{
    public class GetActionDetailsRequest
    {
        [JsonProperty("Session_id")]
        public long SessionId { get; set; }

        [JsonProperty("Ushd_id")]
        public long UshdId { get; set; }

        [JsonProperty("achd_id")]
        public long ActionId { get; set; }

        [JsonProperty("cahd_id")]
        public long CatHdId { get; set; }
    }
}
