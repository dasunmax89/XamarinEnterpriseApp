using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace XamarinEnterpriseApp.Xamarin.Core.Models
{
    public class Areas
    {
        [JsonProperty("SuperAreas")]
        public List<SuperArea> SuperAreas { get; set; }
    }
}
