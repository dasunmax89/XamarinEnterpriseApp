using System;
using System.IO;
using XamarinEnterpriseApp.Xamarin.Core.Helpers;
using XamarinEnterpriseApp.Xamarin.Droid.Helpers;
using Xamarin.Forms;

[assembly: Dependency(typeof(FileHelper))]
namespace XamarinEnterpriseApp.Xamarin.Droid.Helpers
{
    public class FileHelper : IFileHelper
    {
        public string GetLocalFilePath(string filename)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            return Path.Combine(path, filename);
        }

        public FileHelper()
        {

        }
    }
}
