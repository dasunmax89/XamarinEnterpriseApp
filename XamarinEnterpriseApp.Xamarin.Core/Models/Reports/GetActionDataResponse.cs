using System;
using System.Collections.Generic;
using XamarinEnterpriseApp.Xamarin.Core.Converters.JsonConverters;
using Newtonsoft.Json;

namespace XamarinEnterpriseApp.Xamarin.Core.Models
{
    public class GetActionDataResponse : BaseResponse
    {
        [JsonProperty("Achd_id")]
        [JsonConverter(typeof(ParseNumberConverter))]
        public long AchdId { get; set; }

        [JsonProperty("Achd_astt_id")]
        [JsonConverter(typeof(ParseNumberConverter))]
        public long AchdAsttId { get; set; }

        [JsonProperty("Achd_ushd_id")]
        [JsonConverter(typeof(ParseNumberConverter))]
        public long AchdUshdId { get; set; }

        [JsonProperty("Achd_athd_id")]
        [JsonConverter(typeof(ParseNumberConverter))]
        public long AchdAthdId { get; set; }

        [JsonProperty("Achd_date_finished_target")]
        public DateTimeOffset AchdDateFinishedTarget { get; set; }

        [JsonProperty("ActionOwner")]
        public ActionOwner ActionOwner { get; set; }

        [JsonProperty("ActionCandidates")]
        public ActionCandidates ActionCandidates { get; set; }

        [JsonProperty("ActionTypes")]
        public ActionTypes ActionTypes { get; set; }

        [JsonProperty("ActionStates")]
        public ActionStates ActionStates { get; set; }

        [JsonProperty("ActionRights")]
        public List<ActionRight> ActionRights { get; set; }

        public static GetActionDataResponse GetDefault()
        {
            GetActionDataResponse getActionDataResponse = new GetActionDataResponse();

            return getActionDataResponse;
        }
    }
}
