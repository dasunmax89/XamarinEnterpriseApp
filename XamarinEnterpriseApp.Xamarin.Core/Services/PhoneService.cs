using System;
using System.Threading.Tasks;
using XamarinEnterpriseApp.Xamarin.Core.Helpers;
using XamarinEnterpriseApp.Xamarin.Core.Ioc;
using XamarinEnterpriseApp.Xamarin.Core.Localization;
using Plugin.Messaging;
using Xamarin.Essentials;

namespace XamarinEnterpriseApp.Xamarin.Core.Services
{
    public interface IPhoneService
    {
        Task MakePhoneCall(string phoneNumber);
    }

    public class PhoneService : BaseService, IPhoneService
    {
        public async Task MakePhoneCall(string phoneNumber)
        {
            if (!string.IsNullOrEmpty(phoneNumber))
            {
                try
                {
                    PhoneDialer.Open(phoneNumber);
                }
                catch (Exception ex)
                {
                    IDialogService dialogService = DependencyResolver.Resolve<IDialogService>();

                    await dialogService.ShowToast(AppResources.CouldntMakePhoneCall);

                    LogHelper.LogException("Exception occurred in MakePhoneCall", ex);
                }
            }
        }
    }
}
