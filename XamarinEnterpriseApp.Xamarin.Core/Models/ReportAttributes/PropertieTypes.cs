using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace XamarinEnterpriseApp.Xamarin.Core.Models
{
    public class PropertieTypes
    {
        [JsonProperty("PropertieTypes")]
        public List<PropertieType> PropertieTypeList { get; set; }
    }
}
