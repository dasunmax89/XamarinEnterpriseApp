using System;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using XamarinEnterpriseApp.Xamarin.Core.Dependency;
using XamarinEnterpriseApp.Xamarin.Core.Helpers;
using XamarinEnterpriseApp.Xamarin.iOS.Dependency;
using XF = Xamarin.Forms;
using System.Linq;
using System.Diagnostics;
using UserNotifications;
using System.Threading.Tasks;
using Foundation;
using UIKit;
using XamarinEnterpriseApp.Xamarin.Core.Ioc;
using XamarinEnterpriseApp.Xamarin.Core.Services;
using XamarinEnterpriseApp.Xamarin.iOS.Helpers;
using System.Collections.Generic;
using XamarinEnterpriseApp.Xamarin.Core.Models;
using XamarinEnterpriseApp.Xamarin.Core.Extensions;
using System.IO;
using XamarinEnterpriseApp.Xamarin.iOS.Delegates;
using System.Text.RegularExpressions;
using System.Drawing;
using XamarinEnterpriseApp.Xamarin.Core;
using CoreGraphics;
using Xamarin.Forms.Platform.iOS;

[assembly: XF.Dependency(typeof(PlatformManager))]
namespace XamarinEnterpriseApp.Xamarin.iOS.Dependency
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
                var netInterfaces = NetworkInterface.GetAllNetworkInterfaces();

                foreach (var netInterface in netInterfaces)
                {
                    if (netInterface.NetworkInterfaceType == NetworkInterfaceType.Wireless80211 ||
                        netInterface.NetworkInterfaceType == NetworkInterfaceType.Ethernet)
                    {
                        foreach (var addrInfo in netInterface.GetIPProperties().UnicastAddresses)
                        {
                            if (addrInfo.Address.AddressFamily == AddressFamily.InterNetwork)
                            {
                                ipAddress = addrInfo.Address.ToString();

                                if (!string.IsNullOrEmpty(ipAddress))
                                    break;
                            }
                        }
                    }
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
            UNNotificationSettings settings = null;

            bool isEnabled = false;
            try
            {
                settings = await UNUserNotificationCenter.Current.GetNotificationSettingsAsync();

                isEnabled = (settings?.AlertSetting == UNNotificationSetting.Enabled);

                if (!isEnabled && invokeSettings)
                {
                    OpenPushSettings();
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogException("CheckPushEnabled - Error", ex);

                await ShowToast(ex.Message);
            }

            return isEnabled;
        }

        protected async Task ShowToast(string message)
        {
            var dialogService = DependencyResolver.Resolve<IDialogService>();
            await dialogService.ShowToast(message);
        }

        public void IncrementBadgeCount(bool isDecrement = false)
        {
            if (UIApplication.SharedApplication != null)
            {
                if (isDecrement)
                {
                    UIApplication.SharedApplication.ApplicationIconBadgeNumber--;
                }
                else
                {
                    UIApplication.SharedApplication.ApplicationIconBadgeNumber++;
                }
            }
        }

        public async void MaintainBadgeCount()
        {
            try
            {
                var pendingItems = await UNUserNotificationCenter.Current.GetDeliveredNotificationsAsync();

                int count = pendingItems == null ? 0 : pendingItems.Count();

                if (UIApplication.SharedApplication != null)
                {
                    UIApplication.SharedApplication.ApplicationIconBadgeNumber = count;
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogException("CheckBadgeCount - Error", ex);
            }
        }

        public async void MarkAsRead(long reportId, long custId)
        {
            try
            {
                if (GlobalSetting.Instance.PushNotificationData != null)
                {
                    GlobalSetting.Instance.PushNotificationData.IsProcessed = true;
                }

                var pendingItems = await UNUserNotificationCenter.Current.GetDeliveredNotificationsAsync();

                if (pendingItems == null)
                {
                    return;
                }

                foreach (var pendingItem in pendingItems)
                {
                    bool isRemoving = false;

                    var userInfo = pendingItem.Request?.Content?.UserInfo;

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
                        string[] ids = new string[] { pendingItem.Request.Identifier };

                        UNUserNotificationCenter.Current.RemoveDeliveredNotifications(ids);
                    }
                }

                MaintainBadgeCount();
            }
            catch (Exception ex)
            {
                LogHelper.LogException("MarkAsRead - Error", ex);
            }
        }

        public bool CheckPackageInstalled(string packageName)
        {
            bool installed = false;

            try
            {
                NSUrl url = NSUrl.FromString(packageName);
                installed = UIApplication.SharedApplication.CanOpenUrl(url);
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

            listItems.Add(new ListItemModel()
            {
                ViewModel = null,
                Header = "Apple Maps",
                Identifier = "iosmaps://",
                IconSource = "Map_blue.png",
                Tag = position,
            });

            string[] urlNames = new string[] {
                "comgooglemaps://",
                "waze://"};

            foreach (var packageName in urlNames)
            {
                bool isInstalled = CheckPackageInstalled(packageName);
                if (isInstalled)
                {
                    string appName = string.Empty;

                    switch (packageName)
                    {
                        case "comgooglemaps://":
                            appName = "Google Maps";
                            break;
                        case "uber://":
                            appName = "Uber";
                            break;
                        case "waze://":
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

        public async void OpenGoogleMaps(GeographicLocation position)
        {
            var uri = NSUrl.FromString($"comgooglemaps://?dir_action=navigate&saddr=&daddr={position.Latitude.FormatInvariant()},{position.Longitude.FormatInvariant()}");

            await UIApplication.SharedApplication.OpenUrlAsync(uri, new UIApplicationOpenUrlOptions());
        }

        public async void OpenUber(GeographicLocation position)
        {
            var uri = NSUrl.FromString($"uber://?action=setPickup&dropoff[latitude]={position.Latitude.FormatInvariant()}&dropoff[longitude]={position.Longitude.FormatInvariant()}");

            await UIApplication.SharedApplication.OpenUrlAsync(uri, new UIApplicationOpenUrlOptions());
        }

        public async void OpenWaze(GeographicLocation position)
        {
            var uri = NSUrl.FromString($"waze://?ll={position.Latitude.FormatInvariant()},{position.Longitude.FormatInvariant()}&navigate=yes:");

            await UIApplication.SharedApplication.OpenUrlAsync(uri, new UIApplicationOpenUrlOptions());
        }

        public async Task SaveFile(ReportFile reportFile)
        {

            try
            {
                var bytes = Convert.FromBase64String(reportFile.FihdBytes);

                var decoded = System.Text.Encoding.UTF8.GetString(bytes);

                string extention = reportFile.FihdType.ReplaceFileNames();

                string fileName = $"{reportFile.FihdLabel}{extention}";

                var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);

                var filePath = Path.Combine(documentsPath, fileName);

                File.WriteAllBytes(filePath, bytes);

                var previewController = UIDocumentInteractionController.FromUrl(NSUrl.FromFilename(filePath));

                previewController.Delegate = new UIDocumentInteractionControllerDelegateHandler(UIApplication.SharedApplication.KeyWindow.RootViewController);

                XF.Device.BeginInvokeOnMainThread(() =>
                {
                    previewController.PresentPreview(true);
                });
            }
            catch (Exception ex)
            {
                var renderService = DependencyResolver.Resolve<IUIRenderService>();

                string message = renderService.Translate("InstallDocViewerMessage");

                await ShowToast(message);

                LogHelper.LogException("SaveFile-Exception occured", ex);
            }

            await Task.FromResult(true);
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
            string resized = string.Empty;

            try
            {
                byte[] imageData = Convert.FromBase64String(source);

                UIImage originalImage = new UIKit.UIImage(Foundation.NSData.FromArray(imageData));

                var originalHeight = originalImage.Size.Height;
                var originalWidth = originalImage.Size.Width;

                double newHeight = 0;
                double newWidth = 0;

                if (originalHeight > originalWidth)
                {
                    newHeight = height;
                    double ratio = originalHeight / height;
                    newWidth = originalWidth / ratio;
                }
                else
                {
                    newWidth = width;
                    double ratio = originalWidth / width;
                    newHeight = originalHeight / ratio;
                }

                width = newWidth;
                height = newHeight;

                UIGraphics.BeginImageContext(new SizeF((float)width, (float)height));
                originalImage.Draw(new RectangleF(0, 0, (float)width, (float)height));
                var resizedImage = UIGraphics.GetImageFromCurrentImageContext();
                UIGraphics.EndImageContext();

                var bytesImagen = resizedImage.AsPNG().ToArray();

                resizedImage.Dispose();

                resized = Convert.ToBase64String(bytesImagen);

            }
            catch (Exception ex)
            {
                LogHelper.LogException("ResizeImage-Exception occured", ex);
            }

            return resized;
        }

        public void OpenPushSettings()
        {
            UIApplication.SharedApplication.OpenUrl(new NSUrl("app-settings:"));
        }

        public bool IsVoiceOverOn()
        {
            bool isVoiceOverOn = UIAccessibility.IsVoiceOverRunning;

            return isVoiceOverOn;
        }

        public async void PostNotification(string message, int type = 0)
        {
            bool isVoiceOverOn = UIAccessibility.IsVoiceOverRunning;

            if (isVoiceOverOn)
            {
                if (type == 0)
                {
                    await Task.Delay(2000).ContinueWith(x => UIAccessibility.PostNotification((UIAccessibilityPostNotification)(type), new NSString(message)));
                }
                else
                {
                    UIAccessibility.PostNotification((UIAccessibilityPostNotification)(type), new NSString(message));
                }
            }
        }

        public Task<byte[]> ResizeImage(byte[] imageData, int quality, bool toRotate)
        {
            byte[] resized = null;

            try
            {
                UIImage originalImage = new UIImage(Foundation.NSData.FromArray(imageData));

                UIImageOrientation orientation = originalImage.Orientation;

                //create a 24bit RGB image

                if (toRotate)
                {
                    var rotatedImage = FileHelper.RotateImage(originalImage, UIImageOrientation.Right);

                    resized = rotatedImage.AsJPEG(quality / 100).ToArray();
                }
                else
                {
                    resized = originalImage.AsJPEG(quality / 100).ToArray();
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
                if (UIDevice.CurrentDevice.CheckSystemVersion(13, 0))
                {
                    UIView statusBar = new UIView(UIApplication.SharedApplication.KeyWindow.WindowScene.StatusBarManager.StatusBarFrame);
                    statusBar.BackgroundColor = color.ToUIColor();
                    UIApplication.SharedApplication.KeyWindow.AddSubview(statusBar);
                }
                else
                {
                    UIView statusBar = UIApplication.SharedApplication.ValueForKey(new NSString("statusBar")) as UIView;
                    if (statusBar.RespondsToSelector(new ObjCRuntime.Selector("setBackgroundColor:")))
                    {
                        statusBar.BackgroundColor = color.ToUIColor();
                        statusBar.TintColor = UIColor.White;
                        UIApplication.SharedApplication.StatusBarStyle = UIStatusBarStyle.LightContent;
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogException("SetStatusBarColor-Exception occured", ex);
            }
        }
    }
}
