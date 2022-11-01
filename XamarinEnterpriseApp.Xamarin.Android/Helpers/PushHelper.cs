using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Gms.Common;
using Android.Media;
using Android.OS;
using Android.Support.V4.App;
using XamarinEnterpriseApp.Xamarin.Core;
using XamarinEnterpriseApp.Xamarin.Core.Constants;
using XamarinEnterpriseApp.Xamarin.Core.Ioc;
using XamarinEnterpriseApp.Xamarin.Core.Models;
using XamarinEnterpriseApp.Xamarin.Core.Services;
using XamarinEnterpriseApp.Xamarin.Droid.Activities;

namespace XamarinEnterpriseApp.Xamarin.Droid.Helpers
{
    public static class PushHelper
    {
        public static PushNotificationData GetPushNotificationData(Bundle data)
        {
            PushNotificationData pushData = null;

            if (data != null)
            {
                long reportId = 0;
                long custId = 0;
                string title = string.Empty;
                string body = string.Empty;

                var d = new Dictionary<string, string>();

                var keyset = data.KeySet();

                foreach (var key in keyset)
                {
                    d.Add(key, data.Get(key)?.ToString());
                }

                if (data.ContainsKey("title"))
                {
                    title = data.GetString("title");
                }

                if (data.ContainsKey("body"))
                {
                    body = data.GetString("body");
                }

                if (data.ContainsKey("ithd_id"))
                {
                    var stringVal = data.GetString("ithd_id");

                    if (!string.IsNullOrEmpty(stringVal))
                    {
                        long.TryParse(stringVal, out reportId);
                    }
                    else
                    {
                        reportId = data.GetLong("ithd_id");
                    }
                }

                if (data.ContainsKey("Cust_id"))
                {
                    var stringVal = data.GetString("Cust_id");

                    if (!string.IsNullOrEmpty(stringVal))
                    {
                        long.TryParse(stringVal, out custId);
                    }
                    else
                    {
                        custId = data.GetLong("Cust_id");
                    }
                }

                pushData = new PushNotificationData()
                {
                    ReportId = reportId,
                    CustId = custId,
                    Body = body,
                    Title = title,
                };
            }

            return pushData;
        }

        public static void SendNotification(string messageBody, string messageTitle, long reportId, long custId, Context context)
        {
            var intent = new Intent(context, typeof(MainActivity));
            intent.AddFlags(ActivityFlags.ClearTop);
            intent.PutExtra("ithd_id", reportId);
            intent.PutExtra("Cust_id", custId);

            Bundle notificationCenterExtras = new Bundle();
            notificationCenterExtras.PutLong("ithd_id", reportId);
            notificationCenterExtras.PutLong("Cust_id", custId);

            Random random = new Random();
            int pushCount = random.Next(9999 - 1000) + 1000;

            intent.AddFlags(ActivityFlags.ClearTop);
            var pendingIntent = PendingIntent.GetActivity(context, pushCount, intent, PendingIntentFlags.Immutable);

            var notificationBuilder = new NotificationCompat.Builder(context, PushNotificationConstants.CHANNEL_ID)
                .SetSmallIcon(Resource.Drawable.icon)
                .SetContentTitle(messageTitle)
                .SetContentText(messageBody)
                .SetContentIntent(pendingIntent)
                .SetSound(RingtoneManager.GetDefaultUri(RingtoneType.Notification))
                .SetAutoCancel(false)
                .SetExtras(notificationCenterExtras);

            var notificationManager = NotificationManager.FromContext(context);

            // Since android Oreo notification channel is needed.

            if (Build.VERSION.SdkInt >= BuildVersionCodes.O)
            {
                var channel = new NotificationChannel(PushNotificationConstants.CHANNEL_ID,
                                                  PushNotificationConstants.CHANNEL_NAME,
                                                   NotificationImportance.Default)
                {

                    Description = "Firebase Cloud Messages appear in this channel"
                };

                notificationManager.CreateNotificationChannel(channel);
            }


            notificationManager.Notify(pushCount, notificationBuilder.Build());
        }

        public static async Task<bool> CheckPlayServicesAvailability(Context context)
        {
            bool isAvailable = false;

            int resultCode = GoogleApiAvailability.Instance.IsGooglePlayServicesAvailable(context);

            if (resultCode != ConnectionResult.Success)
            {
                string message = string.Empty;

                if (GoogleApiAvailability.Instance.IsUserResolvableError(resultCode))
                {
                    message = GoogleApiAvailability.Instance.GetErrorString(resultCode);
                }
                else
                {
                    message = "This device is not supported";
                }

                if (string.IsNullOrEmpty(message))
                {
                    await ShowToast(message);
                }

                isAvailable = false;
            }
            else
            {
                isAvailable = true;
            }

            return isAvailable;
        }

        public static async Task ShowToast(string message)
        {
            var dialogService = DependencyResolver.Resolve<IDialogService>();
            await dialogService.ShowToast(message);
        }
    }
}
