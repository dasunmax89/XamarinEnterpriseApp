using System;
using Newtonsoft.Json;

namespace XamarinEnterpriseApp.Xamarin.Core.Models
{
    public class GetOffendersRequest
    {
        public long OffenderId { get; set; }

        public long SessionId { get; set; }
    }
}
