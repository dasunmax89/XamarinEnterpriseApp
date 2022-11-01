using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace XamarinEnterpriseApp.Xamarin.Core.Models
{
    public class ActionsData
    {
        [JsonProperty("Actions")]
        public List<ReportAction> Actions { get; set; }

        [JsonProperty("GeoTemplate")]
        public GeoTemplate GeoTemplate { get; set; }
         
        [JsonProperty("Result")]
        public APIResult Result { get; set; }

        public ActionsData()
        {
        }
    }
}
