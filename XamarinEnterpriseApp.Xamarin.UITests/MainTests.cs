using System;
using System.IO;
using System.Linq;
using XamarinEnterpriseApp.Xamarin.UITests;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace XamarinEnterpriseApp.Xamarin.UITests
{
    [TestFixture(Platform.Android)]
    [TestFixture(Platform.iOS)]
    public class MainTests : TestBase
    {
        Platform platform;

        public MainTests(Platform platform)
        {
            this.platform = platform;
        }

        [SetUp]
        public void BeforeEachTest()
        {
            app = AppInitializer.StartApp(platform);
        }

        [Test]
        public void HomeTest()
        {
            LoginToApp();
        }
    }
}
