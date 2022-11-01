using Newtonsoft.Json;

namespace XamarinEnterpriseApp.Xamarin.Core.Models
{
    public class GetAreasRequest
    {
        [JsonProperty("Cahd_id")]
        public long CahdId { get; set; }

        [JsonProperty("Maca_id")]
        public long MacaId { get; set; }

        [JsonProperty("Suca_id")]
        public long SucaId { get; set; }

        [JsonProperty("Action")]
        public string Action { get; set; }

        public long SessionId { get; set; }
    }
}