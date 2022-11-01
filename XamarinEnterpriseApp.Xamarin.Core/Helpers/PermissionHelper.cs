using System;
using System.Threading.Tasks;
using XamarinEnterpriseApp.Xamarin.Core.Ioc;
using XamarinEnterpriseApp.Xamarin.Core.Localization;
using XamarinEnterpriseApp.Xamarin.Core.Services;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;

namespace XamarinEnterpriseApp.Xamarin.Core.Helpers
{
    public static class PermissionHelper
    {
        public async static Task<PermissionStatus> RequestPermission<T>(Permission permission) where T : BasePermission, new()
        {
            PermissionStatus status = await CrossPermissions.Current.CheckPermissionStatusAsync<T>();

            if (status != PermissionStatus.Granted)
            {
                if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(permission))
                {
                    IDialogService dialogService = DependencyResolver.Resolve<IDialogService>();
                    await dialogService.ShowDialog(
                        AppResources.PermissionRequired,
                        AppResources.AppName,
                        AppResources.OK);
                }

                var results = await CrossPermissions.Current.RequestPermissionAsync<T>();
            }

            return status;
        }
    }
}
