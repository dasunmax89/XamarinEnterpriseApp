using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace XamarinEnterpriseApp.Xamarin.Core.Models
{
    public class GetReportingMethodsResponse : BaseResponse
    {
        [JsonProperty("ALists")]
        public List<ReportingMethod> ALists { get; set; }

        [JsonProperty("Result")]
        public APIResult Result { get; set; }
    }
}
