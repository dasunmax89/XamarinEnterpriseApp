using System;
using Newtonsoft.Json;

namespace XamarinEnterpriseApp.Xamarin.Core.Models
{
    public class GetReportAttributesResponse : BaseResponse
    {
        [JsonProperty("Props")]
        public Properties Properties { get; set; }

        [JsonProperty("PropertieTypes")]
        public PropertieTypes PropertieTypes { get; set; }
    }
}
