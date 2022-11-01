using Newtonsoft.Json;

namespace XamarinEnterpriseApp.Xamarin.Core.Models
{
    public class BlistElement
    {
        [JsonProperty("Blhd_id")]
        public long BlhdId { get; set; }

        [JsonProperty("Blhd_label")]
        public string BlhdLabel { get; set; }

        [JsonProperty("Blhd_hint")]
        public string BlhdHint { get; set; }
    }
}