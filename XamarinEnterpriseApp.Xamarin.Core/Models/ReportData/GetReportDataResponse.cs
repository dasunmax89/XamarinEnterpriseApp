using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace XamarinEnterpriseApp.Xamarin.Core.Models
{
    public class GetReportDataResponse : BaseResponse
    {
        [JsonProperty("ItemCandidates")]
        public ItemCandidates ItemCandidates { get; set; }

        [JsonProperty("ItemOwner")]
        public ReportOwner ItemOwner { get; set; }

        [JsonProperty("Categorie")]
        public Categorie Categorie { get; set; }

        [JsonProperty("Areas")]
        public Areas Areas { get; set; }

        [JsonProperty("Propertietypes")]
        public PropertyTypes PropertieTypes { get; set; }

        [JsonProperty("ReportStates")]
        public ReportStates ReportStates { get; set; }

        [JsonProperty("CatList")]
        public CatList CatList { get; set; }

        [JsonProperty("Alist")]
        public ReportDataAlist Alist { get; set; }

        [JsonProperty("Blist")]
        public ReportDataBlist Blist { get; set; }

        [JsonProperty("ItemRights")]
        public List<ReportRight> ItemRights { get; set; }

        [JsonProperty("Result")]
        public APIResult Result { get; set; }

    }
}
