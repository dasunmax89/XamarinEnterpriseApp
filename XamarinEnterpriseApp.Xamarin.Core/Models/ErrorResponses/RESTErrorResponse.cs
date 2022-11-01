using System;
using XamarinEnterpriseApp.Xamarin.Core.Constants;
using XamarinEnterpriseApp.Xamarin.Core.Enums;
using XamarinEnterpriseApp.Xamarin.Core.Localization;
using Newtonsoft.Json;

namespace XamarinEnterpriseApp.Xamarin.Core.Models
{
    public class RESTErrorResponse
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

        public static RESTErrorResponse CreateErrorResponseFor(Exception exception)
        {
            RESTErrorResponse response =  new RESTErrorResponse()
            {
                ResultCode = ErrorCodes.INTERNAL_SERVER_ERROR.ToString(),
                ResultTitle = AppResources.AppName,
                ResultType = ResultTypes.LOCAL_CLIENT.ToDescriptionString(),
                ResultMsgBody = exception.Message,
                DetailedMsg = exception.StackTrace
            };

            return response;
        }
    }
}
