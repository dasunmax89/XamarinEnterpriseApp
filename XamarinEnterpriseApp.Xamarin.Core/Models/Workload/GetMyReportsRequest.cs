using System;
using Newtonsoft.Json;

namespace XamarinEnterpriseApp.Xamarin.Core.Models
{
    public class GetMyReportsRequest : BaseRequest
    {
        public string SearchString { get; set; }

        public GetMyReportsRequest()
        {

        }
    }
}
