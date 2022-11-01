using System;
using Newtonsoft.Json;

namespace XamarinEnterpriseApp.Xamarin.Core.Models
{
    public class GetReportersRequest
    {
        public long ReporterId { get; set; }

        public long SessionId { get; set; }
    }
}
