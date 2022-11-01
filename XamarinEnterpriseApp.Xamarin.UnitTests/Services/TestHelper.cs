using System.Threading.Tasks;
using XamarinEnterpriseApp.Xamarin.Core;
using XamarinEnterpriseApp.Xamarin.Core.Constants;
using XamarinEnterpriseApp.Xamarin.Core.Ioc;
using XamarinEnterpriseApp.Xamarin.Core.Models;
using XamarinEnterpriseApp.Xamarin.Core.Services;
using XamarinEnterpriseApp.Xamarin.Core.Services.Mocks;
using Xamarin.Forms.Mocks;

namespace XamarinEnterpriseApp.Xamarin.UnitTests.Services
{
    public class TestHelper
    {
        public static void Init()
        { 
            DependencyResolver.RegisterComponents(true);
            MockForms.Init();
            SettingsMockService.SetMockValues();
        }

        public async Task<AuthenticationResponse> Login()
        {
            var settingsService = DependencyResolver.Resolve<ISettingsService>();
            var userService = DependencyResolver.Resolve<IUserService>();
            var connectionService = DependencyResolver.Resolve<IConnectionService>();

            var authRequest = new AuthenticationRequest()
            {
                Password = "Wachtwoord@1",
                UserName = "THOR",
                DeviceToken = "",
                AppVersion = "1.0.0",
                DevicePlatform = "ios",
                Language = "ENGLISH",
                Application = "XamarinEnterpriseApp.App",
                IpAddress = connectionService.DeviceIP
            };

            var response = await userService.Authenticate(authRequest);

            return response;
        }

        public async Task<AuthenticationResponse> GetToken()
        {
            AuthenticationResponse loginResponse;

            loginResponse = await Login();

            return loginResponse;
        }
    }
}
