using Android.Content.PM;
using XamarinEnterpriseApp.Xamarin.Core.Dependency;
using XamarinEnterpriseApp.Xamarin.Droid.Dependency;
using Xamarin.Forms;

[assembly: Dependency(typeof(VersionManager))]
namespace XamarinEnterpriseApp.Xamarin.Droid.Dependency
{
    public class VersionManager : IVersionManager
    {
        PackageInfo _appInfo;

        public VersionManager()
        {
            var context = Android.App.Application.Context;
            _appInfo = context.PackageManager.GetPackageInfo(context.PackageName, 0);
        }

        public string BuildNumber()
        {
            return _appInfo.LongVersionCode.ToString();
        }

        public string VersionNumber()
        {
            return _appInfo.VersionName;
        }
    }
}
