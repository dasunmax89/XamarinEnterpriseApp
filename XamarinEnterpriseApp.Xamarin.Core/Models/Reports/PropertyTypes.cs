using System.Collections.Generic;
using Newtonsoft.Json;

namespace XamarinEnterpriseApp.Xamarin.Core.Models
{
    public class PropertyTypes
    {
        [JsonProperty("PropertieTypes")]
        public List<PropertieType> PropertieTypes { get; set; }
    }
}