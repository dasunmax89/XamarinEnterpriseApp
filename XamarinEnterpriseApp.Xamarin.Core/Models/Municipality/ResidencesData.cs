using System.Collections.Generic;
using Newtonsoft.Json;

namespace XamarinEnterpriseApp.Xamarin.Core.Models
{
    public class ResidencesData
    {
        [JsonProperty("Residences")]
        public List<Residence> Residences { get; set; }

        public ResidencesData()
        {
            Residences = new List<Residence>();
        }
    }
}