using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using XamarinEnterpriseApp.Xamarin.Core.Constants;
using XamarinEnterpriseApp.Xamarin.Core.Dependency;
using XamarinEnterpriseApp.Xamarin.Core.Localization;
using XamarinEnterpriseApp.Xamarin.Core.Models;
using XamarinEnterpriseApp.Xamarin.Core.Repositories;
using XamarinEnterpriseApp.Xamarin.Core.Services;
using XamarinEnterpriseApp.Xamarin.Core.ViewModels.Base;
using Xamarin.Forms;

namespace XamarinEnterpriseApp.Xamarin.Core.ViewModels
{
    public class InitialViewModel : ViewModelBase
    {
        #region Fields

        #endregion

        #region Actions

        #endregion

        #region Bindables

        private ObservableCollection<GalleryListItemModel> _introItems;
        public ObservableCollection<GalleryListItemModel> IntroItems
        {
            get => _introItems;
            set
            {
                _introItems = value;
                RaisePropertyChanged(() => IntroItems);
            }
        }

        #endregion

        #region Commands


        #endregion

        public InitialViewModel(IConnectionService connectionService, INavigationService navigationService,
                                  IDialogService dialogService, ISettingsService settingsService,
                                  IAnalyticsService analyticsService, IApplicationDataService applicationDataService,
                                  IUserService userService, ILocalDbContextService localDbContextService)
          : base(connectionService, navigationService, dialogService, settingsService, analyticsService, applicationDataService, userService, localDbContextService)
        {

        }

        public override async Task InitializeAsync(object navigationData = null)
        {
            IntroItems = new ObservableCollection<GalleryListItemModel>() {
                new GalleryListItemModel(){
                    ImageLocal = "introImage1.png",
                    Header = AppResources.Intro1Header,
                    SubHeader =AppResources.Intro1Desc,
                    BackgroundColor = "#62DF85",
                    TitleColor = "#313143",
                    TextColor = "#28284D",
                    ImageAcc = AppResources.Intro1ImageDesc,
                } ,
                new GalleryListItemModel(){
                    ImageLocal = "introImage2.png",
                    Header = AppResources.Intro2Header,
                    SubHeader =AppResources.Intro2Desc,
                    BackgroundColor = "#7B61FF",
                    TitleColor = "#ffffff",
                    TextColor = "#ffffff",
                    ImageAcc = AppResources.Intro2ImageDesc,
                },
                new GalleryListItemModel(){
                    ImageLocal = "introImage3.png",
                    Header = AppResources.Intro3Header,
                    SubHeader =AppResources.Intro3Desc,
                    BackgroundColor = "#E28733",
                    TitleColor = "#ffffff",
                    TextColor = "#ffffff",
                    ImageAcc = AppResources.Intro2ImageDesc,
                },
                new GalleryListItemModel(){
                    ImageLocal = "introImage4.png",
                    Header = AppResources.Intro4Header,
                    SubHeader =AppResources.Intro4Desc,
                    BackgroundColor = "#F9C795",
                    TitleColor = "#313143",
                    TextColor = "#313143",
                    ImageAcc = AppResources.Intro4ImageDesc,
                }
            };

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

            if (IsAccessibilityOn)
            {
                _platformManager.PostNotification(AppResources.IntroDoubleTapToNextScreen);
            }
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
