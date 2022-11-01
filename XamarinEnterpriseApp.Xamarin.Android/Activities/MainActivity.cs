
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Acr.UserDialogs;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Content.Res;
using Android.Gms.Common;
using Android.Gms.Maps;
using Android.Locations;
using Android.OS;
using XamarinEnterpriseApp.Xamarin.Core;
using XamarinEnterpriseApp.Xamarin.Core.Constants;
using XamarinEnterpriseApp.Xamarin.Core.Helpers;
using XamarinEnterpriseApp.Xamarin.Core.Ioc;
using XamarinEnterpriseApp.Xamarin.Core.Models;
using XamarinEnterpriseApp.Xamarin.Core.Services;
using XamarinEnterpriseApp.Xamarin.Core.ViewModels;
using XamarinEnterpriseApp.Xamarin.Core.ViewModels.Base;
using XamarinEnterpriseApp.Xamarin.Droid.Dependency;
using XamarinEnterpriseApp.Xamarin.Droid.Helpers;
using XamarinEnterpriseApp.Xamarin.Droid.Receivers;
using Plugin.Permissions;
using XE = Xamarin.Essentials;

namespace XamarinEnterpriseApp.Xamarin.Droid.Activities
{
    [Activity(Label = "XamarinEnterpriseApp.Xamarin",
        Icon = "@mipmap/ic_launcher",
        Theme = "@style/MainTheme",
        MainLauncher = false,
        ConfigurationChanges = ConfigChanges.Orientation |
        ConfigChanges.ScreenSize | ConfigChanges.KeyboardHidden,
        ScreenOrientation = ScreenOrientation.User)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        private GPSStatusReceiver gpsStatusReceiver;

        protected async override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            var context = Android.App.Application.Context;

            gpsStatusReceiver = new GPSStatusReceiver();

            var pushData = PushHelper.GetPushNotificationData(Intent.Extras);

            if (pushData != null)
            {
                GlobalSetting.Instance.PushNotificationData = pushData;
            }

            bool isGooglePlayAvailable = await PushHelper.CheckPlayServicesAvailability(context);

            if (isGooglePlayAvailable)
            {
                CreateNotificationChannel();
            }

            XE.Platform.Init(this, savedInstanceState);

            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            global::Xamarin.FormsMaps.Init(this, savedInstanceState);

            UserDialogs.Init(this);

            Plugin.CurrentActivity.CrossCurrentActivity.Current.Init(this, savedInstanceState);

            Toolkit.Init(this, savedInstanceState);

            App XamarinEnterpriseApp = new App();

            LoadApplication(XamarinEnterpriseApp);
        }

        public async override void OnConfigurationChanged(Configuration newConfig)
        {
            base.OnConfigurationChanged(newConfig);

            try
            {
                var navigationService = DependencyResolver.Resolve<INavigationService>();

                ViewModelBase currentViewModel = navigationService.GetCurrentViewModel();

                bool isValid = false;//TODOcurrentViewModel is MapViewModel || currentViewModel is SelectReportLocationViewModel;

                if (isValid)
                {
                    var mapViewModel = currentViewModel as ViewModelBase;

                    await mapViewModel.OnAppearing();
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogException("Map-OnConfigurationChanged", ex);
            }
        }

        void CreateNotificationChannel()
        {
            if (Build.VERSION.SdkInt < BuildVersionCodes.O)
            {
                // Notification channels are new in API 26 (and not a part of the
                // support library). There is no need to create a notification
                // channel on older versions of Android.
                return;
            }

            var channel = new NotificationChannel(PushNotificationConstants.CHANNEL_ID,
                                                 PushNotificationConstants.CHANNEL_NAME,
                                                  NotificationImportance.Default)
            {

                Description = "Firebase Cloud Messages appear in this channel"
            };

            var notificationManager = (NotificationManager)GetSystemService(Android.Content.Context.NotificationService);
            notificationManager.CreateNotificationChannel(channel);

        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
        {
            XE.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        protected override void OnPause()
        {
            base.OnPause();

            if (gpsStatusReceiver != null)
            {
                try
                {
                    UnregisterReceiver(gpsStatusReceiver);
                }
                catch (Exception ex)
                {

                }
            }
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
        }

        protected override void OnResume()
        {
            base.OnResume();

            if (gpsStatusReceiver != null)
            {
                try
                {
                    RegisterReceiver(gpsStatusReceiver, new IntentFilter(LocationManager.ProvidersChangedAction));
                }
                catch (Exception ex)
                {

                }
            }
        }

        private void LogException(Exception ex)
        {
            LogHelper.LogException("Exception occured", ex);
        }
    }
}