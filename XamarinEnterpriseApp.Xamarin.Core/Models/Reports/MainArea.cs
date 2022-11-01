using System.Collections.Generic;
using Newtonsoft.Json;

namespace XamarinEnterpriseApp.Xamarin.Core.Models
{
    public class MainArea
    {
        [JsonProperty("Maar_id")]
        public long MaarId { get; set; }

        [JsonProperty("Maar_label")]
        public string MaarLabel { get; set; }

        [JsonProperty("Maar_hint")]
        public string MaarHint { get; set; }

        [JsonProperty("Areas")]
        public List<Area> Areas { get; set; }
    }
}