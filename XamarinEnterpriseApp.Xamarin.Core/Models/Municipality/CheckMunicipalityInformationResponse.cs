using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace XamarinEnterpriseApp.Xamarin.Core.Models
{
    public class CheckMunicipalityInformationResponse : BaseResponse
    {
        [JsonProperty("Cust_id")]
        public long CustId { get; set; }

        [JsonProperty("Cust_about")]
        public string CustAbout { get; set; }

        [JsonProperty("Cust_logo")]
        public string CustLogo { get; set; }

        [JsonProperty("errorResult")]
        public ErrorResult ErrorResult { get; set; }

        public CheckMunicipalityInformationResponse()
        {

        }
    }
}
