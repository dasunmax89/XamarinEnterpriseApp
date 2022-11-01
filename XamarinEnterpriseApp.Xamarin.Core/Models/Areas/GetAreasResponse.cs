using System.Collections.Generic;
using Newtonsoft.Json;

namespace XamarinEnterpriseApp.Xamarin.Core.Models
{
    public class GetAreasResponse : BaseResponse
    {
        [JsonProperty("SuperAreas")]
        public List<SuperArea> SuperAreas { get; set; }

        [JsonProperty("Result")]
        public APIResult Result { get; set; }
    }
}