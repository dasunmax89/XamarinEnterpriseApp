
using System;
using System.Threading.Tasks;
using XamarinEnterpriseApp.Xamarin.Core.Models;

namespace XamarinEnterpriseApp.Xamarin.Core.Dependency
{
    public interface ILocationTracker
    {
        bool CheckGPSEnabled();

        void EnableGPSSetting();

        Task<GeographicLocation> GetLocationAsync();
    }
}
