
using Newtonsoft.Json;

namespace XamarinEnterpriseApp.Xamarin.Core.Models
{
    public partial class ErrorResult
    {
        [JsonProperty("result_code")]
        public string ResultCode { get; set; }

        [JsonProperty("result_type")]
        public string ResultType { get; set; }

        [JsonProperty("result_title")]
        public string ResultTitle { get; set; }

        [JsonProperty("result_msg_body")]
        public string ResultMsgBody { get; set; }

        [JsonProperty("detailed_msg")]
        public string DetailedMsg { get; set; }
    }
}

