using System.Collections.Generic;
using Newtonsoft.Json;

namespace XamarinEnterpriseApp.Xamarin.Core.Models
{
    public class ReportDataAlist
    {
        [JsonProperty("ALists")]
        public List<ReportingMethod> ALists { get; set; }

    }
}