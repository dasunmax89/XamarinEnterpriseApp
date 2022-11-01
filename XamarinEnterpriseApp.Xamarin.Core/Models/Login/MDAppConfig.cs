using System;
using System.Collections.Generic;
using XamarinEnterpriseApp.Xamarin.Core.Converters;
using XamarinEnterpriseApp.Xamarin.Core.Converters.JsonConverters;
using Newtonsoft.Json;

namespace XamarinEnterpriseApp.Xamarin.Core.Models
{
    public partial class MDAppConfig
    {
        [JsonProperty("SearchMunicipality")]
        public string SearchMunicipality { get; set; }

        [JsonProperty("AllowCloseOnOpenActions")]
        public bool AllowCloseOnOpenActions { get; set; }

        [JsonProperty("MaxNumberOfFiles")]
        public long MaxNumberOfFiles { get; set; }
    }
}
