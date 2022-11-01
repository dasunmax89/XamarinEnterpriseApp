using System;
using System.Threading.Tasks;
using XamarinEnterpriseApp.Xamarin.Core.Enums;
using XamarinEnterpriseApp.Xamarin.Core.Models;
using XamarinEnterpriseApp.Xamarin.Core.ViewModels;
using XamarinEnterpriseApp.Xamarin.Core.ViewModels.Base;

namespace XamarinEnterpriseApp.Xamarin.Core.Services.Mocks
{
    public class NavigationMockService : INavigationService
    {
        public NavigationMockService()
        {
        }

        public int NavigationCount => 0;

        bool INavigationService.IsBackButtonVisible => throw new NotImplementedException();

        public Task ClearBackStack()
        {
            return Task.CompletedTask;
        }

        public Task InitializeAsync()
        {
            return Task.CompletedTask;
        }

        public Task NavigateToAsync<TViewModel>(object parameter = null) where TViewModel : ViewModelBase
        {
            return Task.CompletedTask;
        }

        public Task NavigateToAsync(Type type, object parameter = null)
        {
            return Task.CompletedTask;
        }

        public Task NavigateToBack(bool isRoot = false)
        {
            return Task.CompletedTask;
        }

        public Task PerformLogout()
        {
            return Task.CompletedTask;
        }

        public Task PerformViewReportsList()
        {
            return Task.CompletedTask;
        }

        public Task PerformGoBack()
        {
            return Task.CompletedTask;
        }

        public Task ResumeAsync()
        {
            return Task.CompletedTask;
        }

        public ViewModelBase GetCurrentViewModel()
        {
            return null;
        }

        public Task NotifyPushNotification(PushNotificationData pushNotification)
        {
            return Task.CompletedTask;
        }

        public Task NavigateToMenu(PageType pageType)
        {
            return Task.CompletedTask;
        }

        public MenuViewModel GetMenuViewModel()
        {
            return null;
        }

        public void ToggleMenu()
        {

        }

        public void UpdateMenuMessageCount()
        {

        }

        public void NotifyAppConfigChange(object appConfig)
        {

        }
    }
}
