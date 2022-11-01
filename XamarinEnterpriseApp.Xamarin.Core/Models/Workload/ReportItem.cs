using System;
using Newtonsoft.Json;

namespace XamarinEnterpriseApp.Xamarin.Core.Models
{
    public class ReportItem
    {
        [JsonProperty("Ithd_id")]
        public long IthdId { get; set; }

        [JsonProperty("Status")]
        public string Status { get; set; }

        [JsonProperty("Statetype")]
        public string Statetype { get; set; }

        [JsonProperty("Category")]
        public string Category { get; set; }

        [JsonProperty("DateEntry")]
        public DateTimeOffset DateEntry { get; set; }

        [JsonProperty("Location")]
        public string Location { get; set; }

        [JsonProperty("Coordinates")]
        public Coordinates Coordinates { get; set; }

        [JsonProperty("Reporter_count")]
        public long ReporterCount { get; set; }

        [JsonIgnore]
        public int StatusSort
        {
            get
            {
                int statusVal = 9;

                if (Statetype == "ITEM_RECEIVED")
                {
                    statusVal = 1;
                }
                else if (Statetype == "ITEM_ACCEPTED")
                {
                    statusVal = 2;
                }
                else if (Statetype == "ITEM_PLANNED")
                {
                    statusVal = 3;
                }
                else if (Statetype == "ITEM_PROGRESSING")
                {
                    statusVal = 4;
                }
                else if (Statetype == "ITEM_FINISHED")
                {
                    statusVal = 5;
                }
                else if (Statetype == "ITEM_FORWARDED")
                {
                    statusVal = 6;
                }
                else if (Statetype == "ITEM_LINKED")
                {
                    statusVal = 7;
                }
                else if (Statetype == "ITEM_ABORTED")
                {
                    statusVal = 8;
                }

                return statusVal;
            }
        }
    }
}