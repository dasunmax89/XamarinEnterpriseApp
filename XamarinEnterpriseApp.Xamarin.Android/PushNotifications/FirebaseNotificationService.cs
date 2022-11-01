using System;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Media;
using Android.OS;
using Android.Support.V4.App;
using Android.Util;
using Firebase.Messaging;
using XamarinEnterpriseApp.Xamarin.Core;
using XamarinEnterpriseApp.Xamarin.Core.Constants;
using XamarinEnterpriseApp.Xamarin.Core.Helpers;
using XamarinEnterpriseApp.Xamarin.Core.Ioc;
using XamarinEnterpriseApp.Xamarin.Core.Models;
using XamarinEnterpriseApp.Xamarin.Core.Services;
using XamarinEnterpriseApp.Xamarin.Droid.Activities;
using XamarinEnterpriseApp.Xamarin.Droid.Dependency;
using XamarinEnterpriseApp.Xamarin.Droid.Helpers;
using Xamarin.Forms;

namespace XamarinEnterpriseApp.Xamarin.Droid.PushNotifications
{
    [Service(Exported = false)]
    [IntentFilter(new[] { "com.google.firebase.MESSAGING_EVENT" })]
    public class FirebaseNotificationService : FirebaseMessagingService
    {
        public override async void OnNewToken(string p0)
        {
            base.OnNewToken(p0);
            var refreshedToken = p0;
            await SendRegistrationTokenToNotificationHub(refreshedToken);
        }

        public override void OnMessageReceived(RemoteMessage message)
        {
            try
            {
                base.OnMessageReceived(message);

                long reportId = 0;
                long custId = 0;


                if (message.Data.ContainsKey("ithd_id"))
                {
                    var ithd_id = message.Data["ithd_id"];

                    long.TryParse(ithd_id, out reportId);
                }

                if (message.Data.ContainsKey("Cust_id"))
                {
                    var cust_id = message.Data["Cust_id"];

                    long.TryParse(cust_id, out custId);
                }

                var messageBody = message.Data["body"];
                var messageTitle = message.Data["title"];

                PushHelper.SendNotification(messageBody, messageTitle, reportId, custId, this);
            }
            catch (System.Exception ex)
            {
                LogHelper.LogException("Error occured when pushing the notification", ex);
            }
        }

        public async Task SendRegistrationTokenToNotificationHub(string fcmToken)
        {
            // Note: This callback is fired at each app startup and whenever a new token is generated.
            // Monitor token generation: To be notified whenever the token is updated.

            var preferencesManager = new PreferencesManager();

            await preferencesManager.PlatformSet(nameof(ISettingsService.PushNotificationToken), fcmToken);
            await preferencesManager.PlatformSet(nameof(ISettingsService.NewPushNotificationToken), fcmToken);

            if (global::Xamarin.Forms.Forms.IsInitialized)
            {
                var connectionService = DependencyResolver.Resolve<IConnectionService>();

                if (connectionService.IsConnected)
                {
                    var settingsService = DependencyResolver.Resolve<ISettingsService>();
                    var applicationDataService = DependencyResolver.Resolve<IApplicationDataService>();

                    UpdateDeviceTokenRequest request = new UpdateDeviceTokenRequest()
                    {
                        EndPoint = $"Receiver/",
                        Language = "ENGLISH",
                        Application = "XamarinEnterpriseApp.App",
                        IpAddress = connectionService.DeviceIP,
                        DeviceId = settingsService.UniqueDeviceId,
                        DeviceToken = fcmToken,
                    };

                    var response = await applicationDataService.PostRequest<UpdateDeviceTokenResponse, UpdateDeviceTokenRequest>(request);

                    if (response != null && response.IsSuccessful && response.IsSaved)
                    {
                        settingsService.NewPushNotificationToken = string.Empty;
                    }
                    else
                    {
                        LogHelper.TrackEvent("SendRegTokenToNotificationHub- Error", "error");
                    }
                }
                else
                {
                    LogHelper.TrackEvent("SendRegTokenToNotificationHub- Error", "no internet");
                }
            }

            System.Diagnostics.Debug.WriteLine(fcmToken);
        }
    }
}
