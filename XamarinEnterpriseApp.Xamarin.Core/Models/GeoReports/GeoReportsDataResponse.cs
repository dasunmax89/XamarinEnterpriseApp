using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace XamarinEnterpriseApp.Xamarin.Core.Models
{
    public partial class GeoReportsDataResponse : BaseResponse
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("features")]
        public List<GJFeature> Features { get; set; }

        public string GeoJson { get; set; }

        [JsonIgnore]
        public CahdMapLayerDef CahdMapLayerDef { get; set; }

        [JsonIgnore]
        public string SelectedFeatureId { get; set; }

        [JsonIgnore]
        public string LinkedFeatureId { get; set; }

        [JsonIgnore]
        public bool ObjectSelectionEnabled { get; set; }

        public GeoReportsDataResponse()
        {
        }
    }
}
