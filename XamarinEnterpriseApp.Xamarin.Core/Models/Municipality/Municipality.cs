using Newtonsoft.Json;

namespace XamarinEnterpriseApp.Xamarin.Core.Models
{
    public class Municipality
    {
        [JsonProperty("Cust_id")]
        public long CustId { get; set; }

        [JsonProperty("Cust_label")]
        public string CustLabel { get; set; }

        [JsonProperty("ListOfResidences")]
        public ResidencesData ResidencesData { get; set; }

        [JsonProperty("MunicipalityConfig")]
        public object MunicipalityConfig { get; set; }

        [JsonProperty("MunicipalityLogo")]
        public GetMunicipalityLogoResponse MunicipalityLogo { get; set; }

        public Municipality()
        {
            ResidencesData = new ResidencesData();
        }
    }
}