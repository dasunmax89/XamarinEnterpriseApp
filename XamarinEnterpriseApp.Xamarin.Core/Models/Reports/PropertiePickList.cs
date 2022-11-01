using Newtonsoft.Json;

namespace XamarinEnterpriseApp.Xamarin.Core.Models
{
    public class PropertiePickListItem
    {
        [JsonProperty("Id")]
        public long Id { get; set; }

        [JsonProperty("Pitp_id")]
        public long PitpId { get; set; }

        [JsonProperty("Label")]
        public string Label { get; set; }

        [JsonIgnore]
        public ReportProperty ReportProperty { get; set; }
    }
}