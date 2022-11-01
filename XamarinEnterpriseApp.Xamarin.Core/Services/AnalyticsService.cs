using System;
using System.Collections.Generic;
using System.Diagnostics;
using XamarinEnterpriseApp.Xamarin.Core.Dependency;
using Microsoft.AppCenter.Analytics;
using Xamarin.Forms;

namespace XamarinEnterpriseApp.Xamarin.Core.Services
{
    public interface IAnalyticsService
    {
        string DevicePlatform { get; }
        string AppVersionNumber { get; }
        void TrackEvent(string eventName, IDictionary<string, string> properties = null);
    }

    public class AnalyticsService : IAnalyticsService
    {
        public void TrackEvent(string eventName, IDictionary<string, string> properties = null)
        {
            try
            {
                if (!properties.ContainsKey("appVersion"))
                {
                    properties.Add("appVersion", AppVersionNumber);
                }

                if (!properties.ContainsKey("devicePlatform"))
                {
                    properties.Add("devicePlatform", DevicePlatform);
                }

                Analytics.TrackEvent(eventName, properties);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception occured while logging the analytics {0}", ex);
            }
        }

        public string DevicePlatform
        {
            get
            {
                string devicePlatform = Device.RuntimePlatform == Device.Android ? "android" : "ios";
                return devicePlatform;
            }
        }

        public string AppVersionNumber
        {
            get
            {
                string appVersionNumber = DependencyService.Get<IVersionManager>().VersionNumber();
                return appVersionNumber;
            }
        }
    }
}
