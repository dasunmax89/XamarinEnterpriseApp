using System;
namespace XamarinEnterpriseApp.Xamarin.Core.Dependency
{
    public interface IVersionManager
    {
        String VersionNumber();
        String BuildNumber();
    }
}
