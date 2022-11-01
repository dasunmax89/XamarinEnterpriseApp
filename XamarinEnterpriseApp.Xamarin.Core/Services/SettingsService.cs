using System;
using System.Threading.Tasks;
using XamarinEnterpriseApp.Xamarin.Core.Dependency;
using XamarinEnterpriseApp.Xamarin.Core.Helpers;
using Xamarin.Forms;
using Xamarin.Essentials;

namespace XamarinEnterpriseApp.Xamarin.Core.Services
{
    public interface ISettingsService
    {
        string AuthAccessToken { get; set; }
        string PushNotificationToken { get; set; }
        string NewPushNotificationToken { get; set; }
        string UniqueDeviceId { get; set; }
        long SelectedReportId { get; set; }
        long UshdId { get; set; }
        long SessionId { get; set; }
        long MunicipalityId { get; set; }
        long PlaceId { get; set; }
        string MunicipalityName { get; set; }
        string BaseGatewayEndpoint { get; set; }
        string BaseGatewayEndpointEnvironment { get; set; }
        bool HasIntroShown { get; set; }
        string SearchMunicipality { get; set; }
        bool IsPersonalInfoAnimationShown { get; set; }
        bool IsLocationScreenGuideShown { get; set; }
        long MaxNumberOfFiles { get; set; }
    }

    public class SettingsService : BaseService, ISettingsService
    {
        private readonly IPreferencesManager _preferencesManager = DependencyService.Get<IPreferencesManager>();

        #region Public Properties

        public string AuthAccessToken
        {
            get => GetValueOrDefault(nameof(AuthAccessToken), string.Empty);
            set => AddOrUpdateValue(nameof(AuthAccessToken), value);
        }

        public string PushNotificationToken
        {
            get => GetValueOrDefault(nameof(PushNotificationToken), string.Empty);
            set => AddOrUpdateValue(nameof(PushNotificationToken), value);
        }

        public string NewPushNotificationToken
        {
            get => GetValueOrDefault(nameof(NewPushNotificationToken), string.Empty);
            set => AddOrUpdateValue(nameof(NewPushNotificationToken), value);
        }

        public string UniqueDeviceId
        {
            get => GetValueOrDefault(nameof(UniqueDeviceId), string.Empty);
            set => AddOrUpdateValue(nameof(UniqueDeviceId), value);
        }

        public long MunicipalityId
        {
            get => Convert.ToInt32(GetValueOrDefault(nameof(MunicipalityId), "0"));
            set => AddOrUpdateValue(nameof(MunicipalityId), value.ToString());
        }

        public string MunicipalityName
        {
            get => GetValueOrDefault(nameof(MunicipalityName), string.Empty);
            set => AddOrUpdateValue(nameof(MunicipalityName), value);
        }

        public long PlaceId
        {
            get => Convert.ToInt32(GetValueOrDefault(nameof(PlaceId), "0"));
            set => AddOrUpdateValue(nameof(PlaceId), value.ToString());
        }

        public long SelectedReportId
        {
            get => Convert.ToInt32(GetValueOrDefault(nameof(SelectedReportId), "0"));
            set => AddOrUpdateValue(nameof(SelectedReportId), value.ToString());
        }

        public long UshdId
        {
            get => Convert.ToInt32(GetValueOrDefault(nameof(UshdId), "0"));
            set => AddOrUpdateValue(nameof(UshdId), value.ToString());
        }

        public long SessionId
        {
            get => Convert.ToInt32(GetValueOrDefault(nameof(SessionId), "0"));
            set => AddOrUpdateValue(nameof(SessionId), value.ToString());
        }

        public bool HasIntroShown
        {
            get => GetValueOrDefault(nameof(HasIntroShown), false);
            set => AddOrUpdateValue(nameof(HasIntroShown), value);
        }

        public string SearchMunicipality
        {
            get => GetValueOrDefault(nameof(SearchMunicipality), string.Empty);
            set => AddOrUpdateValue(nameof(SearchMunicipality), value);
        }

        public bool IsPersonalInfoAnimationShown
        {
            get => GetValueOrDefault(nameof(IsPersonalInfoAnimationShown), false);
            set => AddOrUpdateValue(nameof(IsPersonalInfoAnimationShown), value);
        }

        public long MaxNumberOfFiles
        {
            get => Convert.ToInt32(GetValueOrDefault(nameof(MaxNumberOfFiles), "0"));
            set => AddOrUpdateValue(nameof(MaxNumberOfFiles), value.ToString());
        }

        public bool IsLocationScreenGuideShown
        {
            get => GetValueOrDefault(nameof(IsLocationScreenGuideShown), false);
            set => AddOrUpdateValue(nameof(IsLocationScreenGuideShown), value);
        }

        public string BaseGatewayEndpoint
        {
            get => GetValueOrDefault(nameof(BaseGatewayEndpoint), string.Empty);
            set => AddOrUpdateValue(nameof(BaseGatewayEndpoint), value);
        }

        public string BaseGatewayEndpointEnvironment
        {
            get => GetValueOrDefault(nameof(BaseGatewayEndpointEnvironment), string.Empty);
            set => AddOrUpdateValue(nameof(BaseGatewayEndpointEnvironment), value);
        }

        #endregion

        #region Private Methods

        public Task AddOrUpdateValue(string key, bool value) => _preferencesManager.PlatformSet(key, value);
        public Task AddOrUpdateValue(string key, string value) => _preferencesManager.PlatformSet(key, value);
        public bool GetValueOrDefault(string key, bool defaultValue) => _preferencesManager.PlatformGet(key, defaultValue);
        public string GetValueOrDefault(string key, string defaultValue) => _preferencesManager.PlatformGet(key, defaultValue);

        #endregion

        public SettingsService()
        {
            _preferencesManager = DependencyService.Get<IPreferencesManager>();
        }
    }
}

