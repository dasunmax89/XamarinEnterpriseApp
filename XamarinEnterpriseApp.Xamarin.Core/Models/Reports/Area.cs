using System.Collections.Generic;
using Newtonsoft.Json;

namespace XamarinEnterpriseApp.Xamarin.Core.Models
{
    public class Area
    {
        [JsonProperty("Arhd_id")]
        public long ArhdId { get; set; }

        [JsonProperty("Arhd_label")]
        public string ArhdLabel { get; set; }

        [JsonProperty("Arhd_hint")]
        public string ArhdHint { get; set; }

        [JsonProperty("ListOfLocations")]
        public List<Location> Locations { get; set; }
    }
}