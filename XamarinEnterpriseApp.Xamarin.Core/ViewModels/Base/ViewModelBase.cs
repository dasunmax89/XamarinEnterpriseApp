using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using GeoCoordinatePortable;
using XamarinEnterpriseApp.Xamarin.Core.Constants;
using XamarinEnterpriseApp.Xamarin.Core.Dependency;
using XamarinEnterpriseApp.Xamarin.Core.Extensions;
using XamarinEnterpriseApp.Xamarin.Core.Helpers;
using XamarinEnterpriseApp.Xamarin.Core.Ioc;
using XamarinEnterpriseApp.Xamarin.Core.Localization;
using XamarinEnterpriseApp.Xamarin.Core.Models;
using XamarinEnterpriseApp.Xamarin.Core.Repositories;
using XamarinEnterpriseApp.Xamarin.Core.Repositories.Entities;
using XamarinEnterpriseApp.Xamarin.Core.Services;
using XamarinEnterpriseApp.Xamarin.Core.Views;
using Xamarin.Essentials;
using Xamarin.Forms;
using PP = Plugin.Permissions;

namespace XamarinEnterpriseApp.Xamarin.Core.ViewModels.Base
{
    public abstract class ViewModelBase : ExtendedBindableObject
    {
        protected readonly IConnectionService _connectionService;
        protected readonly INavigationService _navigationService;
        protected readonly IDialogService _dialogService;
        protected readonly ISettingsService _settingsService;
        protected readonly IAnalyticsService _analyticsService;
        protected readonly IApplicationDataService _applicationDataService;
        protected readonly IUserService _userService;
        protected readonly ILocalDbContextService _localDbContext;
        protected readonly IPlatformManager _platformManager;

        public ViewModelBase(IConnectionService connectionService, INavigationService navigationService,
            IDialogService dialogService, ISettingsService settingsService,
            IAnalyticsService analyticsService, IApplicationDataService applicationDataService,
            IUserService userService,
            ILocalDbContextService localDbContext)
        {
            _connectionService = connectionService;
            _navigationService = navigationService;
            _dialogService = dialogService;
            _settingsService = settingsService;
            _analyticsService = analyticsService;
            _applicationDataService = applicationDataService;
            _userService = userService;
            _localDbContext = localDbContext;
            _platformManager = DependencyService.Get<IPlatformManager>();
        }

        #region Commands

        public ICommand SelectListOkCommand { protected set; get; }

        public ICommand ActionItemTappedCommand { protected set; get; }

        public ICommand TextboxCompletedCommand { protected set; get; }

        public ICommand TextboxFocusedCommand { protected set; get; }

        #endregion

        public bool IsInitialized { get; set; }

        private bool _isBusy;
        public bool IsBusy
        {
            get
            {
                return _isBusy;
            }

            set
            {
                _isBusy = value;

                RaisePropertyChanged(() => IsBusy);

                if (_isBusy)
                {
                    _platformManager?.PostNotification(AppResources.Loading);
                }
            }
        }

        private bool _isBackgroundBusy;
        public bool IsBackgroundBusy
        {
            get
            {
                return _isBackgroundBusy;
            }

            set
            {
                _isBackgroundBusy = value;
                RaisePropertyChanged(() => IsBackgroundBusy);
            }
        }

        private bool _isPopupVisible;
        public bool IsPopupVisible
        {
            get
            {
                return _isPopupVisible;
            }

            set
            {
                _isPopupVisible = value;

                if (value && _platformManager != null && RadioButtonHelpText != null)
                {
                    _platformManager?.PostNotification(RadioButtonHelpText, 3);
                }
                else
                {
                    _platformManager?.PostNotification(string.Empty, 3);
                }

                RaisePropertyChanged(() => IsPopupVisible);
            }
        }

        private string _radioButtonHelpText;
        public string RadioButtonHelpText
        {
            get
            {
                return _radioButtonHelpText;
            }

            set
            {
                _radioButtonHelpText = value;
                RaisePropertyChanged(() => RadioButtonHelpText);
            }
        }

        private bool _isScreenGuideVisible;
        public bool IsScreenGuideVisible
        {
            get
            {
                return _isScreenGuideVisible;
            }

            set
            {
                _isScreenGuideVisible = value;
                _platformManager?.PostNotification(string.Empty, 3);
                RaisePropertyChanged(() => IsScreenGuideVisible);
            }
        }

