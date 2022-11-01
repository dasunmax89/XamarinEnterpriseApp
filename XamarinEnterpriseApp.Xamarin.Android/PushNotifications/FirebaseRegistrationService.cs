using System;
using System.Threading.Tasks;
using Android.App;
using Android.Util;
using Firebase.Iid;
using Firebase.Messaging;
using XamarinEnterpriseApp.Xamarin.Core.Ioc;
using XamarinEnterpriseApp.Xamarin.Core.Services;
using Xamarin.Forms;

namespace XamarinEnterpriseApp.Xamarin.Droid.PushNotifications
{
    [Service]
    [IntentFilter(new[] { "com.google.firebase.INSTANCE_ID_EVENT" })]
    public class FirebaseRegistrationService : FirebaseMessagingService
    {
        const string TAG = "FirebaseRegistrationService";

        public override void OnNewToken(string p0)
        {
            base.OnNewToken(p0);
            var refreshedToken = p0;
            SendRegistrationTokenToNotificationHub(refreshedToken);
        }

        public override void OnMessageReceived(RemoteMessage p0)
        {
            base.OnMessageReceived(p0);
        }

        private void SendRegistrationTokenToNotificationHub(string token)
        {
            var settingsService = DependencyResolver.Resolve<ISettingsService>();
            settingsService.PushNotificationToken = token; 
        }
    }
}
