using Newtonsoft.Json;

namespace XamarinEnterpriseApp.Xamarin.Core.Models
{
    public class Residence
    {
        [JsonProperty("Cust_id")]
        public long CustId { get; set; }

        [JsonProperty("Suar_id")]
        public long SuarId { get; set; }

        [JsonProperty("ResidenceLabel")]
        public string ResidenceLabel { get; set; }
    }
}