using System.Collections.Generic;
using Newtonsoft.Json;

namespace XamarinEnterpriseApp.Xamarin.Core.Models
{
    public class ReportDataBlist
    {
        [JsonProperty("Blists")]
        public List<BlistElement> Blists { get; set; }
    }
}