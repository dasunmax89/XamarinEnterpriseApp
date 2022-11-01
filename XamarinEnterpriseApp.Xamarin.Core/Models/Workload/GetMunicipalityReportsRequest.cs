using System;
using Newtonsoft.Json;

namespace XamarinEnterpriseApp.Xamarin.Core.Models
{
    public class GetMunicipalityReportsRequest : BaseRequest
    {
        public string SearchString { get; set; }

        public GetMunicipalityReportsRequest()
        {

        }
    }
}
