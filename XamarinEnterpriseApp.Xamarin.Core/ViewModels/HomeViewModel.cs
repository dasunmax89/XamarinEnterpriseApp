using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using XamarinEnterpriseApp.Xamarin.Core.Constants;
using XamarinEnterpriseApp.Xamarin.Core.Dependency;
using XamarinEnterpriseApp.Xamarin.Core.Extensions;
using XamarinEnterpriseApp.Xamarin.Core.Localization;
using XamarinEnterpriseApp.Xamarin.Core.Models;
using XamarinEnterpriseApp.Xamarin.Core.Repositories;
using XamarinEnterpriseApp.Xamarin.Core.Services;
using XamarinEnterpriseApp.Xamarin.Core.ViewModels.Base;
using Xamarin.Forms;
namespace XamarinEnterpriseApp.Xamarin.Core.ViewModels
{
    public class HomeViewModel : ViewModelBase
    {
        #region Fields

        #endregion

        #region Actions

        #endregion

        #region Bindables

        #endregion

        #region Commands


        #endregion

        public HomeViewModel(IConnectionService connectionService, INavigationService navigationService,
                                  IDialogService dialogService, ISettingsService settingsService,
                                  IAnalyticsService analyticsService, IApplicationDataService applicationDataService,
                                  IUserService userService, ILocalDbContextService localDbContextService)
          : base(connectionService, navigationService, dialogService, settingsService, analyticsService, applicationDataService, userService, localDbContextService)
        {

        }

        public override async Task InitializeAsync(object navigationData = null)
        {
            await AttemptUpdateDeviceToken();

            TrackView();
        }

        public override async Task OnAppearing()
        {
            await SetTokenToClipBoard();

            if (GlobalSetting.Instance.PushNotificationData != null)
            {
                //to allow to main view to setup
                await Task.Delay(400);
                await NotifyPushNotification();
            }

            _platformManager.SetStatusBarColor(Color.FromHex("#63DF85"));

            await RequestPermisisons(Plugin.Permissions.Abstractions.Permission.Location);
        }

        public override async Task NavigateToBack()
        {
            await _navigationService.NavigateToBack();
        }

        public override void SubscribeToEvents(object viewArgs = null)
        {
            MessagingCenter.Subscribe<GlobalSetting, PushNotificationData>(GlobalSetting.Instance, MessageKeys.OpenReportRequested, async (sender, data) =>
            {
                await OpenAndNavigateToReportNotification(data);
            });
        }

        public override void UnSubscribeToEvents()
        {
            MessagingCenter.Unsubscribe<GlobalSetting, PushNotificationData>(GlobalSetting.Instance, MessageKeys.OpenReportRequested);
        }
    }
}
