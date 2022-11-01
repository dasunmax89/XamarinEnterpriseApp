using System.Collections.Generic;
using Newtonsoft.Json;

namespace XamarinEnterpriseApp.Xamarin.Core.Models
{
    public class Categorie
    {
        [JsonProperty("SuperCategories")]
        public List<SuperCategory> SuperCategories { get; set; }

    }
}