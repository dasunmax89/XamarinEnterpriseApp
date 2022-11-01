using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace XamarinEnterpriseApp.Xamarin.Core.Models
{
    public class GetMunicipalityByLocationResponse : BaseResponse
    {
        [JsonProperty("Municipalities")]
        public List<Municipality> Municipalities { get; set; }

        [JsonProperty("FoundByLonLat")]
        public Municipality FoundByLonLat { get; set; }

        [JsonProperty("Result")]
        public APIResult Result { get; set; }

        public GetMunicipalityByLocationResponse()
        {
        }
    }
}
