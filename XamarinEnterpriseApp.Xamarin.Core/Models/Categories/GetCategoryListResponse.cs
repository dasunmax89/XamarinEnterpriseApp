using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace XamarinEnterpriseApp.Xamarin.Core.Models
{
    public class GetCategoryListResponse : BaseResponse
    {
        [JsonProperty("SuperCategories")]
        public List<SuperCategory> SuperCategories { get; set; }

        [JsonProperty("ListAICategories")]
        public List<Category> AISuggestions { get; set; }

        [JsonIgnore]
        public List<Category> AllSubCategories
        {
            get
            {
                List<Category> allSubCategories = new List<Category>();

                allSubCategories = SuperCategories?.SelectMany(h => h.ListOfMainCategories)
                                  ?.SelectMany(rt => rt.Categories)
                                  .ToList();

                return allSubCategories;
            }
        }

    }
}
