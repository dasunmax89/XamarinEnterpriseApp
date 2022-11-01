using System;
using System.Collections.Generic;
using System.Linq;
using XamarinEnterpriseApp.Xamarin.Core.Converters.JsonConverters;
using XamarinEnterpriseApp.Xamarin.Core.Enums;
using Newtonsoft.Json;

namespace XamarinEnterpriseApp.Xamarin.Core.Models
{
    public class AddReportRequest : BaseRequest
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

        [JsonProperty("DeviceId")]
        public string DeviceId { get; set; }

        public long SessionId { get; set; }

        [JsonIgnore]
        public GetReportDataResponse ReportData { get; set; }

        [JsonIgnore]
        private Category _selectedCategory;
        [JsonIgnore]
        public Category SelectedCategory
        {
            get
            {
                return _selectedCategory;
            }

            set
            {
                bool clearGisData = ValidateGIS(value);

                if (clearGisData)
                {
                    GisData = new GisData();
                }

                _selectedCategory = value;
            }
        }

        [JsonIgnore]
        public bool IsGisObjectAvailable
        {
            get
            {
                return GisData != null && GisData.GisItems != null && GisData.GisItems.Any();
            }
        }

        [JsonIgnore]
        public bool IsMapLayerAvailable
        {
            get
            {
                return SelectedCategory != null && SelectedCategory.IsMapLayerAvailable;
            }
        }

        [JsonIgnore]
        public bool IsReportEditMode { get; set; }

        public AddReportRequest()
        {
            ReportFiles = new ReportFiles();

            Reporters = new Reporters();
        }

        public bool GetRight(string grantType)
        {
            bool isGranted = false;

            ReportRight reportRight = ReportData?.ItemRights?.FirstOrDefault();

            if (reportRight != null)
            {
                bool isUniversallyGranted = reportRight.IsslIndUpdate == "Y";

                if (isUniversallyGranted)
                {
                    switch (grantType)
                    {
                        case WellknownReportRight.CHANGE_REPORT_TYPE:
                            isGranted = reportRight.UsdeUpdateType == "Y";
                            break;
                        case WellknownReportRight.CHANGE_REPORT_OWNER:
                            isGranted = reportRight.UsdeIndUpdOwner == "Y";
                            break;
                        case WellknownReportRight.CHANGE_REPORT_STATUS:
                            isGranted = reportRight.UsdeIndUpdStatus == "Y";
                            break;
                        case WellknownReportRight.CHANGE_DESCRIPTION:
                            isGranted = reportRight.UsdeIndUpdText == "Y";
                            break;
                        case WellknownReportRight.CHANGE_DUE_DATE:
                            isGranted = reportRight.UsdeIndUpdDateFinished == "Y";
                            break;
                        case WellknownReportRight.CHANGE_FINAL_DUE_DATE:
                            isGranted = reportRight.UsdeIndUpdDateFinishedT == "Y";
                            break;
                        case WellknownReportRight.CHANGE_ARRIVAL_DATE:
                            isGranted = reportRight.UsdeIndUpdDateEntry == "Y";
                            break;
                        case WellknownReportRight.CHANGE_REPORTING_METHOD:
                            isGranted = true;
                            break;
                        case WellknownReportRight.CHANGE_AREA:
                            isGranted = reportRight.UsdeIndUpdArea == "Y";
                            break;
                        case WellknownReportRight.CHANGE_CATEGORY:
                            isGranted = reportRight.UsdeIndUpdCategory == "Y";
                            break;
                        case WellknownReportRight.CHANGE_PROPERTIES:
                            isGranted = reportRight.UsdeIndUpdProperties == "Y";
                            break;
                        case WellknownReportRight.VIEW_REPORTER:
                            isGranted = reportRight.UsdeIndViewReporters == "Y";
                            break;
                        case WellknownReportRight.CHANGE_PLANNED_DATE:
                            isGranted = true;
                            break;
                    }
                }
                else
                {
                    if (IthdId == 0)
                    {
                        isGranted = true;
                    }
                }
            }

            return isGranted;
        }

        public static AddReportRequest CreateNewReportRequest()
        {
            AddReportRequest addreportRequest = new AddReportRequest()
            {

            };

            return addreportRequest;
        }

        internal bool ValidateGIS(Category newCategory)
        {
            if (_selectedCategory == null || newCategory == null)
            {
                return true;
            }

            bool isCategoriesDifferent = _selectedCategory.CahdId != newCategory.CahdId;

            if (!_selectedCategory.IsMapLayerAvailable && !newCategory.IsMapLayerAvailable)
            {
                isCategoriesDifferent = false;
            }

            return isCategoriesDifferent;
        }

        public void ValidateProperties()
        {
            bool isPropsEditingGranted = GetRight(WellknownReportRight.CHANGE_PROPERTIES);

            if (Properties != null && Properties.Props != null)
            {
                foreach (var prop in Properties.Props)
                {
                    prop.IsReadOnly = !isPropsEditingGranted;

                    prop.PropertyType = ReportData.PropertieTypes?.PropertieTypes?.FirstOrDefault(x => x.Id == prop.PrhdPthdId);

                    if (prop.PropertyType == null)
                    {
                        prop.PropertyType = PropertieType.GetDefault(prop.PrhdPthdId);
                    }
                }
            }
        }

        public void InvalidateGisObjects()
        {
            if (IsGisObjectAvailable)
            {
                foreach (var gisItem in GisData.GisItems)
                {
                    gisItem.State = "DELETED";
                }
            }
        }

        public List<GetActionDetailsResponse> GetUnfinishedActions()
        {
            List<GetActionDetailsResponse> actions = new List<GetActionDetailsResponse>();

            foreach (var actionItem in ReportActions?.Actions)
            {
                if (actionItem.AchdId > 0)
                {
                    ActionState actionState = actionItem.GetState(actionItem.AchdAsttId);

                    if (actionState.AsttStatetype != WellknownActionStateType.ACTION_FINISHED)
                    {
                        actions.Add(actionItem);
                    }
                }
            }

            return actions;
        }
    }
}
