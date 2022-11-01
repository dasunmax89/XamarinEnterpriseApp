using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace XamarinEnterpriseApp.Xamarin.Core.Models
{
    public class ReportsData
    {
        [JsonProperty("Reports")]
        public List<Report> Reports { get; set; }

        [JsonProperty("GeoTemplate")]
        public GeoTemplate GeoTemplate { get; set; }

        [JsonProperty("Result")]
        public APIResult Result { get; set; }
    }
}
