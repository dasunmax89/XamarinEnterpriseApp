using System;
using XamarinEnterpriseApp.Xamarin.Core.Converters.JsonConverters;
using Newtonsoft.Json;

namespace XamarinEnterpriseApp.Xamarin.Core.Models
{
    public class GeoLocation
    {
        [JsonProperty("Timestamp")]
        public DateTimeOffset Timestamp { get; set; }

        [JsonProperty("Latitude")]
        public double Latitude { get; set; }

        [JsonProperty("Longitude")]
        public double Longitude { get; set; }

        [JsonProperty("Altitude")]
        public string Altitude { get; set; }

        [JsonProperty("Accuracy")]
        public string Accuracy { get; set; }

        [JsonProperty("VerticalAccuracy")]
        public string VerticalAccuracy { get; set; }

        [JsonProperty("Speed")]
        public string Speed { get; set; }

        [JsonProperty("Course")]
        public string Course { get; set; }

        [JsonProperty("IsFromMockProvider")]
        public bool IsFromMockProvider { get; set; }

        [JsonProperty("AltitudeReferenceSystem")]
        public string AltitudeReferenceSystem { get; set; }
    }
}