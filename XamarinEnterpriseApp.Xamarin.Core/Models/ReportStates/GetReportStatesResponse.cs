using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace XamarinEnterpriseApp.Xamarin.Core.Models
{
    public class GetReportStatesResponse : BaseResponse
    {
        [JsonProperty("ItemStates")]
        public List<ReportItemState> ItemStates { get; set; }

        [JsonProperty("Result")]
        public APIResult Result { get; set; }
    }
}
