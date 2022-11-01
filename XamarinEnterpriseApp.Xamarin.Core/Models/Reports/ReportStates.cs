using System.Collections.Generic;
using Newtonsoft.Json;

namespace XamarinEnterpriseApp.Xamarin.Core.Models
{
    public class ReportStates
    {
        [JsonProperty("ItemStates")]
        public List<ReportItemState> ItemStates { get; set; }

    }
}