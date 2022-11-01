using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace XamarinEnterpriseApp.Xamarin.Core.Models
{
    public class GetCausesResponse : BaseResponse
    {
        [JsonProperty("CatLists")]
        public List<CatListItem> CatLists { get; set; }

        [JsonProperty("Result")]
        public APIResult Result { get; set; }
    }
}