        private bool _isObjectInfoIconVisible;
        public bool IsObjectInfoIconVisible
        {
            get
            {
                return _isObjectInfoIconVisible;
            }

            set
            {
                _isObjectInfoIconVisible = value;
                RaisePropertyChanged(() => IsObjectInfoIconVisible);
            }
        }

        private bool _isPopupFilterVisible;
        public bool IsPopupFilterVisible
        {
            get
            {
                return _isPopupFilterVisible;
            }

            set
            {
                _isPopupFilterVisible = value;
                RaisePropertyChanged(() => IsPopupFilterVisible);
            }
        }

        private bool _isAutoCompleteListVisible;
        public bool IsAutoCompleteListVisible
        {
            get { return _isAutoCompleteListVisible; }
            set
            {
                _isAutoCompleteListVisible = value;
                _platformManager?.PostNotification(string.Empty, 3);
                RaisePropertyChanged(() => IsAutoCompleteListVisible);
            }
        }

        private ObservableCollection<ListItemModel> _autoCompletePredictions;
        public ObservableCollection<ListItemModel> AutoCompletePredictions
        {
            get => _autoCompletePredictions;
            set
            {
                _autoCompletePredictions = value;
                RaisePropertyChanged(() => AutoCompletePredictions);
            }
        }

        private bool _isFullScreenPopupVisible;
        public bool IsFullScreenPopupVisible
        {
            get
            {
                return _isFullScreenPopupVisible;
            }

            set
            {
                _isFullScreenPopupVisible = value;
                _platformManager?.PostNotification(string.Empty, 3);
                RaisePropertyChanged(() => IsFullScreenPopupVisible);
            }
        }

        private bool _isInfoPopupVisible;
        public bool IsInfoPopupVisible
        {
            get
            {
                return _isInfoPopupVisible;
            }

            set
            {
                _isInfoPopupVisible = value;
                _platformManager?.PostNotification(string.Empty, 3);
                RaisePropertyChanged(() => IsInfoPopupVisible);
            }
        }

        private GalleryListItemModel _selectedFullScreenPopupItem;
        public GalleryListItemModel SelectedFullScreenPopupItem
        {
            get
            {
                return _selectedFullScreenPopupItem;
            }

            set
            {
                _selectedFullScreenPopupItem = value;
                _platformManager?.PostNotification(string.Empty, 3);
                RaisePropertyChanged(() => SelectedFullScreenPopupItem);
            }
        }

        private bool _isActionsheetVisible;
        public bool IsActionsheetVisible
        {
            get
            {
                return _isActionsheetVisible;
            }

            set
            {
                _isActionsheetVisible = value;
                _platformManager?.PostNotification(string.Empty, 3);
                RaisePropertyChanged(() => IsActionsheetVisible);
            }
        }

        private string _pageTitle;
        public string PageTitle
        {
            get
            {
                return _pageTitle;
            }

            set
            {
                _pageTitle = value;
                RaisePropertyChanged(() => PageTitle);
            }
        }

        ObservableCollection<RadioButtonItemModel> _popupListItems;
        public ObservableCollection<RadioButtonItemModel> PopupListItems
        {
            get
            {
                return _popupListItems;
            }
            set
            {
                _popupListItems = value;
                RaisePropertyChanged(() => PopupListItems);
            }
        }

        ObservableCollection<ListItemModel> _actionListItems;
        public ObservableCollection<ListItemModel> ActionListItems
        {
            get
            {
                return _actionListItems;
            }
            set
            {
                _actionListItems = value;
                RaisePropertyChanged(() => ActionListItems);
            }
        }

        private string _popupTitle;
        public string PopupTitle
        {
            get { return _popupTitle; }
            set
            {
                _popupTitle = value;
                RaisePropertyChanged(() => PopupTitle);
            }
        }

        private bool _hasData;
        public bool HasData
        {
            get
            {
                return _hasData;
            }

            set
            {
                _hasData = value;
                RaisePropertyChanged(() => HasData);
            }
        }

        public bool IsLogin
        {
            get
            {
                return GlobalSetting.Instance.IsLoggedIn;
            }
        }

        public AddReportRequest AddReportRequest
        {
            get
            {
                return GlobalSetting.Instance.AddReportRequest;
            }

            set
            {
                GlobalSetting.Instance.AddReportRequest = value;
            }
        }

