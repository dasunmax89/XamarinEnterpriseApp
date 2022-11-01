using System;
using Newtonsoft.Json;

namespace XamarinEnterpriseApp.Xamarin.Core.Models
{
    public class GetReportFilesRequest
    {
        [JsonProperty("ithd_id")]
        public long Ithd_Id { get; set; }

        public long SessionId { get; set; }

        public bool IsThumbnails { get; set; }

    }
}
