using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace XamarinEnterpriseApp.Xamarin.Core.Models
{
    public class GetLanguagesResponse : BaseResponse
    {
        [JsonProperty("Languages")]
        public List<Language> Languages { get; set; }

        [JsonProperty("Result")]
        public APIResult Result { get; set; }
    }
}
