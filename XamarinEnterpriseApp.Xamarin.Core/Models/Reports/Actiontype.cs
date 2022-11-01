using XamarinEnterpriseApp.Xamarin.Core.Converters.JsonConverters;
using Newtonsoft.Json;

namespace XamarinEnterpriseApp.Xamarin.Core.Models
{
    public class ActionType
    {
        [JsonProperty("Athd_id")]
        [JsonConverter(typeof(ParseNumberConverter))]
        public long AthdId { get; set; }

        [JsonProperty("Athd_label")]
        public string AthdLabel { get; set; }

        [JsonProperty("Athd_hint")]
        public string AthdHint { get; set; }
    }
}