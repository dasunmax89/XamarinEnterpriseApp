using System;
using Foundation;
using XamarinEnterpriseApp.Xamarin.Core.Models;
using UIKit;

namespace XamarinEnterpriseApp.Xamarin.iOS.Helpers
{
    public static class PushHelper
    {
        public static PushNotificationData GetPushNotificationData(NSDictionary userInfo)
        {
            PushNotificationData data = null;

            long reportId = 0;
            long custId = 0;
            string title = string.Empty;
            string body = string.Empty;

            if (userInfo.ContainsKey(new NSString("Cust_id")) && userInfo.ContainsKey(new NSString("ithd_id")))
            {
                string custIdString = (userInfo[new NSString("Cust_id")] as NSString).ToString();
                string idString = (userInfo[new NSString("ithd_id")] as NSString).ToString();
                title = (userInfo[new NSString("title")] as NSString).ToString();
                body = (userInfo[new NSString("body")] as NSString).ToString();

                long.TryParse(custIdString, out custId);

                long.TryParse(idString, out reportId);

                data = new PushNotificationData()
                {
                    ReportId = reportId,
                    CustId = custId,
                    Body = body,
                    Title = title,
                };
            }

            return data;
        }

        public static void SendNotification(string alertBody, string title, int badgeCount, NSDictionary userInfo)
        {
            // create the notification
            var notification = new UILocalNotification();

            // set the fire date (the date time in which it will fire)
            notification.FireDate = NSDate.FromTimeIntervalSinceNow(2);

            // configure the alert
            notification.AlertAction = title;
            notification.AlertBody = alertBody;

            // modify the badge
            if (badgeCount > 0)
            {
                notification.ApplicationIconBadgeNumber = badgeCount;
            }

            notification.UserInfo = userInfo;

            // set the sound to be the default sound
            notification.SoundName = UILocalNotification.DefaultSoundName;

            // schedule it
            UIApplication.SharedApplication.ScheduleLocalNotification(notification);
        }
    }
}
