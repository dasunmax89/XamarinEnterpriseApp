using System;
using Newtonsoft.Json;

namespace XamarinEnterpriseApp.Xamarin.Core.Models
{
    public class UpdateDeviceTokenRequest : BaseRequest
    {
        [JsonProperty("pushToken")]
        public string PushToken { get; set; }

        [JsonProperty("deviceId")]
        public string DeviceId { get; set; }
    }
}
