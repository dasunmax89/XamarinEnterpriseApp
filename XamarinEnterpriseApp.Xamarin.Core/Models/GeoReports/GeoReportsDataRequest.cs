using System;
using Newtonsoft.Json;

namespace XamarinEnterpriseApp.Xamarin.Core.Models
{
    public class GeoReportsDataRequest : BaseRequest
    {
        [JsonIgnore]
        public long SessionId { get; set; }

        public CahdMapLayerDef CahdMapLayerDef { get; internal set; }

        public int MinX { get; set; }

        public int MinY { get; set; }

        public int MaxX { get; set; }

        public int MaxY { get; set; }
    }
}
