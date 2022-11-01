using XamarinEnterpriseApp.Xamarin.Core.Ioc;
using XamarinEnterpriseApp.Xamarin.Core.ViewModels;
using XamarinEnterpriseApp.Xamarin.UnitTests.Services;
using Xunit;

namespace XamarinEnterpriseApp.Xamarin.UnitTests.ViewModels
{
    public class LoginViewModelTests
    {
        public LoginViewModelTests()
        {
            TestHelper.Init();
        }

        // [Fact]
        public void LoginTest()
        {
            var vm = DependencyResolver.Resolve<LoginViewModel>();
            vm.UserName = "THOR";
            vm.Password = "WACHTWOORD@1";
            vm.OnSubmit();
        }
    }
}
