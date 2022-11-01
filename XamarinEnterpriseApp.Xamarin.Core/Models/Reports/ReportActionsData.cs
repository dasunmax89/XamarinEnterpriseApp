using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace XamarinEnterpriseApp.Xamarin.Core.Models
{
    public class ReportActionsData
    {
        [JsonProperty("Actions")]
        public List<GetActionDetailsResponse> Actions { get; set; }

        [JsonProperty("Result")]
        public APIResult Result { get; set; }

        public ReportActionsData()
        {
            Actions = new List<GetActionDetailsResponse>();
        }
    }
}
