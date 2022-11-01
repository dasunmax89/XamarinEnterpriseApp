using System;
using XamarinEnterpriseApp.Xamarin.Core.Converters.JsonConverters;
using Newtonsoft.Json;

namespace XamarinEnterpriseApp.Xamarin.Core.Models
{
    public class ActionState
    {
        [JsonProperty("Astt_id")]
        [JsonConverter(typeof(ParseNumberConverter))]
        public long AsttId { get; set; }

        [JsonProperty("Astt_label")]
        public string AsttLabel { get; set; }

        [JsonProperty("Astt_hint")]
        public string AsttHint { get; set; }

        [JsonProperty("Astt_statetype")]
        public string AsttStatetype { get; set; }

        public string GetIconSource()
        {
            string iconSource = string.Empty;

            if (AsttId == 1)
            {
                iconSource = "Checkmark_green.png";
            }
            else
            {
                iconSource = "Action.png";
            }

            return iconSource;
        }
    }
}