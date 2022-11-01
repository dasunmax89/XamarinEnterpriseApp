using Newtonsoft.Json;

namespace XamarinEnterpriseApp.Xamarin.Core.Models
{
    public class GisItem
    {
        [JsonProperty("Gisi_id")]
        public long GisiId { get; set; }

        [JsonProperty("Gisi_ithd_id")]
        public long GisiIthdId { get; set; }

        [JsonProperty("Gisi_vendor")]
        public string GisiVendor { get; set; }

        [JsonProperty("Gisi_object")]
        public string GisiObject { get; set; }

        [JsonProperty("Gisi_coord_x")]
        public long GisiCoordX { get; set; }

        [JsonProperty("Gisi_coord_y")]
        public long GisiCoordY { get; set; }

        [JsonProperty("Coordinates")]
        public Coordinates Coordinates { get; set; }

        [JsonProperty("State")]
        public string State { get; set; }

        [JsonIgnore]
        public string Caption { get; set; }

    }
}