        public bool IsAndroid
        {
            get
            {
                return Device.RuntimePlatform == Device.Android;
            }
        }

        public bool IsIOS
        {
            get
            {
                return Device.RuntimePlatform == Device.iOS;
            }
        }

        public bool IsAccessibilityOn
        {
            get
            {
                return _platformManager.IsVoiceOverOn();
            }
        }

        public bool IsPickIntentOn
        {
            get
            {
                return GlobalSetting.Instance.IsPickIntentOn;
            }

            set
            {
                GlobalSetting.Instance.IsPickIntentOn = value;
            }
        }

        string _logoImageString;
        public string LogoImageString
        {
            get
            {
                return _logoImageString;
            }
            set
            {
                _logoImageString = value;
                RaisePropertyChanged(() => LogoImageString);
            }
        }

        string _logoGeementeText;
        public string LogoGeementeText
        {
            get
            {
                return string.Format(AppResources.LogoGeementeText, _settingsService.MunicipalityName);
            }
        }

        private string _activityMessage;
        private INavigationService navigationService;
        private IDialogService dialogService;
        private ISettingsService settingsService;
        private IAnalyticsService analyticsService;

        public string ActivityMessage
        {
            get
            {
                return _activityMessage;
            }

            set
            {
                _activityMessage = value;
                RaisePropertyChanged(() => ActivityMessage);
            }
        }

        public string AppVersionNumber => _analyticsService.AppVersionNumber;

        public string DevicePlatform => _analyticsService.DevicePlatform;

        public ViewModelBase()
        {
            _navigationService = DependencyResolver.Resolve<INavigationService>();
        }

        protected ViewModelBase(INavigationService navigationService, IDialogService dialogService, ISettingsService settingsService, IAnalyticsService analyticsService)
        {
            this.navigationService = navigationService;
            this.dialogService = dialogService;
            this.settingsService = settingsService;
            this.analyticsService = analyticsService;
        }

        public virtual Task InitializeAsync(object navigationData = null)
        {
            return Task.FromResult(false);
        }

        public virtual void SubscribeToEvents(object viewArgs = null)
        {

        }

        public virtual void UnSubscribeToEvents()
        {

        }

        public virtual Task OnAppearing()
        {
            return Task.FromResult(true);
        }

        public virtual Task OnDisappearing()
        {
            return Task.FromResult(true);
        }

        public virtual Task NavigateToBack()
        {
            return Task.FromResult(true);
        }

        public virtual Task OnDelete()
        {
            return Task.FromResult(true);
        }

        public virtual Task DisplayCameraActionsheet()
        {
            return Task.FromResult(true);
        }

        public virtual bool FormValidation()
        {
            return false;
        }

        public virtual void AccesibilityFocus()
        {
            if (IsAccessibilityOn)
            {
                _platformManager?.PostNotification(string.Empty, 3);
            }
        }

        public async Task ValidateTokenExpiry(BaseResponse response)
        {
            if (response.IsSessionExpired)
            {
                await _navigationService.PerformLogout();
            }
        }

        public async Task InvokeErrorMessage(BaseResponse response, string defaultErrorTitle, string defaultErrorMsg, bool isLogin = false)
        {
            if (response == null)
            {
                await _dialogService.ShowDialog(
                        defaultErrorMsg,
                        defaultErrorTitle,
                        AppResources.OK);

                return;
            }

            if (response.IsTaskCanceled)
            {
                await _dialogService.ShowDialog(
                          defaultErrorMsg,
                          defaultErrorTitle,
                          AppResources.OK);

                return;
            }

            if (response.IsSessionExpired && !isLogin)
            {
                await OnLogout(false);

                return;
            }

            RESTErrorResponse error = response.Error;

            await ShowError(error, defaultErrorTitle, defaultErrorMsg);
        }

        private async Task ShowError(RESTErrorResponse error, string defaultErrorTitle, string defaultErrorMsg)
        {
            string errorTitle = error != null && !string.IsNullOrEmpty(error.ResultTitle) ?
                                                        error.ResultTitle : defaultErrorTitle;
            string errorMsg = string.Empty;

            errorMsg = error != null && !string.IsNullOrEmpty(error.ResultMsgBody) ?
                                                                   error.ResultMsgBody : defaultErrorMsg;
            if (errorMsg.Contains("<!DOCTYPE html"))
            {
                errorMsg = defaultErrorMsg;
            }

            await _dialogService.ShowDialog(
                errorMsg,
                errorTitle,
                AppResources.OK);
        }

