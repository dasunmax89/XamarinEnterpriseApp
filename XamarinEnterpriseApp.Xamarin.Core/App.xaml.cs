using System;
using System.Threading.Tasks;
using XamarinEnterpriseApp.Xamarin.Core.Helpers;
using XamarinEnterpriseApp.Xamarin.Core.Ioc;
using XamarinEnterpriseApp.Xamarin.Core.Localization;
using XamarinEnterpriseApp.Xamarin.Core.Services;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using PP = Plugin.Permissions;
using XamarinEnterpriseApp.Xamarin.Core.Constants;

[assembly: ExportFont("BasierCircle-Bold.otf", Alias = "BasierCircle-Bold")]
[assembly: ExportFont("BasierCircle-BoldItalic.otf", Alias = "BasierCircle-BoldItalic")]
[assembly: ExportFont("BasierCircle-Medium.otf", Alias = "BasierCircle-Medium")]
[assembly: ExportFont("BasierCircle-MediumItalic.otf", Alias = "BasierCircle-MediumItalic")]
[assembly: ExportFont("BasierCircle-Regular.otf", Alias = "BasierCircle-Regular")]
[assembly: ExportFont("BasierCircle-RegularItalic.otf", Alias = "BasierCircle-RegularItalic")]
[assembly: ExportFont("BasierCircle-SemiBold.otf", Alias = "BasierCircle-SemiBold")]
[assembly: ExportFont("BasierCircle-SemiBoldItalic.otf", Alias = "BasierCircle-SemiBoldItalic")]
[assembly: ExportFont("ComicSans.ttf", Alias = "ComicSans")]
[assembly: ExportFont("SegoeUI.ttf", Alias = "SegoeUI")]
[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace XamarinEnterpriseApp.Xamarin.Core
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            InitApp();
        }

        private void InitApp()
        {
            DependencyResolver.RegisterComponents();

            var settingsService = DependencyResolver.Resolve<ISettingsService>();

            settingsService.BaseGatewayEndpoint = ApiConstants.BaseApiUrl;

#if DEBUG
            settingsService.BaseGatewayEndpoint = ApiConstants.BaseApiDemoUrl;
#endif

        }

        protected override async void OnStart()
        {
            await InitNavigation();

            SetupAppCenter();
        }

        private void SetupAppCenter()
        {
            try
            {
                AppCenter.Start("ios=d333175a-555f-4042-864c-77142b7eb82b;" +
                                "android=debcae46-6099-4811-85a5-b9dd57b417ad;",
                                typeof(Analytics), typeof(Crashes));
            }
            catch (Exception ex)
            {
                LogHelper.LogException("Exception occured while starting the app center", ex);
            }
        }

        private Task InitNavigation()
        {
            var navigationService = DependencyResolver.Resolve<INavigationService>();
            return navigationService.InitializeAsync();
        }

        private Task ResumeNavigation()
        {
            var navigationService = DependencyResolver.Resolve<INavigationService>();
            return navigationService.ResumeAsync();
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected async override void OnResume()
        {
            try
            {
                await ResumeNavigation();
            }
            catch (Exception ex)
            {
                LogHelper.LogException("Exception occured while resuming the app", ex);
            }
        }
    }
}
