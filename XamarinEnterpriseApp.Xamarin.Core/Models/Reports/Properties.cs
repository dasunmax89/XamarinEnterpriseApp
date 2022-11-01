using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace XamarinEnterpriseApp.Xamarin.Core.Models
{
    public class Properties
    {
        [JsonProperty("Props")]
        public List<ReportProperty> Props { get; set; }

        [JsonIgnore]
        public List<ReportProperty> DeletedProps { get; set; }

        public Properties()
        {
            Props = new List<ReportProperty>();
        }
    }
}
