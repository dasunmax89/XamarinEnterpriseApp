using System;
using XamarinEnterpriseApp.Xamarin.Core.Converters.JsonConverters;
using Newtonsoft.Json;

namespace XamarinEnterpriseApp.Xamarin.Core.Models
{
    public class Presentation
    {
        [JsonProperty("Marker")]
        public string Marker { get; set; }

        [JsonProperty("DiplayColor")]
        public string DiplayColor { get; set; }
    }
}
