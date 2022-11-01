using System;
using Newtonsoft.Json;

namespace XamarinEnterpriseApp.Xamarin.Core.Models
{
    public class ReportRequestFilter
    {
        [JsonProperty("Suca_id")]
        public string SucaId { get; set; }

        [JsonProperty("Maca_id")]
        public string MacaId { get; set; }

        [JsonProperty("Cahd_id")]
        public string CahdId { get; set; }

        [JsonProperty("Residence")]
        public string Residence { get; set; }

        [JsonProperty("Street")]
        public string Street { get; set; }

        [JsonProperty("Location_text")]
        public string LocationText { get; set; }

        [JsonProperty("Isst_id")]
        public string IsstId { get; set; }

        [JsonProperty("Ushd_id")]
        public string UshdId { get; set; }

        [JsonProperty("DateEntryFrom")]
        public DateTime DateEntryFrom { get; set; }

        [JsonProperty("DateEntryTo")]
        public DateTime DateEntryTo { get; set; }

        public ReportRequestFilter()
        {
        }
    }
}
