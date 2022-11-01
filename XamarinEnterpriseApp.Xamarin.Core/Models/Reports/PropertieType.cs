using System;
using System.Collections.Generic;
using XamarinEnterpriseApp.Xamarin.Core.Constants;
using Newtonsoft.Json;

namespace XamarinEnterpriseApp.Xamarin.Core.Models
{
    public class PropertieType
    {
        [JsonProperty("Id")]
        public long Id { get; set; }

        [JsonProperty("Label")]
        public string Label { get; set; }

        [JsonProperty("Type")]
        public string Type { get; set; }

        [JsonProperty("Pitp_id")]
        public string PitpId { get; set; }

        [JsonProperty("Isobject")]
        public string Isobject { get; set; }

        [JsonProperty("DefaultValue")]
        public string DefaultValue { get; set; }

        [JsonProperty("IndMoreThenOne")]
        public string IndMoreThenOne { get; set; }

        [JsonProperty("IndMandatory")]
        public string IndMandatory { get; set; }

        [JsonProperty("PropertiePickLists")]
        public List<PropertiePickListItem> PropertiePickLists { get; set; }

        [JsonProperty("MandatoryStates")]
        public List<long> MandatoryStates { get; set; }

        public static PropertieType GetDefault(long id)
        {
            return new PropertieType()
            {
                Id = id,
                Type = "Text",
                Label = "Default"
            };
        }

        public PropertieType()
        {
            MandatoryStates = new List<long>();
        }
    }
}