using System.Diagnostics;
using System.Threading.Tasks;
using XamarinEnterpriseApp.Xamarin.Core.Ioc;
using XamarinEnterpriseApp.Xamarin.Core.Models;
using XamarinEnterpriseApp.Xamarin.Core.Services;
using XamarinEnterpriseApp.Xamarin.Core.Services.Mocks;
using Xunit;

namespace XamarinEnterpriseApp.Xamarin.UnitTests.Services
{
    public class UserServiceTest : ServiceTestBase
    {
        public UserServiceTest()
        {
            TestHelper.Init();
        }

        [Fact]
        public async Task AuthenticateTest()
        {
            var testHelper = new TestHelper();

            var response = await testHelper.Login();

            Assert.True(response != null &&
                        response.IsSuccessful);
        }

        [Fact]
        public async Task LogoutTest()
        {
            var settingsService = DependencyResolver.Resolve<ISettingsService>();
            var userService = DependencyResolver.Resolve<IUserService>();

            var loginResponse = await GetToken();
            await SettingsMockService.SetMockToken(Task.FromResult<string>(loginResponse.AccessToken));

            LogoutRequest logoutRequest = new LogoutRequest()
            {
                SessionId = loginResponse.SessionId,
            };

            LogoutResponse logoutResponse = await userService.Logout(logoutRequest);

            Assert.True(logoutResponse != null &&
                        logoutResponse.IsSuccessful);

        }

    }
}
