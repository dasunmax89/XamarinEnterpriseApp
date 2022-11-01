using System.Collections.Generic;
using Newtonsoft.Json;

namespace XamarinEnterpriseApp.Xamarin.Core.Models
{
    public class ActionCandidates
    {
        [JsonProperty("Owners")]
        public List<ActionOwner> Owners { get; set; }
    }
}