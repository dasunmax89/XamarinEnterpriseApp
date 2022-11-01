using System.Collections.Generic;
using Newtonsoft.Json;

namespace XamarinEnterpriseApp.Xamarin.Core.Models
{
    public class SuperArea
    {
        [JsonProperty("Suar_id")]
        public long SuarId { get; set; }

        [JsonProperty("Suar_label")]
        public string SuarLabel { get; set; }

        [JsonProperty("Suar_hint")]
        public string SuarHint { get; set; }

        [JsonProperty("ListOfMainAreas")]
        public List<MainArea> MainAreas { get; set; }
    }
}