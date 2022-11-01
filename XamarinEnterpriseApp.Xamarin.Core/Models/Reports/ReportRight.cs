using Newtonsoft.Json;

namespace XamarinEnterpriseApp.Xamarin.Core.Models
{
    public class ReportRight
    {
        [JsonProperty("Ithd_id")]
        public long IthdId { get; set; }

        [JsonProperty("Ushd_ind_view_relations")]
        public string UshdIndViewRelations { get; set; }

        [JsonProperty("Ushd_ind_view_gba")]
        public string UshdIndViewGba { get; set; }

        [JsonProperty("Ushd_ind_view_bsn")]
        public string UshdIndViewBsn { get; set; }

        [JsonProperty("Ushd_ind_modify_relations")]
        public object UshdIndModifyRelations { get; set; }

        [JsonProperty("Issl_ind_update")]
        public string IsslIndUpdate { get; set; }

        [JsonProperty("Issl_ind_delete")]
        public string IsslIndDelete { get; set; }

        [JsonProperty("Usde_ind_delete")]
        public string UsdeIndDelete { get; set; }

        [JsonProperty("Usde_ind_view_reporters")]
        public string UsdeIndViewReporters { get; set; }

        [JsonProperty("Usde_ind_view_offenders")]
        public string UsdeIndViewOffenders { get; set; }

        [JsonProperty("Usde_update_type")]
        public string UsdeUpdateType { get; set; }

        [JsonProperty("Usde_ind_upd_category")]
        public string UsdeIndUpdCategory { get; set; }

        [JsonProperty("Usde_ind_upd_area")]
        public string UsdeIndUpdArea { get; set; }

        [JsonProperty("Usde_ind_upd_text")]
        public string UsdeIndUpdText { get; set; }

        [JsonProperty("Usde_ind_upd_owner")]
        public string UsdeIndUpdOwner { get; set; }

        [JsonProperty("Usde_ind_upd_date_entry")]
        public string UsdeIndUpdDateEntry { get; set; }

        [JsonProperty("Usde_ind_upd_date_finished")]
        public string UsdeIndUpdDateFinished { get; set; }

        [JsonProperty("Usde_ind_upd_date_finished_t")]
        public string UsdeIndUpdDateFinishedT { get; set; }

        [JsonProperty("Usde_ind_upd_status")]
        public string UsdeIndUpdStatus { get; set; }

        [JsonProperty("Usde_ind_upd_properties")]
        public string UsdeIndUpdProperties { get; set; }

        [JsonProperty("Usde_ind_del_mail")]
        public string UsdeIndDelMail { get; set; }

        [JsonProperty("Usde_ind_owner_selector")]
        public string UsdeIndOwnerSelector { get; set; }

        [JsonProperty("Usde_ind_owner_delete")]
        public string UsdeIndOwnerDelete { get; set; }

        [JsonProperty("Usde_ind_intake_risk")]
        public string UsdeIndIntakeRisk { get; set; }

        [JsonProperty("Usde_ind_view_reporter_bsn")]
        public string UsdeIndViewReporterBsn { get; set; }
    }
}