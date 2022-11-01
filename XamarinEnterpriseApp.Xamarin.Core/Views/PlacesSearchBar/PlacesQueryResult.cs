using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace XamarinEnterpriseApp.Xamarin.Core.Views
{
    public class PlacesQueryResult
    {
        [JsonProperty("error_message")]
        public string ErrorMessage { get; set; }

        [JsonProperty("html_attributions")]
        public List<object> HtmlAttributions { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }
    }
}
