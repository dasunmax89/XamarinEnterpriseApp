using System;
using Foundation;
using XamarinEnterpriseApp.Xamarin.Core.Dependency;
using XamarinEnterpriseApp.Xamarin.iOS.Dependency;
using Xamarin.Forms;

[assembly: Dependency(typeof(VersionManager))]
namespace XamarinEnterpriseApp.Xamarin.iOS.Dependency
{
    public class VersionManager: IVersionManager
    {
        public VersionManager()
        {
        }

        public string BuildNumber()
        {
            return NSBundle.MainBundle.ObjectForInfoDictionary("CFBundleVersion").ToString();
        }

        public string VersionNumber()
        {
            return NSBundle.MainBundle.ObjectForInfoDictionary("CFBundleShortVersionString").ToString();
        }
    }
}