        protected async Task ShowToast(string message)
        {
            await _dialogService.ShowToast(message);
        }

        protected void TrackEvent(string eventName, Dictionary<string, string> additionalParams = null)
        {
            try
            {
                EnvironmentSetting selectedEndpoint = _localDbContext.GetSelectedEnvironment();

                var loggedData = new Dictionary<string, string>
                {
                    { "username", selectedEndpoint?.UserName },
                    { "appVersion", AppVersionNumber },
                    { "devicePlatform", DevicePlatform },
                };

                if (additionalParams != null)
                {
                    foreach (var item in additionalParams)
                    {
                        loggedData.Add(item.Key, item.Value);
                    }
                }

                _analyticsService.TrackEvent(eventName, loggedData);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception occured while logging the analytics {0}", ex);
            }
        }

        public void TrackView()
        {
            try
            {
                var viewType = this.GetType();
                var viewName = viewType.Name.Replace("ViewModel", string.Empty);

                var loggedData = new Dictionary<string, string>
                {
                    { "username", _settingsService.UshdId.ToString() },
                    { "appVersion", AppVersionNumber },
                    { "devicePlatform", DevicePlatform },
                };

                _analyticsService.TrackEvent(viewName, loggedData);
            }
            catch (Exception ex)
            {
                LogHelper.LogException("Error occured -TrackView", ex);
            }
        }

        public async Task OnLogout(bool withAlert)
        {
            if (withAlert)
            {
                bool confirmedLogout = await _dialogService.ShowConfirmation(
                       AppResources.AppName,
                       AppResources.ConfirmLogoutMessage,
                       AppResources.Yes,
                       AppResources.No);

                if (!confirmedLogout)
                {
                    return;
                }
            }

            IsBusy = true;

            long sessionId = _settingsService.SessionId;

            LogoutRequest logoutRequest = new LogoutRequest()
            {
                SessionId = sessionId,
            };

            LogoutResponse logoutResponse = await _userService.Logout(logoutRequest);

            IsBusy = false;

            await _navigationService.PerformLogout();
        }

        public List<EnvironmentSetting> GetUserEnvironments()
        {
            List<EnvironmentSetting> endPoints = _localDbContext.GetEntityList<EnvironmentSetting>();

            return endPoints;
        }

        public WorkloadSetting GetWorkloadSetting()
        {
            List<WorkloadSetting> workloadSettings = _localDbContext.GetEntityList<WorkloadSetting>();

            WorkloadSetting workloadSetting = workloadSettings.FirstOrDefault();

            if (workloadSetting == null)
            {
                workloadSetting = WorkloadSetting.GetDefault();

                _localDbContext.SaveEntity<WorkloadSetting>(workloadSetting);

                workloadSettings = _localDbContext.GetEntityList<WorkloadSetting>();

                workloadSetting = workloadSettings.First();
            }

            return workloadSetting;
        }

        public List<WorkloadListItem> GetWorkloadList(GetMunicipalityReportsResponse response, bool isGrouping = false)
        {
            List<WorkloadListItem> workloadList = new List<WorkloadListItem>();

            try
            {
                var displayInfo = DeviceDisplay.MainDisplayInfo;

                WorkloadSetting workloadSetting = GetWorkloadSetting();

                Random random = new Random();

                var followedItems = _localDbContext.GetEntityList<FollowedReport>();

                var savedItems = _localDbContext.GetEntityList<UserLocalReport>();

                int index = 0;

                double width = 40 * displayInfo.Density;

                double height = 40 * displayInfo.Density;

                if (response.StatetypeIcons != null)
                {
                    foreach (var stateTypeIcon in response.StatetypeIcons)
                    {
                        string resized = _platformManager.ResizeImage(stateTypeIcon.Icon, width, height);

                        if (!string.IsNullOrEmpty(resized))
                        {
                            stateTypeIcon.Icon = resized;
                        }
                    }
                }

                if (response.Reports != null)
                {
                    foreach (var dataItem in response.Reports)
                    {
                        StateTypeIcon stateTypeIcon = response.StatetypeIcons.FirstOrDefault(x => x.StateType == dataItem.Statetype);

                        var followedItem = followedItems?.FirstOrDefault(x => x.IthdId == dataItem.IthdId && x.CustId == _settingsService.MunicipalityId);

                        var savedItem = savedItems?.FirstOrDefault(x => x.IthdId == dataItem.IthdId && x.CustId == _settingsService.MunicipalityId);

                        WorkloadListItem workloadListItem = new WorkloadListItem()
                        {
                            Header1 = $"#{dataItem.IthdId}",
                            Header2 = $"{dataItem.Category}",
                            ReportItem = dataItem,
                            IthdId = dataItem.IthdId,
                            Date = dataItem.DateEntry,
                            MarkerIconSource = stateTypeIcon.Icon,
                            StateIconSource = stateTypeIcon,
                            CustId = _settingsService.MunicipalityId,
                            IsFollowed = savedItem != null || followedItem != null && followedItem.IsFollowed,
                        };

                        if (dataItem.Coordinates != null)
                        {
                            workloadListItem.Position = dataItem.Coordinates.ToGeographicLocation();
                        }

                        workloadListItem.Index = index++;
                        workloadList.Add(workloadListItem);
                    }
                }

                if (isGrouping)
                {
                    workloadList = GroupByDistance(workloadList);
                }

                workloadList = WorkloadListItem.Sort(workloadList, workloadSetting);
            }
            catch (Exception ex)
            {
                LogHelper.LogException("GetWorkloadList", ex);
            }

            return workloadList;
        }

