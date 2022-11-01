using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using XamarinEnterpriseApp.Xamarin.Core.Models;
using Plugin.Media.Abstractions;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace XamarinEnterpriseApp.Xamarin.Core.Services.Mocks
{
    public class FileSystemMockService : IFileSystemService
    {
        public Task<string> GetBase64ImageSource(FileResult mediaFile, bool toRotate)
        {
            return null;
        }

        public ImageSource GetImageSource(FileResult mediaFile)
        {
            return null;
        }

        public async Task OpenFile(string path)
        {
            await Task.FromResult(string.Empty);
        }

        public Task<List<MediaFile>> PickMultiPhotoAsync(int maxImagesCount)
        {
            return null;
        }

        public Task<List<FileResult>> PickPhotoAsync()
        {
            return null;
        }

        public async Task<string> SaveFile(ReportFile file)
        {
            return await Task.FromResult(string.Empty);
        }

        public Task<FileResult> TakePhotoAsync()
        {
            return null;
        }
    }
}
