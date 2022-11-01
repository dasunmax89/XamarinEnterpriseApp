using System.Collections.Generic;
using Newtonsoft.Json;

namespace XamarinEnterpriseApp.Xamarin.Core.Models
{
    public class SuperCategory
    {
        [JsonProperty("Suca_id")]
        public long SucaId { get; set; }

        [JsonProperty("Suca_label")]
        public string SucaLabel { get; set; }

        [JsonProperty("Suca_hint")]
        public object SucaHint { get; set; }

        [JsonProperty("ListOfMainCategories")]
        public List<MainCategory> ListOfMainCategories { get; set; }

        public SuperCategory()
        {
            ListOfMainCategories = new List<MainCategory>();
        }
    }
}
