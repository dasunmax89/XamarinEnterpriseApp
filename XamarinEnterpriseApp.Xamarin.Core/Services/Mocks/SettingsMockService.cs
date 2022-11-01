using System;
using System.Threading.Tasks;
using XamarinEnterpriseApp.Xamarin.Core.Constants;

namespace XamarinEnterpriseApp.Xamarin.Core.Services.Mocks
{
    public class SettingsMockService : ISettingsService
    {
        public SettingsMockService()
        {
        }

        private static string _authAccessTokenInternal { get; set; }
        public string AuthAccessToken
        {
            get => _authAccessTokenInternal;
            set => _authAccessTokenInternal = value;
        }

        public static async Task SetMockToken(Task<string> value)
        {
            _authAccessTokenInternal = await value;
        }

        public static void SetMockValues()
        {

        }

        public string PushNotificationToken { get; set; }
        public string NewPushNotificationToken { get; set; }
        public long MunicipalityId { get; set; }
        public string MunicipalityName { get; set; }
        public long PlaceId { get; set; }
        public long SelectedReportId { get; set; }
        public long UshdId { get; set; }
        public long SessionId { get; set; }
        public bool HasIntroShown { get; set; }
        public string SearchMunicipality { get; set; }
        public bool IsPersonalInfoAnimationShown { get; set; }
        public long MaxNumberOfFiles { get; set; }
        public bool IsLocationScreenGuideShown { get; set; }
        public string UniqueDeviceId { get; set; }
        public string BaseGatewayEndpoint { get; set; }
        public string BaseGatewayEndpointEnvironment { get; set; }
    }
}
