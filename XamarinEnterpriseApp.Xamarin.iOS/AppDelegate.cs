
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Firebase.CloudMessaging;
using Firebase.InstanceID;
using Foundation;
using XamarinEnterpriseApp.Xamarin.Core;
using XamarinEnterpriseApp.Xamarin.Core.Constants;
using XamarinEnterpriseApp.Xamarin.Core.Dependency;
using XamarinEnterpriseApp.Xamarin.Core.Helpers;
using XamarinEnterpriseApp.Xamarin.Core.Ioc;
using XamarinEnterpriseApp.Xamarin.Core.Models;
using XamarinEnterpriseApp.Xamarin.Core.Services;
using XamarinEnterpriseApp.Xamarin.iOS.Dependency;
using XamarinEnterpriseApp.Xamarin.iOS.Helpers;
using UIKit;
using UserNotifications;
using Xamarin;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps.iOS;
using XGM = Xamarin.FormsGoogleMaps;
using Xamarin.Forms.Platform.iOS;

namespace XamarinEnterpriseApp.Xamarin.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate, IUNUserNotificationCenterDelegate, IMessagingDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
#if ENABLE_TEST_CLOUD
            Calabash.Start();
#endif
            global::Xamarin.Forms.Forms.SetFlags("SwipeView_Experimental");
            global::Xamarin.Forms.Forms.Init();

            ConfigureFirebaseServices();

            var platformConfig = new PlatformConfig
            {
                ImageFactory = new CachingImageFactory()
            };

            XGM.Init(ApiConstants.MapsAPIKey, platformConfig);

            if (options != null)
            {
                if (options.ContainsKey(UIApplication.LaunchOptionsRemoteNotificationKey))
                {
                    NSDictionary userInfo = (NSDictionary)options[UIApplication.LaunchOptionsRemoteNotificationKey];

                    if (userInfo != null)
                    {
                        var pushNotificationData = PushHelper.GetPushNotificationData(userInfo);

                        if (pushNotificationData != null)
                        {
                            GlobalSetting.Instance.PushNotificationData = pushNotificationData;
                        }
                    }
                }
            }

            LoadApplication(new App());

            RegisterForRemoteNotifications(app);

            return base.FinishedLaunching(app, options);
        }

        private void RegisterForRemoteNotifications(UIApplication app)
        {
            try
            {
                Firebase.Core.App.Configure();

                // Register your app for remote notifications.
                if (UIDevice.CurrentDevice.CheckSystemVersion(10, 0))
                {

                    // For iOS 10 display notification (sent via APNS)
                    UNUserNotificationCenter.Current.Delegate = this;

                    var authOptions = UNAuthorizationOptions.Alert |
                        UNAuthorizationOptions.Badge |
                        UNAuthorizationOptions.Sound;
                    UNUserNotificationCenter.Current.RequestAuthorization(authOptions, (granted, error) =>
                    {
                        if (!granted)
                        {
                            //TODO log
                        }

                    });
                }
                else
                {
                    // iOS 9 or before
                    var allNotificationTypes = UIUserNotificationType.Alert |
                        UIUserNotificationType.Badge |
                        UIUserNotificationType.Sound;
                    var settings = UIUserNotificationSettings.GetSettingsForTypes(allNotificationTypes, null);

                    UIApplication.SharedApplication.RegisterUserNotificationSettings(settings);
                }

                UIApplication.SharedApplication.RegisterForRemoteNotifications();

                Messaging.SharedInstance.Delegate = this;

                InstanceId.SharedInstance.GetInstanceId(InstanceIdResultHandler);
            }
            catch (Exception ex)
            {
                LogException(ex);
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

        private void InstanceIdResultHandler(InstanceIdResult result, NSError error)
        {
            if (error != null)
            {
                LogHelper.TrackEvent(nameof(InstanceIdResultHandler), $"Error: {error.LocalizedDescription}");
                return;
            }
        }

        [Export("messaging:didReceiveRegistrationToken:")]
        public void DidReceiveRegistrationToken(Messaging messaging, string fcmToken)
        {
            LogHelper.TrackEvent("messaging2:didReceiveRegistrationToken", fcmToken);

            Task.Run(async () =>
            {
                await SendRegistrationTokenToNotificationHub(fcmToken);
            });
        }

        public override void DidReceiveRemoteNotification(UIApplication application, NSDictionary userInfo, Action<UIBackgroundFetchResult> completionHandler)
        {
            // Handle Notification messages in the background and foreground.
            // Handle Data messages for iOS 9 and below.

            // If you are receiving a notification message while your app is in the background,
            // this callback will not be fired till the user taps on the notification launching the application.
            // TODO: Handle data of notification

            try
            {
                ProcessNotification(userInfo, false, true);

                completionHandler(UIBackgroundFetchResult.NewData);
            }
            catch (Exception ex)
            {
                LogException(ex);
            }
        }

        public override void ReceivedRemoteNotification(UIApplication application, NSDictionary userInfo)
        {
            if (userInfo != null)
            {
                try
                {
                    ProcessNotification(userInfo, false, true);
                }
                catch (Exception ex)
                {
                    LogException(ex);
                }
            }
        }

        [Export("messaging:didReceiveMessage:")]
        public void DidReceiveMessage(Messaging messaging, RemoteMessage remoteMessage)
        {
            // Handle Data messages for iOS 10 and above.
            try
            {
                ProcessNotification(remoteMessage.AppData, false, false);
            }
            catch (Exception ex)
            {
                LogException(ex);
            }
        }

        [Export("userNotificationCenter:willPresentNotification:withCompletionHandler:")]
        public void WillPresentNotification(UNUserNotificationCenter center, UNNotification notification, Action<UNNotificationPresentationOptions> completionHandler)
        {
            Console.WriteLine("Handling iOS 11 foreground notification");

            completionHandler(UNNotificationPresentationOptions.Sound | UNNotificationPresentationOptions.Alert | UNNotificationPresentationOptions.Badge);
        }

        private void ProcessNotification(NSDictionary userInfo, bool isFromResponse, bool isFromRemoteNotification)
        {
            var pushNotificationData = PushHelper.GetPushNotificationData(userInfo);

            GlobalSetting.Instance.PushNotificationData = pushNotificationData;

            bool isActive = UIApplication.SharedApplication?.ApplicationState == UIApplicationState.Active;

            if (isFromResponse)
            {
                bool isNotified = GlobalSetting.Instance.FirePushNotificationEvent();
            }
            else
            {
                var platformManager = DependencyService.Get<IPlatformManager>();

                platformManager?.IncrementBadgeCount();

                if (isActive)
                {
                    //handled in will present
                }
                else
                {
                    if (!isFromRemoteNotification)
                    {
                        PushHelper.SendNotification(pushNotificationData.Body, pushNotificationData.Title, 0, userInfo);
                    }
                }
            }
        }

        private async Task<UNNotificationSettings> GetNotificationSettingsAsync()
        {
            UNNotificationSettings settings = null;

            try
            {
                settings = await UNUserNotificationCenter.Current.GetNotificationSettingsAsync();
            }
            catch (Exception ex)
            {
                LogException(ex);
            }

            return settings;
        }

        //Notification tapping when the app is in the foreground mode.
        [Export("userNotificationCenter:didReceiveNotificationResponse:withCompletionHandler:")]
        public void DidReceiveNotificationResponse(UNUserNotificationCenter center, UNNotificationResponse response, Action
                    completionHandler)
        {
            var userInfo = response?.Notification?.Request?.Content?.UserInfo;

            if (response.IsDismissAction)
            {
                //TODO
            }
            else
            {
                if (userInfo != null)
                {
                    ProcessNotification(userInfo, true, false);
                }
            }

            completionHandler();
        }

        private void ConfigureFirebaseServices()
        {
            try
            {
            }
            catch (Exception ex)
            {
                LogException(ex);
            }
        }

        private void SetStatusBar()
        {
            try
            {
                var statusBarColor = Color.FromHex("#62DF85").ToUIColor();

                if (UIDevice.CurrentDevice.CheckSystemVersion(13, 0))
                {
                    UIView statusBar = new UIView(UIApplication.SharedApplication.KeyWindow.WindowScene.StatusBarManager.StatusBarFrame);
                    statusBar.BackgroundColor = statusBarColor;
                    UIApplication.SharedApplication.KeyWindow.AddSubview(statusBar);
                }
                else
                {
                    UIView statusBar = UIApplication.SharedApplication.ValueForKey(new NSString("statusBar")) as UIView;
                    if (statusBar.RespondsToSelector(new ObjCRuntime.Selector("setBackgroundColor:")))
                    {
                        statusBar.BackgroundColor = statusBarColor;
                        statusBar.TintColor = UIColor.White;
                        UIApplication.SharedApplication.StatusBarStyle = UIStatusBarStyle.LightContent;
                    }
                }
            }
            catch (Exception ex)
            {
                LogException(ex);
            }
        }

        public override void OnActivated(UIApplication uiApplication)
        {
            //SetStatusBar();
        }

        private void LogException(Exception ex)
        {
            LogHelper.LogException("ApplicationDelegate-Exception occured", ex);
        }
    }
}
