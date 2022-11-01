using System;
using XamarinEnterpriseApp.Xamarin.Core.Models.Base;
using Newtonsoft.Json;

namespace XamarinEnterpriseApp.Xamarin.Core.Models
{
    public class Language : ISelectable
    {
        [JsonProperty("Lahd_id")]
        public long LahdId { get; set; }

        [JsonProperty("Lahd_code")]
        public string LahdCode { get; set; }

        [JsonIgnore]
        public bool IsSelected { get; set; }
    }
}