        private List<WorkloadListItem> GroupByDistance(List<WorkloadListItem> workloadList)
        {
            List<WorkloadListItem> workloadListGrouped = new List<WorkloadListItem>();
            List<WorkloadListItem> nonClusteredItems = new List<WorkloadListItem>();

            int iterationCount = 0;
            int maxIterationCount = 8;

            do
            {
                nonClusteredItems = workloadList.FindAll(x => !x.IsClustered && x.Position != null).OrderBy(y => (y.PositionFactor)).ToList();

                foreach (var workloadListItem in nonClusteredItems)
                {
                    if (workloadListItem.IsClustered)
                        continue;

                    GeographicLocation position = workloadListItem.Position;

                    List<WorkloadListItem> siblings = nonClusteredItems.FindAll(
                        x => x != workloadListItem &&
                        !x.IsClustered &&
                        x.DistanceTo(workloadListItem, x) <= AppSettingConstants.FIELD_GROUPING_THRESHOLD);

                    if (siblings.Any())
                    {
                        workloadListItem.Siblings = siblings;

                        siblings.ForEach(x => x.IsClustered = true);
                        workloadListItem.IsClustered = true;

                        workloadListGrouped.Add(workloadListItem);

                        workloadListItem.MarkerIconSource = "meerInBehandelingPin.png";
                    }
                }

                iterationCount++;

                if (iterationCount > maxIterationCount)
                {
                    var recidualItems = workloadList.FindAll(x => !x.IsClustered);

                    foreach (var recidualItem in recidualItems)
                    {
                        recidualItem.SetMarkerIcon(false);
                    }

                    workloadListGrouped.AddRange(recidualItems);

                    break;
                }

            }
            while (nonClusteredItems.Count > 0);

            int total = 0;

            foreach (var workloadListItem in workloadListGrouped)
            {
                total = workloadListItem.Siblings.Count();
            }

            total = workloadListGrouped.Count();

            return workloadListGrouped;
        }

        private Coordinates InjectRandomCoards()
        {
            Random random = new Random();

            Coordinates coordinates = new Coordinates();

            var next = random.NextDouble();

            double minLatValue = 6.927079;
            double maxLatValue = 8.927079;

            double minLonValue = 79.861244;
            double maxLonValue = 81.861244;

            coordinates.Lat = minLatValue + (next * (maxLatValue - minLatValue));
            coordinates.Lon = minLonValue + (next * (maxLonValue - minLonValue));

            return coordinates;
        }

        public T GetAppResource<T>(string resourceName)
        {
            var resources = Application.Current.Resources;

            T resource = (T)resources[resourceName];

            return resource;
        }

        public void SetBaseApiUrl(string baseApiUrl)
        {
            _settingsService.BaseGatewayEndpoint = baseApiUrl;
        }

        public async Task EditCurrentEnvironment(Type fromScreen)
        {
            //TODO
            await Task.FromResult(true);
        }

        protected void ClearAddReportRequest()
        {
            AddReportRequest = null;
        }

