using System.Threading.Tasks;
using XamarinEnterpriseApp.Xamarin.Core.Dependency;
using XamarinEnterpriseApp.Xamarin.Core.Localization;
using XamarinEnterpriseApp.Xamarin.Core.Models;
using XamarinEnterpriseApp.Xamarin.Core.Repositories;
using XamarinEnterpriseApp.Xamarin.Core.Services;
using XamarinEnterpriseApp.Xamarin.Core.ViewModels.Base;
using Xamarin.Essentials;

namespace XamarinEnterpriseApp.Xamarin.Core.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        #region Actions

        #endregion

        #region Bindables

        #endregion

        #region Commands

        #endregion

        public MainViewModel(IConnectionService connectionService, INavigationService navigationService,
                                  IDialogService dialogService, ISettingsService settingsService,
                                  IAnalyticsService analyticsService, IApplicationDataService applicationDataService,
                                  IUserService userService, ILocalDbContextService localDbContextService)
          : base(connectionService, navigationService, dialogService, settingsService, analyticsService, applicationDataService, userService, localDbContextService)
        {

        }

        public override async Task InitializeAsync(object navigationData)
        {
            IsBusy = true;

            await SetTokenToClipBoard();

            IsBusy = false;
        }

        public async override Task OnAppearing()
        {
            await Task.FromResult(true);
        }

        public async override Task OnDisappearing()
        {
            await Task.FromResult(true);
        }
    }
}
