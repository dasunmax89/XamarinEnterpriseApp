using System.Collections.Generic;
using Newtonsoft.Json;

namespace XamarinEnterpriseApp.Xamarin.Core.Models
{
    public class CahdMapLayerDef
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("vendor")]
        public string Vendor { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("fields")]
        public List<string> Fields { get; set; }

        [JsonProperty("mapHint")]
        public string MapHint { get; set; }

        [JsonProperty("residence")]
        public string Residence { get; set; }

        [JsonProperty("street")]
        public string Street { get; set; }

        [JsonProperty("dropdownSelect")]
        public string DropdownSelect { get; set; }

        [JsonProperty("dropdownPlaceholder")]
        public string DropdownPlaceholder { get; set; }

        [JsonProperty("dropdownLabel")]
        public string DropdownLabel { get; set; }

        [JsonProperty("minXUriParam")]
        public string MinXUriParam { get; set; }

        [JsonProperty("minYUriParam")]
        public string MinYUriParam { get; set; }

        [JsonProperty("maxXUriParam")]
        public string MaxXUriParam { get; set; }

        [JsonProperty("maxYUriParam")]
        public string MaxYUriParam { get; set; }

        [JsonProperty("icon")]
        public string Icon { get; set; }

        [JsonProperty("MijnGemPlural")]
        public string PluralObject { get; set; }

        [JsonProperty("MijnGemSingular")]
        public string SingularObject { get; set; }

        [JsonIgnore]
        public long CahdId { get; set; }

    }
}