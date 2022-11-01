using System;
using XamarinEnterpriseApp.Xamarin.Core.Helpers;

namespace XamarinEnterpriseApp.Xamarin.Core.Services.Mocks
{
    public class FileHelperMock : IFileHelper
    {
        public FileHelperMock()
        {
        }

        public string GetLocalFilePath(string filename)
        {
            return string.Empty;
        }
    }
}
