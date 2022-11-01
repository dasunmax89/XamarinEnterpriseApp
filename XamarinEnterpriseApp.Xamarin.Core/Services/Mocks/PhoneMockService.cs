using System;
using System.Threading.Tasks;

namespace XamarinEnterpriseApp.Xamarin.Core.Services.Mocks
{
    public class PhoneMockService : IPhoneService
    {
        public PhoneMockService()
        {
        }

        public async Task MakePhoneCall(string phoneNumber)
        {
            await Task.FromResult(true);
        }
    }
}
