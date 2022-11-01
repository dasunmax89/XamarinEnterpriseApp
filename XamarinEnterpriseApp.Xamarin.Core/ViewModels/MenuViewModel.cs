using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using XamarinEnterpriseApp.Xamarin.Core.Constants;
using XamarinEnterpriseApp.Xamarin.Core.Dependency;
using XamarinEnterpriseApp.Xamarin.Core.Enums;
using XamarinEnterpriseApp.Xamarin.Core.Ioc;
using XamarinEnterpriseApp.Xamarin.Core.Localization;
using XamarinEnterpriseApp.Xamarin.Core.Models;
using XamarinEnterpriseApp.Xamarin.Core.Models.Base;
using XamarinEnterpriseApp.Xamarin.Core.Repositories;
using XamarinEnterpriseApp.Xamarin.Core.Services;
using XamarinEnterpriseApp.Xamarin.Core.ViewModels.Base;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace XamarinEnterpriseApp.Xamarin.Core.ViewModels
{
    public class MenuViewModel : ViewModelBase
    {
        private readonly IPhoneService _phoneService;

        #region Actions

        #endregion

        #region Bindables

        private ObservableCollection<HomeMenuItem> _menuItems;
        public ObservableCollection<HomeMenuItem> MenuItems
        {
            get => _menuItems;
            set
            {
                _menuItems = value;
                RaisePropertyChanged(() => MenuItems);
            }
        }

        #endregion

        #region Commands

        public ICommand MenuItemTappedCommand => new Command(OnMenuItemTapped);

        public ICommand SocialButtonTappedCommand => new Command(OnSocialButtonTapped);

        #endregion

        public MenuViewModel(IConnectionService connectionService, INavigationService navigationService,
                                  IDialogService dialogService, ISettingsService settingsService, IPhoneService phoneService,
                                  IAnalyticsService analyticsService, IApplicationDataService applicationDataService,
                                  IUserService userService, ILocalDbContextService localDbContextService)
          : base(connectionService, navigationService, dialogService, settingsService, analyticsService, applicationDataService, userService, localDbContextService)
        {
            _phoneService = phoneService;

            MenuItems = CreateMenuItems();
        }

        private ObservableCollection<HomeMenuItem> CreateMenuItems()
        {
            var resources = Application.Current.Resources;

            var sectionSeparatorStyle = (Style)resources["MenuSectionSeparatorStyle"];
            var menuSeparatorStyle = (Style)resources["MenuItemSeparatorStyle"];

            var menuItems = new ObservableCollection<HomeMenuItem>();

            menuItems.Add(HomeMenuItem.Create(PageType.Home));
            menuItems.Add(HomeMenuItem.Create(PageType.Contact));
            menuItems.Add(HomeMenuItem.Create(PageType.About));

            return menuItems;
        }

        public override async Task InitializeAsync(object navigationData)
        {
            IsBusy = true;

            await Task.FromResult(true);

            IsBusy = false;
        }

        private async void OnMenuItemTapped(object menuItemTappedEventArgs)
        {
            ISettingsService settingsService = DependencyResolver.Resolve<ISettingsService>();

            var menuItem = ((menuItemTappedEventArgs as ItemTappedEventArgs)?.Item as HomeMenuItem);

            await NavigateToMenuItem(menuItem);
        }

        private async Task NavigateToMenuItem(HomeMenuItem menuItem)
        {
            ISettingsService settingsService = DependencyResolver.Resolve<ISettingsService>();

            if (menuItem != null)
            {
                var type = menuItem?.ViewModel;

                object parameter = null;

                await _navigationService.NavigateToAsync(type, parameter);
            }
        }

        public void OnAppConfigurationChanged(object appConfiguration)
        {
            var resources = Application.Current.Resources;

            var sectionSeparatorStyle = (Style)resources["MenuSectionSeparatorStyle"];
            var menuSeparatorStyle = (Style)resources["MenuItemSeparatorStyle"];

            RaisePropertyChanged(() => MenuItems);
        }

        public void OnLoggedOut()
        {
            MenuItems = CreateMenuItems();

            RaisePropertyChanged(() => MenuItems);
        }

        public override void SubscribeToEvents(object viewArgs = null)
        {

        }

        public override void UnSubscribeToEvents()
        {

        }


        public async Task OnNavigateRequested(PageType pageType)
        {
            HomeMenuItem menuItem = MenuItems.FirstOrDefault(x => x.Id == (int)pageType);

            await NavigateToMenuItem(menuItem);
        }

        public async override Task OnAppearing()
        {
            await Task.FromResult(true);
        }

        public async override Task OnDisappearing()
        {
            await Task.FromResult(true);
        }

        private async void OnSocialButtonTapped(object obj)
        {
            string id = obj as string;

            string url = string.Empty;

            switch (id)
            {
                case "1":
                    url = "https://www.facebook.com/reppido";
                    break;
                case "2":
                    url = "https://www.linkedin.com/company/reppido/";
                    break;
                case "3":
                    url = "https://twitter.com/reppidoNL";
                    break;
            }

            await Launcher.OpenAsync(url);
        }

        internal void UpdateMenuMessageCount()
        {
            throw new NotImplementedException();
        }
    }
}
