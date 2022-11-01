using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace XamarinEnterpriseApp.Xamarin.Core.Models
{
    public class Reporters
    {
        [JsonProperty("Relations")]
        public List<Relation> Relations { get; set; }

        public Reporters()
        {
            Relations = new List<Relation>();
        }
    }
}
