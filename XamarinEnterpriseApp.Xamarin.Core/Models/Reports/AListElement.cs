using Newtonsoft.Json;

namespace XamarinEnterpriseApp.Xamarin.Core.Models
{
    public class ReportingMethod
    {
        [JsonProperty("Alhd_id")]
        public long AlhdId { get; set; }

        [JsonProperty("Alhd_label")]
        public string AlhdLabel { get; set; }

        [JsonProperty("Alhd_hint")]
        public string AlhdHint { get; set; }
    }
}