        protected void UnEditAddReportRequest()
        {
            if (AddReportRequest != null)
            {
                AddReportRequest.IsReportEditMode = false;
            }
        }

        public async Task ShowPendingPushNotification()
        {
            if (GlobalSetting.Instance.PushNotificationData != null)
            {
                var data = GlobalSetting.Instance.PushNotificationData;

                if (!data.IsProcessed)
                {
                    await ShowToast(AppResources.PendingPushNotificationMessage);
                }
            }
        }

        public async Task OpenAndNavigateToReportNotification(PushNotificationData data)
        {
            if (data.IsProcessing)
            {
                return;
            }
            else
            {
                data.IsProcessing = true;
            }

            if (data.IsProcessed)
            {
                return;
            }
            else
            {
                data.IsProcessed = true;
            }

            //TODO navigate
        }

        public async Task SelectMunicipality(long municipalityId, string municipalityName)
        {
            _settingsService.MunicipalityId = municipalityId;
            _settingsService.MunicipalityName = municipalityName;

            await _navigationService.ClearBackStack();

            await Task.Delay(500);

            await _navigationService.NavigateToAsync<MainViewModel>();
        }

        protected async Task LoadMunicipalitAppConfigData(long custId)
        {
            if (!_connectionService.IsConnected)
            {
                await _dialogService.ShowDialog(
                     AppResources.ConnectivityErrorMsg,
                     AppResources.ConnectivityError,
                     AppResources.OK);

                return;
            }

            IsBusy = true;

            GetMgAppConfigRequest configRequest = new GetMgAppConfigRequest()
            {
                EndPoint = $"MunicipalityConfig/{custId}",
                DeviceToken = _settingsService.PushNotificationToken,
                AppVersion = AppVersionNumber,
                DevicePlatform = DevicePlatform,
                Language = "ENGLISH",
                Application = "XamarinEnterpriseApp.App",
                IpAddress = _connectionService.DeviceIP
            };

            MGAppConfigDataResponse configResponse = await _applicationDataService.GetRequest<MGAppConfigDataResponse, GetMgAppConfigRequest>(configRequest);

            if (configResponse != null && configResponse.IsSuccessful)
            {
                GlobalSetting.Instance.MGAppConfig = configResponse;

                // Adding MDAppConfigurations into setting service

            }
            else
            {
                // Set default file count to 4 incase if configs are missing
                _settingsService.MaxNumberOfFiles = 4;

                //await InvokeErrorMessage(response, AppResources.AppName, AppResources.FetchDataErrorMsg);
            }

            IsBusy = false;
        }

        public async Task AttemptUpdateDeviceToken()
        {
            try
            {
                if (!string.IsNullOrEmpty(_settingsService.NewPushNotificationToken))
                {
                    if (_connectionService.IsConnected)
                    {
                        UpdateDeviceTokenRequest request = new UpdateDeviceTokenRequest()
                        {
                            EndPoint = $"callPaul/",
                            Language = "ENGLISH",
                            Application = "XamarinEnterpriseApp.App",
                            IpAddress = _connectionService.DeviceIP,
                            DeviceId = settingsService.UniqueDeviceId,
                            DeviceToken = settingsService.NewPushNotificationToken,
                            PushToken = settingsService.NewPushNotificationToken,
                        };

                        var response = await _applicationDataService.PostRequest<UpdateDeviceTokenResponse, UpdateDeviceTokenRequest>(request);

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
            }
            catch (Exception ex)
            {
                LogHelper.LogException("SendRegTokenToNotificationHub- Error", ex);
            }
        }

        public async Task<bool> NotifyPushNotification()
        {
            bool isNotified = false;

            var data = GlobalSetting.Instance.PushNotificationData;

            if (data != null)
            {
                await OpenAndNavigateToReportNotification(data);
            }

            return isNotified;
        }

        public async Task SetTokenToClipBoard()
        {
            try
            {
                await Clipboard.SetTextAsync(_settingsService.PushNotificationToken);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception occured while logging token {0}", ex);
            }
        }

        public async Task RequestPermisisons(PP.Abstractions.Permission permission)
        {
            try
            {
                PP.Abstractions.PermissionStatus status = await PermissionHelper.RequestPermission<PP.LocationPermission>(permission);
            }
            catch (Exception ex)
            {
                LogHelper.LogException("Exception occured while requestiong the permissions", ex);
            }
        }
    }
}
