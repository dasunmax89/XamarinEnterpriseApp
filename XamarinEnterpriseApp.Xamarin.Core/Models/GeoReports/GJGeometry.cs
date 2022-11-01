using System;
using Newtonsoft.Json;

namespace XamarinEnterpriseApp.Xamarin.Core.Models
{
    public partial class GJGeometry
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("coordinates")]
        public double[] Coordinates { get; set; }
    }
}
