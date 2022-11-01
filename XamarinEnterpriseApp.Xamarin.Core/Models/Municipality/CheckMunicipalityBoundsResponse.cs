using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace XamarinEnterpriseApp.Xamarin.Core.Models
{
    public class CheckMunicipalityBoundsResponse : BaseResponse
    {
        [JsonProperty("InMunicipality")]
        public bool InMunicipality { get; set; }

        [JsonProperty("Result")]
        public APIResult Result { get; set; }

        public CheckMunicipalityBoundsResponse()
        {

        }
    }
}
