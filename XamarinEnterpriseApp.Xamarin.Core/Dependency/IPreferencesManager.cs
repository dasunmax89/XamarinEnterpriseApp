using System;
using System.Threading.Tasks;

namespace XamarinEnterpriseApp.Xamarin.Core
{
    public interface IPreferencesManager
    {
        Task PlatformSet<T>(string key, T value);
        T PlatformGet<T>(string key, T defaultValue);
    }
}
