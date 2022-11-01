using System;
using System.Globalization;
using System.Reflection;
using System.Threading.Tasks;
using XamarinEnterpriseApp.Xamarin.Core.ViewModels;
using XamarinEnterpriseApp.Xamarin.Core.ViewModels.Base;
using XamarinEnterpriseApp.Xamarin.Core.Views;
using Xamarin.Forms;
using XamarinEnterpriseApp.Xamarin.Core.Ioc;
using XamarinEnterpriseApp.Xamarin.Core.Helpers;
using XamarinEnterpriseApp.Xamarin.Core.Dependency;
using XamarinEnterpriseApp.Xamarin.Core.Controls;
using XamarinEnterpriseApp.Xamarin.Core.Enums;
using XamarinEnterpriseApp.Xamarin.Core.Models;

namespace XamarinEnterpriseApp.Xamarin.Core.Services
{
    public interface INavigationService
    {
        int NavigationCount { get; }

        bool IsBackButtonVisible { get; }

        Task InitializeAsync();

        Task ResumeAsync();

        Task NavigateToAsync<TViewModel>(object parameter = null) where TViewModel : ViewModelBase;

        Task NavigateToAsync(Type type, object parameter = null);

        Task ClearBackStack();

        Task PerformLogout();

        Task NavigateToBack(bool isRoot = false);

        Task NotifyPushNotification(PushNotificationData pushNotification);

        Task NavigateToMenu(PageType pageType);

        MenuViewModel GetMenuViewModel();

        ViewModelBase GetCurrentViewModel();

        void ToggleMenu();

        void UpdateMenuMessageCount();

        void NotifyAppConfigChange(object appConfig);
    }

    public class NavigationService : BaseService, INavigationService
    {
        protected Application CurrentApplication => Application.Current;

        public bool IsBackButtonVisible
        {
            get
            {
                int navigationCount = 0;

                try
                {
                    var navigationPage = GetNavigationPage();

                    if (navigationPage != null)
                    {
                        navigationCount = navigationPage.Navigation.NavigationStack.Count;
                    }
                }
                catch (Exception ex)
                {
                    LogHelper.LogException("Exception occured - IsBackButtonVisible", ex);
                }

                return navigationCount >= 1;
            }
        }

        public int NavigationCount
        {
            get
            {
                int navigationCount = 0;

                var navigationPage = GetNavigationPage();

                if (navigationPage != null)
                {
                    navigationCount = navigationPage.Navigation.NavigationStack.Count;
                }

                return navigationCount;
            }
        }

        public NavigationService()
        {

        }

        public Task InitializeAsync()
        {
            ISettingsService settingsService = DependencyResolver.Resolve<ISettingsService>();

            if (!settingsService.HasIntroShown)
            {
                return NavigateToAsync<InitialViewModel>();
            }
            else
            {
                return NavigateToAsync<MainViewModel>();
            }

        }

        public async Task ResumeAsync()
        {
            if (!GlobalSetting.Instance.IsPickIntentOn)
            {
                var childViewModel = GetCurrentViewModel();

                if (childViewModel != null)
                {
                    await childViewModel.OnAppearing();
                }
            }

            MaintainBadgeCount();
        }

        private void CheckForPushEnabled()
        {
            var platformManager = DependencyService.Get<IPlatformManager>();

            platformManager.CheckPushEnabled(true);
        }

        private void MaintainBadgeCount()
        {
            var platformManager = DependencyService.Get<IPlatformManager>();

            platformManager.MaintainBadgeCount();
        }

        public Task NavigateToAsync(Type type, object parameter = null)
        {
            return InternalNavigateToAsync(type, parameter);
        }

        public Task NavigateToAsync<TViewModel>(object parameter = null) where TViewModel : ViewModelBase
        {
            return InternalNavigateToAsync(typeof(TViewModel), parameter);
        }

        private async Task InternalNavigateToAsync(Type viewModelType, object parameter)
        {
            GlobalSetting.Instance.NavigationParam = parameter;

            Page page = CreatePage(viewModelType, parameter);

            if (page is InitialView)
            {
                CurrentApplication.MainPage = page;
            }
            else if (page is MainView)
            {
                CurrentApplication.MainPage = page;
            }
            else
            {
                if (CurrentApplication.MainPage is MainView)
                {
                    var mainPage = CurrentApplication.MainPage as MainView;

                    if (page is HomeView)
                    {
                        var navigationPage = new NavigationPage(page);

                        mainPage.Detail = navigationPage;
                    }

                    mainPage.IsPresented = false;
                }
                else
                {
                    await AddToNavigationPage(page);
                }
            }

            await (page.BindingContext as ViewModelBase).InitializeAsync(parameter);

        }

