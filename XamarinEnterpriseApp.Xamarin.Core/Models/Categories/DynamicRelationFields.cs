using System.Collections.Generic;
using Newtonsoft.Json;

namespace XamarinEnterpriseApp.Xamarin.Core.Models
{
    public class DynamicRelationFields
    {
        [JsonProperty("RelationAttributes")]
        public List<RelationAttribute> RelationAttributes { get; set; }
    }
}