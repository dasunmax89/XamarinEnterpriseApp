using System;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Locations;
using XamarinEnterpriseApp.Xamarin.Core.Dependency;
using XamarinEnterpriseApp.Xamarin.Core.Helpers;
using XamarinEnterpriseApp.Xamarin.Core.Ioc;
using XamarinEnterpriseApp.Xamarin.Core.Models;
using XamarinEnterpriseApp.Xamarin.Core.Services;
using Xamarin.Essentials;
using Xamarin.Forms;

[assembly: Dependency(typeof(XamarinEnterpriseApp.Xamarin.Droid.Dependency.LocationTracker))]

namespace XamarinEnterpriseApp.Xamarin.Droid.Dependency
{
    public class LocationTracker : Java.Lang.Object, ILocationTracker
    {
        LocationManager locationManager;

        public event EventHandler<GeographicLocation> LocationChanged;

        public LocationTracker()
        {
            Activity activity = Toolkit.Activity;

            if (activity == null)
                throw new InvalidOperationException(
                    "Must call Toolkit.Init before using LocationProvider");

            locationManager =
                activity.GetSystemService(Context.LocationService) as LocationManager;

        }

        public bool CheckGPSEnabled()
        {
            bool isGPSEnabled = false;

            try
            {
                isGPSEnabled = locationManager.IsProviderEnabled(LocationManager.GpsProvider);
            }
            catch (Exception ex)
            {
                LogHelper.LogException("CheckGPSEnabled-Exception occured", ex);
            }

            return isGPSEnabled;
        }

        public async void EnableGPSSetting()
        {
            await ShowToast("Zet de GPS aan om een nauwkeurige locatie te krijgen.");

            OpenGPSSettings();

            LocationChanged?.Invoke(this, null);
        }

        private void OpenGPSSettings()
        {
            try
            {
                Intent gpsSettingIntent = new Intent(Android.Provider.Settings.ActionLocat‌​ionSourceSettings);
                gpsSettingIntent.AddFlags(ActivityFlags.NewTask);
                Android.App.Application.Context.StartActivity(gpsSettingIntent);
            }
            catch (Exception ex)
            {
                LogHelper.LogException("OpenGPSSettings-Exception occured", ex);
            }
        }

        protected async Task ShowToast(string message)
        {
            var dialogService = DependencyResolver.Resolve<IDialogService>();
            await dialogService.ShowToast(message);
        }

        public async Task<GeographicLocation> GetLocationAsync()
        {
            GeographicLocation geographicLocation = null;

            try
            {
                var request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));

                var location = await Geolocation.GetLocationAsync(request);

                if (location != null)
                {
                    geographicLocation = new GeographicLocation(location.Latitude, location.Longitude);

                    geographicLocation.IsUserLocation = true;
                }
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                await ShowToast(fnsEx.Message);
            }
            catch (FeatureNotEnabledException fneEx)
            {
                await ShowToast(fneEx.Message);
            }
            catch (PermissionException pEx)
            {
                await ShowToast(pEx.Message);
            }
            catch (Exception ex)
            {
                await ShowToast(ex.Message);
            }

            return geographicLocation;
        }
    }
}