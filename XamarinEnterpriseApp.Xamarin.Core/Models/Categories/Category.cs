using XamarinEnterpriseApp.Xamarin.Core.Converters;
using XamarinEnterpriseApp.Xamarin.Core.Converters.JsonConverters;
using Newtonsoft.Json;

namespace XamarinEnterpriseApp.Xamarin.Core.Models
{
    public class Category
    {
        [JsonProperty("Cahd_id")]
        public long CahdId { get; set; }

        [JsonProperty("Cahd_label")]
        public string CahdLabel { get; set; }

        [JsonProperty("Cahd_hint")]
        public string CahdHint { get; set; }

        [JsonProperty("Cahd_ex_hint")]
        public string CahdExHint { get; set; }

        [JsonProperty("IsObjectCategory")]
        [JsonConverter(typeof(ParseBoolConverter))]
        public bool IsObjectCategory { get; set; }

        [JsonProperty("Cahd_prgr_id")]
        public long? CahdPrgrId { get; set; }

        [JsonProperty("Cahd_maplayer_def")]
        [JsonConverter(typeof(MapLayerConverter))]
        public CahdMapLayerDef CahdMaplayerDef { get; set; }

        [JsonProperty("Normtime")]
        public string Normtime { get; set; }

        [JsonProperty("Allow_anonymous")]
        public string AllowAnonymous { get; set; }

        [JsonProperty("DynamicRelationFields")]
        public DynamicRelationFields DynamicRelationFields { get; set; }

        [JsonIgnore]
        public long MacaId { get; set; }

        [JsonIgnore]
        public long SucaId { get; set; }

        [JsonIgnore]
        public bool IsMapLayerAvailable
        {
            get
            {
                return CahdMaplayerDef != null;
            }
        }

        [JsonIgnore]
        public string MacaHint { get; set; }

        [JsonIgnore]
        public string MacaExHint { get; set; }

        public Category()
        {

        }
    }
}