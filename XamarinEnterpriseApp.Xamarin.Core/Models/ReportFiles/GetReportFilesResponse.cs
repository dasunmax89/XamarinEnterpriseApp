using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace XamarinEnterpriseApp.Xamarin.Core.Models
{
    public class GetReportFilesResponse : BaseResponse
    { 
        [JsonProperty("Files")]
        public List<ReportFile> Files { get; set; }

        [JsonProperty("Result")]
        public APIResult Result { get; set; }
    }
}
