using System.Collections.Generic;
using Newtonsoft.Json;

namespace XamarinEnterpriseApp.Xamarin.Core.Models
{
    public class CatList
    {
        [JsonProperty("CatLists")]
        public List<CatListItem> CatLists { get; set; }
    }
}