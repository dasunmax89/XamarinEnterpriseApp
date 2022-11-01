using XamarinEnterpriseApp.Xamarin.Core.Converters.JsonConverters;
using Newtonsoft.Json;

namespace XamarinEnterpriseApp.Xamarin.Core.Models
{
    public class ReportOwner
    {
        [JsonProperty("Ushd_id")]
        [JsonConverter(typeof(ParseNumberConverter))]
        public long UshdId { get; set; }

        [JsonProperty("Ushd_desc")]
        public string UshdDesc { get; set; }

        [JsonProperty("Usde_ind_default_user")]
        public string UsdeIndDefaultUser { get; set; }
    }
}