using System.Collections.Generic;
using Newtonsoft.Json;

namespace XamarinEnterpriseApp.Xamarin.Core.Models
{
    public class MGAppConfigDataResponse : BaseResponse
    {
        [JsonProperty("errorResult")]
        public ErrorResult ErrorResult { get; set; }

        [JsonProperty("Cust_id")]
        public long CustId { get; set; }

        [JsonProperty("MaxNumberOfFiles")]
        public long MaxNumberOfFiles { get; set; }

        [JsonProperty("SearchMunicipality")]
        public string SearchMunicipality { get; set; }

        [JsonProperty("NumberOfSubcategories")]
        public int NumberOfSubcategories { get; set; }

        [JsonProperty("Display_maincategory")]
        public bool DisplayMainCategory { get; set; }

        [JsonProperty("Display_supercategory")]
        public bool DisplaySuperCategory { get; set; }

        [JsonProperty("Api")]
        public string Api { get; set; }

        [JsonProperty("Extent")]
        public MapExtent Extent { get; set; }

        [JsonIgnore]
        public string LogoImageString { get; set; }
    }
}
