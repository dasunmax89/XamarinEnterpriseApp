using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Support.V4.App;
using XamarinEnterpriseApp.Xamarin.Core.Dependency;
using XamarinEnterpriseApp.Xamarin.Core.Helpers;
using XamarinEnterpriseApp.Xamarin.Core.Ioc;
using XamarinEnterpriseApp.Xamarin.Core.Models;
using XamarinEnterpriseApp.Xamarin.Core.Services;
using XamarinEnterpriseApp.Xamarin.Droid.Dependency;
using XamarinEnterpriseApp.Xamarin.Droid.Helpers;
using XF = Xamarin.Forms;
using XamarinEnterpriseApp.Xamarin.Core.Extensions;
using System.IO;
using XE = Xamarin.Essentials;
using PP = Plugin.Permissions;
using Android.OS;
using static Android.Provider.Settings;
using Android.Util;
using Android.Graphics;
using XamarinEnterpriseApp.Xamarin.Core;
using Android.Views.Accessibility;
using Xamarin.Essentials;
using Xamarin.Forms.Platform.Android;

[assembly: XF.Dependency(typeof(PlatformManager))]
namespace XamarinEnterpriseApp.Xamarin.Droid.Dependency
{
    public class PlatformManager : IPlatformManager
    {
        public PlatformManager()
        {

        }

