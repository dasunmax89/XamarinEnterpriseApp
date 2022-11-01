using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace XamarinEnterpriseApp.Xamarin.Core.Models
{
    public class GetReportOwnersResponse : BaseResponse
    {
        [JsonProperty("Owners")]
        public List<ReportOwner> Owners { get; set; }

        [JsonProperty("Result")]
        public APIResult Result { get; set; }
    }
}
