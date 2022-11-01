using Newtonsoft.Json;

namespace XamarinEnterpriseApp.Xamarin.Core.Models
{
    public class StateIcon
    {
        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("stateIcon")]
        public string Icon { get; set; }
    }
}