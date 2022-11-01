using System;
using Newtonsoft.Json;

namespace XamarinEnterpriseApp.Xamarin.Core.Models
{
    public class BaseResponse
    {
        [JsonIgnore]
        public bool IsSuccessful { get; set; }

        [JsonIgnore]
        public bool IsSessionExpired { get; internal set; }

        [JsonIgnore]
        public RESTErrorResponse Error { get; set; }

        [JsonIgnore]
        public bool IsTaskCanceled { get; set; }

        [JsonIgnore]
        public bool IsUrlError { get; set; }

        public BaseResponse()
        {
        }
    }
}
