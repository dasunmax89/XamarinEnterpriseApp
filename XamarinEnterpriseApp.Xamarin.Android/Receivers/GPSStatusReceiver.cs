using System;
using Android.App;
using Android.Content;

using Android.Gms.Common.Apis;
using Android.Locations;
using Android.OS;
using XamarinEnterpriseApp.Xamarin.Core;
using XamarinEnterpriseApp.Xamarin.Core.Helpers;
using Xamarin.Forms;

namespace XamarinEnterpriseApp.Xamarin.Droid.Receivers
{
    [BroadcastReceiver(Exported = false)]
    [IntentFilter(new[] { LocationManager.ProvidersChangedAction })]
    public class GPSStatusReceiver : BroadcastReceiver
    {
        public override void OnReceive(Context context, Intent intent)
        {
            try
            {
                LocationManager leManager = (LocationManager)context.GetSystemService(Context.LocationService);

                bool gpsEnabled = leManager.IsProviderEnabled(LocationManager.GpsProvider);
                bool networkEnabled = leManager.IsProviderEnabled(LocationManager.NetworkProvider);

                int state = gpsEnabled ? 2 : 1;

                MessagingCenter.Send<GlobalSetting, int>(GlobalSetting.Instance, Core.Constants.MessageKeys.GPSStatusChanged, state);

            }
            catch (Exception ex)
            {
                LogHelper.LogException("GPSStatusReceiver - Error occured OnReceive", ex);
            }
        }
    }
}
