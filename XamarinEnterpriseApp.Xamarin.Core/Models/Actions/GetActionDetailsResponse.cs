using System;
using System.Collections.Generic;
using System.Linq;
using XamarinEnterpriseApp.Xamarin.Core.Converters.JsonConverters;
using XamarinEnterpriseApp.Xamarin.Core.Enums;
using Newtonsoft.Json;

namespace XamarinEnterpriseApp.Xamarin.Core.Models
{
    public class GetActionDetailsResponse : BaseResponse
    {
        [JsonProperty("Achd_id")]
        [JsonConverter(typeof(ParseNumberConverter))]
        public long AchdId { get; set; }

        [JsonProperty("Achd_athd_id")]
        [JsonConverter(typeof(ParseNumberConverter))]
        public long AchdAthdId { get; set; }

        [JsonProperty("Achd_astt_id")]
        [JsonConverter(typeof(ParseNumberConverter))]
        public long AchdAsttId { get; set; }

        [JsonProperty("Achd_ushd_id")]
        [JsonConverter(typeof(ParseNumberConverter))]
        public long AchdUshdId { get; set; }

        [JsonProperty("Achd_ithd_id")]
        [JsonConverter(typeof(ParseNumberConverter))]
        public long AchdIthdId { get; set; }

        [JsonProperty("Achd_text")]
        public string AchdText { get; set; }

        [JsonProperty("Achd_date_entry")]
        public DateTimeOffset? AchdDateEntry { get; set; }

        [JsonProperty("Achd_date_finished")]
        public DateTimeOffset? AchdDateFinished { get; set; }

        [JsonProperty("Achd_date_finished_target")]
        public DateTimeOffset? AchdDateFinishedTarget { get; set; }

        [JsonProperty("Achd_rehd_id")]
        [JsonConverter(typeof(ParseNumberConverter))]
        public long AchdRehdId { get; set; }

        [JsonProperty("Achd_rehd_memo")]
        public string AchdRehdMemo { get; set; }

        [JsonProperty("Achd_rehd_date_entry")]
        public DateTimeOffset? AchdRehdDateEntry { get; set; }

        [JsonProperty("Achd_date_planned")]
        public DateTimeOffset? AchdDatePlanned { get; set; }

        [JsonProperty("Achd_creation_user")]
        [JsonConverter(typeof(ParseNumberConverter))]
        public long AchdCreationUser { get; set; }

        [JsonProperty("Achd_creation_date")]
        public DateTimeOffset? AchdCreationDate { get; set; }

        [JsonProperty("ActionData")]
        public GetActionDataResponse ActionData { get; set; }

        [JsonProperty("State")]
        public string State { get; set; }

        [JsonProperty("Result")]
        public APIResult Result { get; set; }

        public ActionState GetState(long achd_astt_id)
        {
            ActionState actionState = ActionData?.ActionStates?.ActionStatesList?.FirstOrDefault(x => x.AsttId == achd_astt_id);

            return actionState;
        }

        public ActionOwner GetOwner()
        {
            ActionOwner actionOwner = ActionData?.ActionOwner;

            return actionOwner;
        }

        public ActionType GetActionType()
        {
            ActionType actionState = ActionData?.ActionTypes?.Actiontypes?.FirstOrDefault(x => x.AthdId == AchdAthdId);

            return actionState;
        }

        public bool GetRight(string grantType)
        {
            bool isGranted = false;

            ActionRight actionRight = ActionData?.ActionRights?.FirstOrDefault();

            if (actionRight != null)
            {
                switch (grantType)
                {
                    case WellknownActionRight.CHANGE_ACTIONTYPE:
                        isGranted = actionRight.UsfuIndUpdType == "Y";
                        break;
                    case WellknownActionRight.CHANGE_ACTION_OWNER:
                        isGranted = actionRight.UsfuIndUpdOwner == "Y";
                        break;
                    case WellknownActionRight.CHANGE_ACTION_STATUS:
                        isGranted = actionRight.UsfuIndUpdStatus == "Y";
                        break;
                    case WellknownActionRight.CHANGE_DESCRIPTION:
                        isGranted = actionRight.UsfuIndUpdText == "Y";
                        break;
                    case WellknownActionRight.CHANGE_DUE_DATE:
                        isGranted = actionRight.UsfuIndUpdDateFinished == "Y";
                        break;
                    case WellknownActionRight.CHANGE_REPORTING_METHOD:
                        isGranted = true;
                        break;
                    case WellknownActionRight.CHANGE_ARRIVAL_DATE:
                        isGranted = actionRight.UsfuIndUpdDateEntry == "Y";
                        break;
                    case WellknownActionRight.CHANGE_FINAL_DUE_DATE:
                        isGranted = actionRight.UsfuIndUpdDateFinishedT == "Y";
                        break;
                    case WellknownActionRight.CHANGE_PLANNED_DATE:
                        isGranted = true;
                        break;
                }
            }
            else
            {
                if (AchdId == 0)
                {
                    isGranted = true;
                }
            }

            return isGranted;
        }

        public IEnumerable<ActionType> GetActionTypes()
        {
            List<ActionType> list = new List<ActionType>();

            list = ActionData?.ActionTypes?.Actiontypes;

            if (list == null)
            {
                list = new List<ActionType>();
            }

            return list;

        }

        public IEnumerable<ActionOwner> GetOwners()
        {
            List<ActionOwner> list = new List<ActionOwner>();

            list = ActionData?.ActionCandidates?.Owners;

            if (list == null)
            {
                list = new List<ActionOwner>();
            }

            return list;
        }

        public IEnumerable<ActionState> GetStates()
        {
            List<ActionState> list = new List<ActionState>();

            list = ActionData?.ActionStates?.ActionStatesList;

            if (list == null)
            {
                list = new List<ActionState>();
            }

            return list;
        }
    }
}
