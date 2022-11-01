using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace XamarinEnterpriseApp.Xamarin.Core.Models
{
    public class GetMunicipalityReportsResponse : BaseResponse
    {
        [JsonProperty("Reports")]
        public List<ReportItem> Reports { get; set; }

        [JsonProperty("Extent")]
        public MapExtent Extent { get; set; }

        [JsonProperty("StatetypeIcons")]
        public List<StateTypeIcon> StatetypeIcons { get; set; }

        [JsonProperty("StateIcons")]
        public List<StateIcon> StateIcons { get; set; }

        [JsonProperty("Result")]
        public APIResult Result { get; set; }
    }
}
