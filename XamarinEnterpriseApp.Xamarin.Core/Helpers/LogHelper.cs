using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.AppCenter.Crashes;
using XamarinEnterpriseApp.Xamarin.Core.Constants;
using XamarinEnterpriseApp.Xamarin.Core.Ioc;
using XamarinEnterpriseApp.Xamarin.Core.Services;
using XamarinEnterpriseApp.Xamarin.Core.Repositories;
using XamarinEnterpriseApp.Xamarin.Core.Models;

namespace XamarinEnterpriseApp.Xamarin.Core.Helpers
{
    public static class LogHelper
    {
        public static void LogException(string messege, Exception ex)
        {
            try
            {
                var settingsService = DependencyResolver.Resolve<ISettingsService>();

                Dictionary<string, string> properties = new Dictionary<string, string>();

                properties.Add("BaseGatewayEndpoint", settingsService.BaseGatewayEndpoint);
                properties.Add("MunicipalityId", $"{settingsService.MunicipalityId}");
                properties.Add("SelectedLanguage", $"{settingsService.HasIntroShown}");

                Crashes.TrackError(ex, properties);

                Debug.WriteLine($"{0} exception {1}", messege, ex);
            }
            catch (Exception exInner)
            {
                Debug.WriteLine($"Exception occured while logging the exception {0}", exInner);
            }
        }

        public static void TrackEvent(string category, string message)
        {
            IAnalyticsService analyticsService = DependencyResolver.Resolve<IAnalyticsService>();

            string devicePlatform = analyticsService.DevicePlatform;
            string appVersionNumber = analyticsService.AppVersionNumber;

            var loggedData = new Dictionary<string, string>
            {
                { "username", "username" },
                { "appVersion", appVersionNumber },
                { "devicePlatform", devicePlatform },
                { "category", category },
                { "exception", message },
            };

            analyticsService.TrackEvent(category, loggedData);

        }
    }
}
