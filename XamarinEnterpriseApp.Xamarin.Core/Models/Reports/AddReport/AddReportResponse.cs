using System;
using XamarinEnterpriseApp.Xamarin.Core.Converters.JsonConverters;
using Newtonsoft.Json;

namespace XamarinEnterpriseApp.Xamarin.Core.Models
{
    public class AddReportResponse : BaseResponse
    {
        [JsonProperty("Ithd_id")]
        [JsonConverter(typeof(ParseNumberConverter))]
        public long IthdId { get; set; }

        [JsonProperty("Ithd_istt_id")]
        [JsonConverter(typeof(ParseNumberConverter))]
        public long IthdIsttId { get; set; }

        [JsonProperty("Ithd_ushd_id")]
        [JsonConverter(typeof(ParseNumberConverter))]
        public long IthdUshdId { get; set; }

        [JsonProperty("Ithd_arhd_id")]
        [JsonConverter(typeof(ParseNumberConverter))]
        public long IthdArhdId { get; set; }

        [JsonProperty("Ithd_cahd_id")]
        [JsonConverter(typeof(ParseNumberConverter))]
        public long IthdCahdId { get; set; }

        [JsonProperty("Cahd_label")]
        public string IthdCahdLabel { get; set; }

        [JsonProperty("Ithd_dehd_id")]
        [JsonConverter(typeof(ParseNumberConverter))]
        public long IthdDehdId { get; set; }

        [JsonProperty("Ithd_alhd_id")]
        [JsonConverter(typeof(ParseNumberConverter))]
        public long IthdAlhdId { get; set; }

        [JsonProperty("Ithd_blhd_id")]
        public string IthdBlhdId { get; set; }

        [JsonProperty("Ithd_clhd_id")]
        public string IthdClhdId { get; set; }

        [JsonProperty("Ithd_text")]
        public string IthdText { get; set; }

        [JsonProperty("Ithd_location_town")]
        public string IthdLocationTown { get; set; }

        [JsonProperty("Ithd_location_desc")]
        public string IthdLocationDesc { get; set; }

        [JsonProperty("Ithd_location_text")]
        public string IthdLocationText { get; set; }

        [JsonProperty("Coordinates")]
        public Coordinates Coordinates { get; set; }

        [JsonProperty("Ithd_date_entry")]
        public DateTimeOffset? IthdDateEntry { get; set; }

        [JsonProperty("Ithd_date_finished")]
        public DateTimeOffset? IthdDateFinished { get; set; }

        [JsonProperty("Ithd_date_finished_target")]
        public DateTimeOffset? IthdDateFinishedTarget { get; set; }

        [JsonProperty("Ithd_creation_ushd_id")]
        [JsonConverter(typeof(ParseNumberConverter))]
        public long IthdCreationUshdId { get; set; }

        [JsonProperty("Ithd_ithd_id")]
        public string IthdIthdId { get; set; }

        [JsonProperty("Ithd_zaaknummer")]
        public string IthdZaaknummer { get; set; }

        [JsonProperty("Ithd_location_code")]
        public string IthdLocationCode { get; set; }

        [JsonProperty("Ithd_date_planned")]
        public DateTimeOffset? IthdDatePlanned { get; set; }

        [JsonProperty("Ithd_creation_date")]
        public DateTimeOffset? IthdCreationDate { get; set; }

        [JsonProperty("Ithd_log")]
        public string IthdLog { get; set; }

        [JsonProperty("Ithd_mailed")]
        public string IthdMailed { get; set; }

        [JsonProperty("Ithd_risk")]
        public string IthdRisk { get; set; }

        [JsonProperty("Ithd_bag_id")]
        public string IthdBagId { get; set; }

        [JsonProperty("Ithd_ibs_gis_strings_id")]
        public string IthdIbsGisstringsId { get; set; }

        [JsonProperty("Ithd_oshp_id")]
        public string IthdOshpId { get; set; }

        [JsonProperty("Ithd_zkn_identification")]
        public string IthdZknIdentification { get; set; }

        [JsonProperty("Ithd_date_was_planned")]
        public string IthdDateWasPlanned { get; set; }

        [JsonProperty("Ithd_item_exists")]
        public string IthdItemExists { get; set; }

        [JsonProperty("Ithd_obir_id")]
        public string IthdObirId { get; set; }

        [JsonProperty("Ithd_devicetoken")]
        public string IthdDevicetoken { get; set; }

        [JsonProperty("Ithd_nodr_ref")]
        public string IthdNodrRef { get; set; }

        [JsonProperty("State")]
        public string State { get; set; }

        [JsonProperty("ReportActions")]
        public ReportActionsData ReportActions { get; set; }

        [JsonProperty("Reporters")]
        public Reporters Reporters { get; set; }

        [JsonProperty("Properties")]
        public Properties Properties { get; set; }

        [JsonProperty("ReportFiles")]
        public ReportFiles ReportFiles { get; set; }

        [JsonProperty("GisItems")]
        public GisData GisData { get; set; }

        [JsonProperty("Result")]
        public APIResult Result { get; set; }
    }
}
