using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using XamarinEnterpriseApp.Xamarin.Core.Constants;
using XamarinEnterpriseApp.Xamarin.Core.Ioc;
using XamarinEnterpriseApp.Xamarin.Core.Models;
using XamarinEnterpriseApp.Xamarin.Core.Repositories;
using XamarinEnterpriseApp.Xamarin.Core.Services;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace XamarinEnterpriseApp.Xamarin.Core
{
    public class GlobalSetting
    {
        public static GlobalSetting Instance { get; } = new GlobalSetting();

        public AddReportRequest AddReportRequest { get; set; }

        public AddActionRequest AddActionRequest { get; set; }

        public GetCategoryListResponse CategoryListResponse { get; set; }

        public DisplayInfo MainDisplayInfo { get; set; }

        public bool IsPickIntentOn { get; set; }

        public bool IsLoggedIn
        {
            get
            {
                ISettingsService settingsService = DependencyResolver.Resolve<ISettingsService>();

                var accessToken = settingsService.AuthAccessToken;
                var ushdId = settingsService.UshdId;
                var sessionId = settingsService.SessionId;

                return !string.IsNullOrEmpty(accessToken) && ushdId > 0 && sessionId > 0;
            }
        }

        public MapExtent MapExtent { get; internal set; }

        public CultureInfo CurrentCulture
        {
            get
            {
                return CultureInfo.CurrentCulture;
            }
        }

        public double DeviceWidth
        {
            get
            {
                double width = MainDisplayInfo.Width / (MainDisplayInfo.Density);

                return width;
            }
        }

        public double DeviceHeight
        {
            get
            {
                double height = MainDisplayInfo.Height / (MainDisplayInfo.Density);

                return height;
            }
        }

        public DisplayOrientation Orientation
        {
            get
            {
                return MainDisplayInfo.Orientation;
            }
        }

        public object NavigationParam { get; set; }

        public PushNotificationData PushNotificationData { get; set; }

        public string SearchString { get; set; }

        public bool IsPreviewOpen { get; set; }

        public MGAppConfigDataResponse MGAppConfig { get; set; }
        public int CachedDisplayOrientation { get; internal set; }

        private GlobalSetting()
        {
            MainDisplayInfo = DeviceDisplay.MainDisplayInfo;

            bool isPortrait = MainDisplayInfo.Width < MainDisplayInfo.Height;

            int orientation = isPortrait ? 1 : 2;

            CachedDisplayOrientation = orientation;
        }

        public void LoggedIn(AuthenticationResponse authResponse)
        {
            ISettingsService settingsService = DependencyResolver.Resolve<ISettingsService>();
            IAnalyticsService analyticsService = DependencyResolver.Resolve<IAnalyticsService>();
            ILocalDbContextService localDbContext = DependencyResolver.Resolve<ILocalDbContextService>();

            EnvironmentSetting selectedEndpoint = localDbContext.GetSelectedEnvironment();

            var loggedData = new Dictionary<string, string>
            {
                { "username", authResponse.UserName },
            };

            analyticsService.TrackEvent(EventNames.UserLogin, loggedData);

            settingsService.AuthAccessToken = authResponse.AccessToken;
            settingsService.UshdId = authResponse.UshdId;
            settingsService.SessionId = authResponse.SessionId;

            // Adding MDAppConfigurations into setting service
            if (authResponse.MDAppConfig != null)
            {
                settingsService.MaxNumberOfFiles = authResponse.MDAppConfig.MaxNumberOfFiles;
                settingsService.SearchMunicipality = authResponse.MDAppConfig.SearchMunicipality;
            }
            else
            {
                // Set default file count to 5 incase if configs are missing
                settingsService.MaxNumberOfFiles = 5;
            }

            if (selectedEndpoint != null)
            {
                if (authResponse.RememberCredentials)
                {
                    selectedEndpoint.UserName = authResponse.UserName;
                    selectedEndpoint.Password = authResponse.Password;
                    selectedEndpoint.RememberCredentials = true;
                }
                else
                {
                    selectedEndpoint.UserName = string.Empty;
                    selectedEndpoint.Password = string.Empty;
                    selectedEndpoint.RememberCredentials = false;
                }
            }

            localDbContext.SaveEntity(selectedEndpoint);
        }

        public void LoggedOut()
        {
            ISettingsService settingsService = DependencyResolver.Resolve<ISettingsService>();
            IAnalyticsService analyticsService = DependencyResolver.Resolve<IAnalyticsService>();
            ILocalDbContextService localDbContext = DependencyResolver.Resolve<ILocalDbContextService>();

            EnvironmentSetting selectedEndpoint = localDbContext.GetSelectedEnvironment();

            var loggedData = new Dictionary<string, string>
            {
                { "username", selectedEndpoint?.UserName },
            };

            analyticsService.TrackEvent(EventNames.UserLogout, loggedData);

            settingsService.AuthAccessToken = string.Empty;
            settingsService.UshdId = 0;
            settingsService.SessionId = 0;
        }

        public void ReportChanged(long id)
        {
            ISettingsService settingsService = DependencyResolver.Resolve<ISettingsService>();

            settingsService.SelectedReportId = id;
        }

        public bool FirePushNotificationEvent()
        {
            bool isNotified = false;

            var data = GlobalSetting.Instance.PushNotificationData;

            if (data != null && !data.IsProcessing)
            {
                if (!data.IsProcessed)
                {
                    if (data.ReportId > 0)
                    {
                        MessagingCenter.Send<GlobalSetting, PushNotificationData>(GlobalSetting.Instance, Core.Constants.MessageKeys.OpenReportRequested, data);

                        data.IsProcessing = true;

                        isNotified = true;
                    }
                }
            }

            return isNotified;
        }
    }
}
