using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace XamarinEnterpriseApp.Xamarin.Core.Models
{
    public class GetGeoInfoResponse : BaseResponse
    {
        [JsonProperty("Location")]
        public GeoLocation Location { get; set; }

        [JsonProperty("CountryCode")]
        public string CountryCode { get; set; }

        [JsonProperty("CountryName")]
        public string CountryName { get; set; }

        [JsonProperty("FeatureName")]
        public string FeatureName { get; set; }

        [JsonProperty("PostalCode")]
        public string PostalCode { get; set; }

        [JsonProperty("SubLocality")]
        public string SubLocality { get; set; }

        [JsonProperty("Thoroughfare")]
        public string Thoroughfare { get; set; }

        [JsonProperty("SubThoroughfare")]
        public string SubThoroughfare { get; set; }

        [JsonProperty("Locality")]
        public string Locality { get; set; }

        [JsonProperty("AdminArea")]
        public string AdminArea { get; set; }

        [JsonProperty("SubAdminArea")]
        public string SubAdminArea { get; set; }

        public GetGeoInfoResponse()
        {
        }
    }
}
