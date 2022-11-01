using Newtonsoft.Json;

namespace XamarinEnterpriseApp.Xamarin.Core.Models
{
    public class ReportItemState
    {
        [JsonProperty("Istt_id")]
        public long IsttId { get; set; }

        [JsonProperty("Istt_label")]
        public string IsttLabel { get; set; }

        [JsonProperty("Istt_hint")]
        public string IsttHint { get; set; }

        [JsonProperty("Istt_statetype")]
        public string IsttStatetype { get; set; }

    }
}