using System;
using Newtonsoft.Json;

namespace XamarinEnterpriseApp.Xamarin.Core.Models
{
    public class DeleteReportFilesRequest
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        public long SessionId { get; set; }
    }
}
