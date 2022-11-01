using System;
using Newtonsoft.Json;

namespace XamarinEnterpriseApp.Xamarin.Core.Models
{
    public class BaseRequest
    {
        [JsonProperty("deviceToken")]
        public string DeviceToken { get; set; }

        [JsonProperty("appVersion")]
        public string AppVersion { get; set; }

        [JsonProperty("devicePlatform")]
        public string DevicePlatform { get; set; }

        [JsonProperty("ipAddress")]
        public string IpAddress { get; set; }

        [JsonProperty("application")]
        public string Application { get; set; }

        [JsonProperty("language")]
        public string Language { get; set; }

        [JsonIgnore]
        public string EndPoint { get; set; }

        [JsonIgnore]
        public bool IsMock { get; set; }

        [JsonIgnore]
        public bool BypassApiEndpoint { get; set; }

        public BaseRequest()
        {

        }

        public virtual void GenerateEndpoint()
        {

        }
    }
}
