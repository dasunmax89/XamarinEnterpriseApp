using Newtonsoft.Json;

namespace XamarinEnterpriseApp.Xamarin.Core.Models
{
    public class CatListItem
    {
        [JsonProperty("Clhd_id")]
        public long ClhdId { get; set; }

        [JsonProperty("Clhd_label")]
        public string ClhdLabel { get; set; }

        [JsonProperty("Clhd_hint")]
        public string ClhdHint { get; set; }
    }
}