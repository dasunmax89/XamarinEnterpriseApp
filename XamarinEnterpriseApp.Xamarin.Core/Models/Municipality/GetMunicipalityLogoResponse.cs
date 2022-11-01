using System;
using System.Collections.Generic;
using Newtonsoft.Json;
namespace XamarinEnterpriseApp.Xamarin.Core.Models
{
    public class GetMunicipalityLogoResponse : BaseResponse
    {
        [JsonProperty("Cust_id")]
        public long CustId { get; set; }

        [JsonProperty("Cust_logo")]
        public string CustLogo { get; set; }

        public GetMunicipalityLogoResponse()
        {
        }
    }
}
