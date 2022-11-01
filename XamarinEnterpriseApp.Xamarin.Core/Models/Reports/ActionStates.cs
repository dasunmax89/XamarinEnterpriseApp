using System.Collections.Generic;
using Newtonsoft.Json;

namespace XamarinEnterpriseApp.Xamarin.Core.Models
{
    public class ActionStates
    {
        [JsonProperty("ActionStates")]
        public List<ActionState> ActionStatesList { get; set; }

    }
}