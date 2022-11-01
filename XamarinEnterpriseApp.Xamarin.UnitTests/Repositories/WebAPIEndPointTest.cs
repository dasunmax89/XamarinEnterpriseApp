using System;
using System.Threading.Tasks;
using XamarinEnterpriseApp.Xamarin.Core.Constants;
using XamarinEnterpriseApp.Xamarin.Core.Ioc;
using XamarinEnterpriseApp.Xamarin.Core.Models;
using XamarinEnterpriseApp.Xamarin.Core.Repositories;
using XamarinEnterpriseApp.Xamarin.UnitTests.Services;
using Xunit;

namespace XamarinEnterpriseApp.Xamarin.UnitTests.Repositories
{
    public class WebAPIEndPointTest
    {
        public WebAPIEndPointTest()
        {
            TestHelper.Init();
        }

        //[Fact]
        public void SaveWebAPIEndpointTest()
        {
            var environmentSetting = new EnvironmentSetting()
            {
                EndPointName = "Test URL 2",
                EndPointURL = "http://testurl2.com",
                Type = TemplateConstants.ITEM_TYPE_RADIO_BUTTON,
                IsSelected = false,
                UserName = string.Empty,
                Password = string.Empty,
                RememberCredentials = false,
            };

            var XamarinEnterpriseAppDB = DependencyResolver.Resolve<ILocalDbContextService>();

            int result = XamarinEnterpriseAppDB.SaveEntity(environmentSetting);

            Assert.True(result >= 1);

        }

        //[Fact]
        public void SaveWebAPIEndpointAsyncTestAsync()
        {
            EnvironmentSetting environmentSetting = new EnvironmentSetting()
            {
                EndPointURL = "something",
                EndPointName = "Something 1",
                UserName = string.Empty,
                Password = string.Empty,
                RememberCredentials = false,
            };

            var XamarinEnterpriseAppDB = DependencyResolver.Resolve<ILocalDbContextService>();
            int result = XamarinEnterpriseAppDB.SaveEntity(environmentSetting);

            Assert.True(result >= 1);
        }

    }
}
