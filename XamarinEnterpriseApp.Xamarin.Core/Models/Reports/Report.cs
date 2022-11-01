using System;
using XamarinEnterpriseApp.Xamarin.Core.Converters.JsonConverters;
using Newtonsoft.Json;

namespace XamarinEnterpriseApp.Xamarin.Core.Models
{
    public class Report
    {
        [JsonProperty("Ithd_id")]
        [JsonConverter(typeof(ParseNumberConverter))]
        public long IthdId { get; set; }

        [JsonProperty("Status")]
        public string Status { get; set; }

        [JsonProperty("MainCategory")]
        public string MainCategory { get; set; }

        [JsonProperty("Category")]
        public string Category { get; set; }

        [JsonProperty("MainArea")]
        public string MainArea { get; set; }

        [JsonProperty("Area")]
        public string Area { get; set; }

        [JsonProperty("DateFinishedTarget")]
        public DateTimeOffset? DateFinishedTarget { get; set; }

        [JsonProperty("DateEntry")]
        public DateTimeOffset? DateEntry { get; set; }

        [JsonProperty("DateFinished")]
        public DateTimeOffset? DateFinished { get; set; }

        [JsonProperty("DatePlanned")]
        public DateTimeOffset? DatePlanned { get; set; }

        [JsonProperty("LocationText")]
        public string LocationText { get; set; }

        [JsonProperty("LocationDesc")]
        public string LocationDesc { get; set; }

        [JsonProperty("LocationTown")]
        public string LocationTown { get; set; }

        [JsonProperty("CaseNumber")]
        public string CaseNumber { get; set; }

        [JsonProperty("Coordinates")]
        public Coordinates Coordinates { get; set; }

        [JsonProperty("Presentation")]
        public Presentation Presentation { get; set; }

        public Report()
        {
        }
    }
}
