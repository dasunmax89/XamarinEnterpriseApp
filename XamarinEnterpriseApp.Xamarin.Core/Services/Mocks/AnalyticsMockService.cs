using System;
using System.Collections.Generic;

namespace XamarinEnterpriseApp.Xamarin.Core.Services.Mocks
{
    public class AnalyticsMockService : IAnalyticsService
    {
        public AnalyticsMockService()
        {
            _appVersionNumberInternal = "1.0.0";
            _devicePlatformInternal = "ios";
        }

        private static string _devicePlatformInternal { get; set; }
        public string DevicePlatform
        {
            get
            {
                return _devicePlatformInternal;
            }
        }

        private static string _appVersionNumberInternal { get; set; }
        public string AppVersionNumber
        {
            get
            {
                return _appVersionNumberInternal;
            }
        }

        public void TrackEvent(string eventName, IDictionary<string, string> properties = null)
        {

        }
    }
}
