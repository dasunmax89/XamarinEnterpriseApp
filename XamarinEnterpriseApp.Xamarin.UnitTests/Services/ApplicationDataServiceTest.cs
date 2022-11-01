using System.Linq;
using System.Threading.Tasks;
using XamarinEnterpriseApp.Xamarin.Core.Helpers;
using XamarinEnterpriseApp.Xamarin.Core.Ioc;
using XamarinEnterpriseApp.Xamarin.Core.Models;
using XamarinEnterpriseApp.Xamarin.Core.Services;
using XamarinEnterpriseApp.Xamarin.Core.Services.Mocks;
using Xunit;

namespace XamarinEnterpriseApp.Xamarin.UnitTests.Services
{
    public class ApplicationDataServiceTest : ServiceTestBase
    {
        public ApplicationDataServiceTest()
        {
            TestHelper.Init();
        }
    }
}
