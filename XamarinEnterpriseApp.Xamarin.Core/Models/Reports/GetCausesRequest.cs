using System;
using Newtonsoft.Json;

namespace XamarinEnterpriseApp.Xamarin.Core.Models
{
    public class GetCausesRequest
    {  
        [JsonProperty("cahd_id")]
        public long CahdId { get; set; }

        public long SessionId { get; set; }
    }
}
