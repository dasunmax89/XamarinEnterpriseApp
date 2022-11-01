using System;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace XamarinEnterpriseApp.Xamarin.UITests
{
    public class AppInitializer
    {
        public static IApp StartApp(Platform platform)
        {
            if (platform == Platform.Android)
            {
                return ConfigureApp.Android.StartApp();
            }

            return ConfigureApp
                    .iOS
                    .Debug()
                    .PreferIdeSettings() // when in VS, use selected device
                    .EnableLocalScreenshots()
                    .StartApp();

        }
    }
}