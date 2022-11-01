using System.Collections.Generic;
using Newtonsoft.Json;

namespace XamarinEnterpriseApp.Xamarin.Core.Models
{
    public class ItemCandidates
    {
        [JsonProperty("Owners")]
        public List<ReportOwner> Owners { get; set; }

    }
}