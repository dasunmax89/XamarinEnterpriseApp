using System;
namespace XamarinEnterpriseApp.Xamarin.Core.Models
{
    public class PushNotificationData
    {
        public long ReportId { get; set; }

        public long CustId { get; set; }

        public string Body { get; set; }

        public string Title { get; set; }

        public bool IsProcessed { get; set; }

        public bool IsProcessing { get; set; }
    }
}
