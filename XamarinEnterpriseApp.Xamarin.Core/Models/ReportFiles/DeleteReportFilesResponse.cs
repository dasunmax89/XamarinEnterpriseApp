using System;
using Newtonsoft.Json;

namespace XamarinEnterpriseApp.Xamarin.Core.Models
{
    public class DeleteReportFilesResponse : BaseResponse
    {
        [JsonProperty("Result")]
        public APIResult Result { get; set; }
    }
}
