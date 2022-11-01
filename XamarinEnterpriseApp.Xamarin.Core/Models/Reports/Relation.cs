using System;
using System.Collections.Generic;
using XamarinEnterpriseApp.Xamarin.Core.Converters.JsonConverters;
using XamarinEnterpriseApp.Xamarin.Core.Localization;
using Newtonsoft.Json;

namespace XamarinEnterpriseApp.Xamarin.Core.Models
{
    public class Relation
    {
        [JsonProperty("Rehd_id")]
        [JsonConverter(typeof(ParseNumberConverter))]
        public long RehdId { get; set; }

        [JsonProperty("Rehd_type")]
        public string RehdType { get; set; }

        [JsonProperty("Rehd_initials")]
        public string RehdInitials { get; set; }

        [JsonProperty("Rehd_prefix")]
        public string RehdMiddlename { get; set; }

        [JsonProperty("Rehd_lastname")]
        public string RehdLastname { get; set; }

        [JsonProperty("Rehd_street")]
        public string RehdStreet { get; set; }

        [JsonProperty("Rehd_zipcode")]
        public string RehdZipcode { get; set; }

        [JsonProperty("Rehd_residence")]
        public string RehdResidence { get; set; }

        [JsonProperty("Rehd_housenr")]
        public string RehdHousenr { get; set; }

        [JsonProperty("Rehd_houseletter")]
        public string RehdHouseletter { get; set; }

        [JsonProperty("Rehd_housesuffix")]
        public string RehdHousesuffix { get; set; }

        [JsonProperty("Rehd_phone")]
        public string RehdPhone { get; set; }

        [JsonProperty("Rehd_fax")]
        public string RehdFax { get; set; }

        [JsonProperty("Rehd_mobile")]
        public string RehdMobile { get; set; }

        [JsonProperty("Rehd_email")]
        public string RehdEmail { get; set; }

        [JsonProperty("Rehd_bsn")]
        public string RehdBsn { get; set; }

        [JsonProperty("Rehd_feedback")]
        public string ExpectFeedback { get; set; }

        [JsonProperty("Rehd_gender")]
        public string Gender { get; set; }

        [JsonProperty("State")]
        public string State { get; set; }

        public List<ListItemModel> GetInfoList()
        {
            List<ListItemModel> userData = new List<ListItemModel>();

            return userData;
        }

        public static List<ListItemModel> GetEmptyInfoList()
        {
            List<ListItemModel> userData = new List<ListItemModel>();

            return userData;
        }
    }
}
