using XamarinEnterpriseApp.Xamarin.Core.Converters.JsonConverters;
using Newtonsoft.Json;

namespace XamarinEnterpriseApp.Xamarin.Core.Models
{
    public class Location
    {
        [JsonProperty("Lohd_location")]
        public string LohdLocation { get; set; }

        [JsonProperty("Lohd_residence")]
        public string LohdResidence { get; set; }

        [JsonProperty("Lohd_text")]
        public string LohdText { get; set; }

        [JsonProperty("Lohd_arhd_id")]
        public long LohdArhdId { get; set; }

        [JsonProperty("Lohd_code")]
        public string LohdCode { get; set; }

        [JsonProperty("Lohd_lon")]
        [JsonConverter(typeof(ParseDoubleConverter))]
        public double LohdLon { get; set; }

        [JsonProperty("Lohd_lat")]
        [JsonConverter(typeof(ParseDoubleConverter))]
        public double LohdLat { get; set; }

        [JsonIgnore]
        public string LocationName
        {
            get
            {
                return !string.IsNullOrEmpty(LohdText) ? $"{LohdLocation},{LohdText}" : $"{LohdLocation}";
            }
        }

        [JsonIgnore]
        public long SuarId { get; set; }
    }
}