using System;
using CoreLocation;
using XamarinEnterpriseApp.Xamarin.Core.Dependency;
using XamarinEnterpriseApp.Xamarin.Core.Ioc;
using XamarinEnterpriseApp.Xamarin.Core.Models;
using XamarinEnterpriseApp.Xamarin.Core.Services;
using UIKit;
using Xamarin.Forms;
using XamarinEnterpriseApp.Xamarin.Core.Localization;
using XamarinEnterpriseApp.Xamarin.Core.Helpers;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Foundation;

[assembly: Dependency(typeof(XamarinEnterpriseApp.Xamarin.iOS.Dependency.LocationTracker))]
namespace XamarinEnterpriseApp.Xamarin.iOS.Dependency
{
    public class LocationTracker : ILocationTracker
    {
        public event EventHandler<GeographicLocation> LocationChanged;

        public LocationTracker()
        {

        }

        public bool CheckGPSEnabled()
        {
            bool isGPSEnabled = false;

            try
            {
                isGPSEnabled = CLLocationManager.LocationServicesEnabled;
            }
            catch (Exception ex)
            {
                LogHelper.LogException("CheckGPSEnabled-Exception occured", ex);
            }

            return isGPSEnabled;
        }

        public void EnableGPSSetting()
        {
            UIApplication.SharedApplication.OpenUrl(new NSUrl("app-settings:"));
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

        protected async Task ShowToast(string message)
        {
            var dialogService = DependencyResolver.Resolve<IDialogService>();
            await dialogService.ShowToast(message);
        }
    }
}