        public string GetIPAddress()
        {
            string ipAddress = string.Empty;

            try
            {
                IPAddress[] adresses = Dns.GetHostAddresses(Dns.GetHostName());

                if (adresses != null && adresses[0] != null)
                {
                    ipAddress = adresses[0].ToString();
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogException("GetIPAddress - Error", ex);
            }

            if (string.IsNullOrEmpty(ipAddress))
            {
                ipAddress = "127.0.0.1";
            }

            return ipAddress;

        }

        public async Task<bool> CheckPushEnabled(bool invokeSettings)
        {
            bool enabled = false;

            try
            {
                var nm = NotificationManagerCompat.From(Android.App.Application.Context);

                enabled = nm.AreNotificationsEnabled();

                if (!enabled && invokeSettings)
                {
                    OpenPushSettings();
                }
            }
            catch (Exception ex)
            {
                await ShowToast(ex.Message);

                LogHelper.LogException("CheckPushEnabled-Exception occured", ex);
            }

            return enabled;
        }

        public void OpenPushSettings()
        {
            var context = Android.App.Application.Context;
            var appInfo = context.PackageManager.GetApplicationInfo(context.PackageName, 0);

            string packageName = context.PackageName;

            var uri = Android.Net.Uri.Parse("package:" + packageName);

            Intent settingIntent = new Intent(Android.Provider.Settings.ActionAppNotificationSettings);

            //for Android 5-7
            settingIntent.PutExtra("app_package", packageName);

            if (appInfo != null)
            {
                settingIntent.PutExtra("app_uid", appInfo.Uid);
            }

            // for Android 8 and above
            settingIntent.PutExtra("android.provider.extra.APP_PACKAGE", packageName);

            settingIntent.AddFlags(ActivityFlags.NewTask);

            context.StartActivity(settingIntent);
        }

        protected async Task ShowToast(string message)
        {
            var dialogService = DependencyResolver.Resolve<IDialogService>();
            await dialogService.ShowToast(message);
        }

        public void IncrementBadgeCount(bool isDecrement = false)
        {

        }

        public async void MarkAsRead(long reportId, long custId)
        {
            try
            {
                if (GlobalSetting.Instance.PushNotificationData != null)
                {
                    GlobalSetting.Instance.PushNotificationData.IsProcessed = true;
                }

                var context = Android.App.Application.Context;

                var notificationManager = NotificationManager.FromContext(context);

                var pendingItems = notificationManager.GetActiveNotifications();

                if (pendingItems == null)
                {
                    return;
                }

                foreach (var pendingItem in pendingItems)
                {
                    bool isRemoving = false;

                    var intent = pendingItem.Notification.ContentIntent;

                    var userInfo = pendingItem.Notification.Extras;

                    if (userInfo != null)
                    {
                        var pushNotificationData = PushHelper.GetPushNotificationData(userInfo);

                        if (pushNotificationData != null)
                        {
                            if (custId > 0 && reportId > 0)
                            {
                                if (pushNotificationData.ReportId == reportId && pushNotificationData.CustId == custId)
                                {
                                    isRemoving = true;
                                }
                            }
                        }
                    }

                    if (isRemoving)
                    {
                        notificationManager.Cancel(pendingItem.Id);

                        IncrementBadgeCount(true);
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogException("MarkAsRead-Exception occured", ex);

                await ShowToast(ex.Message);
            }
        }

        public bool CheckPackageInstalled(string packageName)
        {
            bool installed = false;

            try
            {
                var context = Android.App.Application.Context;
                var packageInfo = context.PackageManager.GetPackageInfo(packageName, PackageInfoFlags.Activities);

                installed = true;
            }
            catch (Exception ex)
            {
                LogHelper.LogException("CheckPackageInstalled-Exception occured", ex);
            }

            return installed;
        }


        public List<ListItemModel> GetNavigationActionsheetItems(GeographicLocation position)
        {
            List<ListItemModel> listItems = new List<ListItemModel>();

            string[] packageNames = new string[] {
                "com.google.android.apps.maps",
                "com.google.android.apps.mapslite",
                "com.waze"};

            var renderService = DependencyResolver.Resolve<IUIRenderService>();

            foreach (var packageName in packageNames)
            {
                bool isInstalled = CheckPackageInstalled(packageName);
                if (isInstalled)
                {
                    string appName = string.Empty;

                    switch (packageName)
                    {
                        case "com.google.android.apps.maps":
                            appName = "Google Maps";
                            break;
                        case "com.google.android.apps.mapslite":
                            appName = "Google Maps Lite";
                            break;
                        case "com.ubercab":
                            appName = "Uber";
                            break;
                        case "com.ubercab.uberlite":
                            appName = "Uber Lite";
                            break;
                        case "com.waze":
                            appName = "Waze";
                            break;
                    }

                    listItems.Add(new ListItemModel()
                    {
                        ViewModel = null,
                        Header = appName,
                        Identifier = packageName,
                        IconSource = "Map_blue.png",
                        Tag = position,
                    });
                }
            }

            return listItems;
        }

        public void OpenGoogleMaps(GeographicLocation position)
        {
            var context = Android.App.Application.Context;

            var uri = Android.Net.Uri.Parse($"https://www.google.com/maps/dir/?api=1&dir_action=navigate&destination={position.Latitude.FormatInvariant()},{position.Longitude.FormatInvariant()}");

            Intent intent = new Intent(Intent.ActionView, uri);

            intent.AddFlags(ActivityFlags.NewTask);

            context.StartActivity(intent);
        }

        public void OpenUber(GeographicLocation position)
        {
            var context = Android.App.Application.Context;

            var uri = Android.Net.Uri.Parse($"uber://?ll={position.Latitude.FormatInvariant()},{position.Longitude.FormatInvariant()}&navigate=yes:");

            Intent intent = new Intent(Intent.ActionView, uri);

            intent.AddFlags(ActivityFlags.NewTask);

            context.StartActivity(intent);
        }

        public void OpenWaze(GeographicLocation position)
        {
            var context = Android.App.Application.Context;

            var uri = Android.Net.Uri.Parse($"waze://?ll={position.Latitude.FormatInvariant()},{position.Longitude.FormatInvariant()}&navigate=yes:");

            Intent intent = new Intent(Intent.ActionView, uri);

            intent.AddFlags(ActivityFlags.NewTask);

            context.StartActivity(intent);
        }

        public void MaintainBadgeCount()
        {

        }

        public async Task SaveFile(ReportFile reportFile)
        {
            try
            {
                PP.Abstractions.PermissionStatus status = await PermissionHelper.RequestPermission<PP.StoragePermission>(PP.Abstractions.Permission.Storage);

                if (status != PP.Abstractions.PermissionStatus.Granted)
                {
                    return;
                }

                var context = Android.App.Application.Context;

                var appInfo = context.PackageManager.GetApplicationInfo(context.PackageName, 0);

                string packageName = context.PackageName;

                var bytes = Convert.FromBase64String(reportFile.FihdBytes);

                var decoded = System.Text.Encoding.UTF8.GetString(bytes);

                string fileName = $"{reportFile.FihdLabel}{reportFile.FihdType}";

                //Copy the private file's data to the EXTERNAL PUBLIC location
                string externalStorageState = global::Android.OS.Environment.ExternalStorageState;

                var externalPath = Android.OS.Environment.ExternalStorageDirectory.Path + "/" + global::Android.OS.Environment.DirectoryDownloads + "/" + fileName;

                File.WriteAllBytes(externalPath, bytes);

                Java.IO.File file = new Java.IO.File(externalPath);

                file.SetReadable(true);

                string mimeType = string.Empty;

                string extension = System.IO.Path.GetExtension(externalPath);

                switch (extension.ToLower())
                {
                    case ".txt":
                        mimeType = "text/plain";
                        break;
                    case ".doc":
                    case ".docx":
                        mimeType = "application/msword";
                        break;
                    case ".pdf":
                        mimeType = "application/pdf";
                        break;
                    case ".xls":
                    case ".xlsx":
                        mimeType = "application/vnd.ms-excel";
                        break;
                    case ".jpg":
                    case ".jpeg":
                    case ".png":
                        mimeType = "image/jpeg";
                        break;
                    default:
                        mimeType = "*/*";
                        break;
                }

                Intent intent = new Intent(Intent.ActionView);
                intent.SetFlags(ActivityFlags.ClearTop | ActivityFlags.NewTask);

                Android.Net.Uri uri = FileProvider.GetUriForFile(
                            context,
                            packageName + ".fileprovider", file);

                intent.SetDataAndType(uri, mimeType);
                intent.AddFlags(ActivityFlags.GrantReadUriPermission);

                context.StartActivity(intent);
            }
            catch (Exception ex)
            {
                var renderService = DependencyResolver.Resolve<IUIRenderService>();

                string message = renderService.Translate("InstallDocViewerMessage");

                await ShowToast(message);

                LogHelper.LogException("SaveFile-Exception occured", ex);
            }
        }

        public string GetDeviceId()
        {
            var settingsService = DependencyResolver.Resolve<ISettingsService>();

            if (string.IsNullOrEmpty(settingsService.UniqueDeviceId))
            {
                settingsService.UniqueDeviceId = Guid.NewGuid().ToString();
            }

            return settingsService.UniqueDeviceId;
        }

        public string ResizeImage(string source, double width, double height)
        {
            byte[] imageAsBytes = Base64.Decode(source, Base64Flags.Default);

            var options = new BitmapFactory.Options()
            {

            };

            var bitmap = BitmapFactory.DecodeByteArray(imageAsBytes, 0, imageAsBytes.Length, options);

            var scaledBitmap = Bitmap.CreateScaledBitmap(bitmap, (int)width, (int)height, false);

            string bitmapDataString = string.Empty;

            using (var stream = new MemoryStream())
            {
                scaledBitmap.Compress(Bitmap.CompressFormat.Png, 0, stream);

                bitmapDataString = Convert.ToBase64String(stream.ToArray());
            }

            return bitmapDataString;
        }

        public bool IsVoiceOverOn()
        {
            bool isVoiceOverOn = false;

            try
            {
                var context = Android.App.Application.Context;

                AccessibilityManager accessibilityManager = (AccessibilityManager)context.GetSystemService(Context.AccessibilityService);

                isVoiceOverOn = accessibilityManager.IsEnabled;
            }
            catch (Exception ex)
            {
                LogHelper.LogException("IsVoiceOverOn-Exception occured", ex);
            }

            return isVoiceOverOn;
        }

        public void PostNotification(string message, int type = 0)
        {
            try
            {
                bool isVoiceOverOn = false;

                var context = Android.App.Application.Context;

                AccessibilityManager accessibilityManager = (AccessibilityManager)context.GetSystemService(Context.AccessibilityService);

                isVoiceOverOn = accessibilityManager.IsEnabled;

                if (isVoiceOverOn)
                {
                    //TODO
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogException("PostNotification-Exception occured", ex);
            }

        }

        public Task<byte[]> ResizeImage(byte[] imageData, int quality, bool toRotate)
        {
            byte[] resized = null;
            try
            {
                Bitmap originalImage = BitmapFactory.DecodeByteArray(imageData, 0, imageData.Length);

                using (MemoryStream ms = new MemoryStream())
                {
                    originalImage.Compress(Bitmap.CompressFormat.Jpeg, quality, ms);
                    resized = ms.ToArray();
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogException("ResizeImage-Exception occured", ex);
            }

            return Task.FromResult(resized);
        }

        public void SetStatusBarColor(XF.Color color)
        {
            try
            {
                if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop)
                {
                    var androidColor = color.AddLuminosity(-0.1f).ToAndroid();
                    //Just use the plugin
                    XE.Platform.CurrentActivity.Window.SetStatusBarColor(androidColor);
                }
                else
                {
                    // Here you will just have to set your 
                    // color in styles.xml file as shown below.
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogException("SetStatusBarColor-Exception occured", ex);
            }
        }
    }
}
