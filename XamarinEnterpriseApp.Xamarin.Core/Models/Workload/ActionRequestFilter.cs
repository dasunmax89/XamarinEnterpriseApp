using System;
using Newtonsoft.Json;

namespace XamarinEnterpriseApp.Xamarin.Core.Models
{
    public class ActionRequestFilter
    {
        [JsonProperty("Athd_id")]
        public string AthdId { get; set; }

        [JsonProperty("Residence")]
        public string Residence { get; set; }

        [JsonProperty("Street")]
        public string Street { get; set; }

        [JsonProperty("Location_text")]
        public string LocationText { get; set; }

        [JsonProperty("Asst_id")]
        public string AsstId { get; set; }

        [JsonProperty("Ushd_id")]
        public string UshdId { get; set; }

        [JsonProperty("DateEntryFrom")]
        public DateTime DateEntryFrom { get; set; }

        [JsonProperty("DateEntryTo")]
        public DateTime DateEntryTo { get; set; }
    }
}
