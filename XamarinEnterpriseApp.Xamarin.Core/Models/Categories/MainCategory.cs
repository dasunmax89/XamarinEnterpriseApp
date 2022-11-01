using System.Collections.Generic;
using Newtonsoft.Json;

namespace XamarinEnterpriseApp.Xamarin.Core.Models
{
    public class MainCategory
    {
        [JsonProperty("Maca_id")]
        public long MacaId { get; set; }

        [JsonProperty("Maca_label")]
        public string MacaLabel { get; set; }

        [JsonProperty("Maca_hint")]
        public string MacaHint { get; set; }

        [JsonProperty("Maca_ex_hint")]
        public string MacaExHint { get; set; }

        [JsonProperty("Categories")]
        public List<Category> Categories { get; set; }

        [JsonIgnore]
        public long SucaId { get; set; }

        public MainCategory()
        {
            Categories = new List<Category>();
        }
    }
}
