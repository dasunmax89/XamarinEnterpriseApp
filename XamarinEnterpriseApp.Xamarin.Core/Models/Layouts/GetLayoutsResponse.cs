using System.Collections.Generic;
using Newtonsoft.Json;

namespace XamarinEnterpriseApp.Xamarin.Core.Models
{
    public class GetLayoutsResponse : BaseResponse
    {
        [JsonProperty("Blists")]
        public List<BlistElement> Blists { get; set; }

        [JsonProperty("Result")]
        public APIResult Result { get; set; }
    }
}
