using XamarinEnterpriseApp.Xamarin.Core.Converters.JsonConverters;
using Newtonsoft.Json;

namespace XamarinEnterpriseApp.Xamarin.Core.Models
{
    public class ActionRight
    {
        [JsonProperty("Athd_id")]
        [JsonConverter(typeof(ParseNumberConverter))]
        public long AthdId { get; set; }

        [JsonProperty("Ushd_ind_view_relations")]
        public string UshdIndViewRelations { get; set; }

        [JsonProperty("Ushd_ind_view_gba")]
        public string UshdIndViewGba { get; set; }

        [JsonProperty("Ushd_ind_view_bsn")]
        public string UshdIndViewBsn { get; set; }

        [JsonProperty("Ushd_ind_modify_relations")]
        public string UshdIndModifyRelations { get; set; }

        [JsonProperty("Assl_ind_update")]
        public string AsslIndUpdate { get; set; }

        [JsonProperty("Assl_ind_delete")]
        public string AsslIndDelete { get; set; }

        [JsonProperty("Usfu_ind_select")]
        public string UsfuIndSelect { get; set; }

        [JsonProperty("Usfu_ind_owner")]
        public string UsfuIndOwner { get; set; }

        [JsonProperty("Usfu_ind_delete")]
        public string UsfuIndDelete { get; set; }

        [JsonProperty("Usfu_update_type")]
        public string UsfuUpdateType { get; set; }

        [JsonProperty("Usfu_ind_upd_owner")]
        public string UsfuIndUpdOwner { get; set; }

        [JsonProperty("Usfu_ind_upd_status")]
        public string UsfuIndUpdStatus { get; set; }

        [JsonProperty("Usfu_ind_upd_text")]
        public string UsfuIndUpdText { get; set; }

        [JsonProperty("Usfu_ind_upd_type")]
        public string UsfuIndUpdType { get; set; }

        [JsonProperty("Usfu_ind_upd_date_entry")]
        public string UsfuIndUpdDateEntry { get; set; }

        [JsonProperty("Usfu_ind_upd_date_finished")]
        public string UsfuIndUpdDateFinished { get; set; }

        [JsonProperty("Usfu_ind_upd_date_finished_t")]
        public string UsfuIndUpdDateFinishedT { get; set; }
    }
}