        private async Task AddToNavigationPage(Page page)
        {
            var navigationPage = GetNavigationPage();

            if (navigationPage != null)
            {
                await navigationPage.PushAsync(page);
            }
            else
            {
                CurrentApplication.MainPage = page;
            }
        }

        private Page CreatePage(Type viewModelType, object parameter)
        {
            Type pageType = GetPageTypeForViewModel(viewModelType);
            if (pageType == null)
            {
                throw new Exception($"Cannot locate page type for {viewModelType}");
            }

            Page page = Activator.CreateInstance(pageType) as Page;

            return page;
        }

        private Type GetPageTypeForViewModel(Type viewModelType)
        {
            var viewName = viewModelType.FullName.Replace("Model", string.Empty);
            var viewModelAssemblyName = viewModelType.GetTypeInfo().Assembly.FullName;
            var viewAssemblyName = string.Format(CultureInfo.InvariantCulture, "{0}, {1}", viewName, viewModelAssemblyName);

            var viewType = Type.GetType(viewAssemblyName);
            return viewType;
        }

        public async Task ClearBackStack()
        {
            try
            {
                var navigationPage = GetNavigationPage();

                await navigationPage.PopToRootAsync();
            }
            catch (Exception ex)
            {
                LogHelper.LogException($"Exception occured - ClearBackStack {0}", ex);
            }
        }

        public Task PerformLogout()
        {
            GlobalSetting.Instance.LoggedOut();

            CacheHelper.Reset();

            //TODOawait NavigateToAsync<LoginViewModel>();

            return Task.CompletedTask;
        }

        public async Task NavigateToBack(bool isRoot = false)
        {
            try
            {
                var navigationPage = GetNavigationPage();

                if (navigationPage != null)
                {
                    if (NavigationCount > 1)
                    {
                        if (isRoot)
                        {
                            await navigationPage.PopToRootAsync();
                        }
                        else
                        {
                            await navigationPage.PopAsync();
                        }
                    }
                    else
                    {
                        ToggleMenu();
                    }
                }
                else
                {
                    ToggleMenu();
                }

            }
            catch (Exception ex)
            {
                LogHelper.LogException($"Exception occured - NavigateToBack {0}", ex);
            }
        }

        private NavigationPage GetNavigationPage()
        {
            NavigationPage navigationPage = null;

            if (CurrentApplication.MainPage is NavigationPage)
            {
                navigationPage = CurrentApplication.MainPage as NavigationPage;
            }
            else if (CurrentApplication.MainPage is MainView)
            {
                var mainPage = CurrentApplication.MainPage as MainView;

                navigationPage = mainPage.Detail as NavigationPage;
            }

            return navigationPage;
        }

        public Task NotifyPushNotification(PushNotificationData pushNotification)
        {
            return Task.FromResult(true);
        }

        public async Task NavigateToMenu(PageType pageType)
        {
            MenuViewModel vm = GetMenuViewModel();

            if (vm != null)
            {
                await vm.OnNavigateRequested(pageType);
            }
        }

        public MenuViewModel GetMenuViewModel()
        {
            MenuViewModel vm = null;

            var mainPage = CurrentApplication.MainPage as MainView;

            if (mainPage != null)
            {
                MenuView menuView = mainPage.Flyout as MenuView;

                vm = menuView?.BindingContext as MenuViewModel;
            }

            return vm;
        }


        public ViewModelBase GetCurrentViewModel()
        {
            ViewModelBase vm = null;

            var currentPage = CurrentApplication.MainPage;

            var mainPage = currentPage as MainView;

            if (mainPage != null)
            {
                ContentPage view = mainPage.Detail as ContentPage;

                vm = view?.BindingContext as ViewModelBase;
            }
            else
            {
                vm = currentPage?.BindingContext as ViewModelBase;
            }

            return vm;
        }

        public void ToggleMenu()
        {
            var mainPage = CurrentApplication.MainPage as MainView;

            if (mainPage != null)
            {
                mainPage.IsPresented = !mainPage.IsPresented;
            }
        }

        public void UpdateMenuMessageCount()
        {
            MenuViewModel vm = GetMenuViewModel();

            if (vm != null)
            {
                vm.UpdateMenuMessageCount();
            }
        }

        public void NotifyAppConfigChange(object appConfig)
        {
            MenuViewModel vm = GetMenuViewModel();

            if (vm != null)
            {
                vm.OnAppConfigurationChanged(appConfig);
            }
        }
    }
}
