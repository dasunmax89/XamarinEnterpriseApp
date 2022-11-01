using Newtonsoft.Json;

namespace XamarinEnterpriseApp.Xamarin.Core.Models
{
    public class RelationAttributeListItem
    {
        [JsonProperty("label")]
        public string Label { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }
        public RelationAttribute Parent { get; internal set; }
    }
}