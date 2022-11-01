using System.Collections.Generic;
using Newtonsoft.Json;

namespace XamarinEnterpriseApp.Xamarin.Core.Models
{
    public class ActionTypes
    {
        [JsonProperty("Actiontypes")]
        public List<ActionType> Actiontypes { get; set; }
    }
}