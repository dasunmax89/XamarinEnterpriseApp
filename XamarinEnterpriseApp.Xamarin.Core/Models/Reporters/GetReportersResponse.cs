using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace XamarinEnterpriseApp.Xamarin.Core.Models
{
    public class GetReportersResponse : BaseResponse
    {
        [JsonProperty("Relations")]
        public List<Relation> Relations { get; set; }

        [JsonProperty("Result")]
        public APIResult Result { get; set; }
    }
}
