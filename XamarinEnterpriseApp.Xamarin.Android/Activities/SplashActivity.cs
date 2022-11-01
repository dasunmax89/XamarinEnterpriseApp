using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using XamarinEnterpriseApp.Xamarin.Core;
using XamarinEnterpriseApp.Xamarin.Core.Models;
using XamarinEnterpriseApp.Xamarin.Droid.Helpers;

namespace XamarinEnterpriseApp.Xamarin.Droid.Activities
{
    [Activity(Icon = "@mipmap/ic_launcher",
       Theme = "@style/SplashTheme",
       MainLauncher = true,
       NoHistory = true,
       ScreenOrientation = ScreenOrientation.Portrait)]
    public class SplashActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            var pushData = PushHelper.GetPushNotificationData(Intent.Extras);

            if (pushData != null)
            {
                GlobalSetting.Instance.PushNotificationData = pushData;
            }

            if (!IsTaskRoot)
            {
                string action = this.Intent.Action;

                if (pushData == null && this.Intent.HasCategory(Intent.CategoryLauncher) && !string.IsNullOrEmpty(this.Intent.Action) && action == Intent.ActionMain)
                {
                    Finish();
                    return;
                }
            }

            CallMainActivity();
        }

        private void CallMainActivity()
        {
            var intent = new Intent(this, typeof(MainActivity));
            StartActivity(intent);
        }
    }
}
