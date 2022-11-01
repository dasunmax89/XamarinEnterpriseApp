using System.Threading.Tasks;
using XamarinEnterpriseApp.Xamarin.Core.Models;

namespace XamarinEnterpriseApp.Xamarin.UnitTests.Services
{
    public class ServiceTestBase
    {
        public ServiceTestBase()
        {
        }

        public async Task<AuthenticationResponse> GetToken()
        {
            var testHelper = new TestHelper();

            return await testHelper.GetToken();
        }
    }
}
