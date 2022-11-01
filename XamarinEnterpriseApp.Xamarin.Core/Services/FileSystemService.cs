using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using XamarinEnterpriseApp.Xamarin.Core.Dependency;
using XamarinEnterpriseApp.Xamarin.Core.Helpers;
using XamarinEnterpriseApp.Xamarin.Core.Localization;
using XamarinEnterpriseApp.Xamarin.Core.Models;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Xamarin.Essentials;
using Xamarin.Forms;
using static Xamarin.Essentials.Permissions;

namespace XamarinEnterpriseApp.Xamarin.Core.Services
{
    public interface IFileSystemService
    {
        Task<List<FileResult>> PickPhotoAsync();

        Task<FileResult> TakePhotoAsync();

        ImageSource GetImageSource(FileResult mediaFile);

        Task<string> GetBase64ImageSource(FileResult mediaFile, bool toRotate);

        Task<List<MediaFile>> PickMultiPhotoAsync(int maxImagesCount);

    }

    public class FileSystemService : IFileSystemService
    {
        private readonly IDialogService _dialogService;

        public FileSystemService(IDialogService dialogService, ISettingsService settingsService)
        {
            _dialogService = dialogService;
        }

        public ImageSource GetImageSource(FileResult mediaFile)
        {
            var imageSource = ImageSource.FromStream(() =>
            {
                var stream = mediaFile.OpenReadAsync();
                return stream.Result;
            });

            return imageSource;
        }

        public async Task<string> GetBase64ImageSource(FileResult mediaFile, bool toRotate)
        {
            string base64String = String.Empty;

            var stream = await mediaFile.OpenReadAsync();

            var bytes = new byte[stream.Length];

            await stream.ReadAsync(bytes, 0, (int)stream.Length);

            var platformManager = DependencyService.Get<IPlatformManager>();

            int quality = 75;

            byte[] resizedImage = await platformManager.ResizeImage(bytes, quality, toRotate);

            if (resizedImage != null)
            {
                base64String = Convert.ToBase64String(resizedImage);
            }

            return base64String;
        }

        public async Task<List<FileResult>> PickPhotoAsync()
        {
            List<FileResult> files = new List<FileResult>();

            try
            {
                await CrossMedia.Current.Initialize();

                if (!CrossMedia.Current.IsPickPhotoSupported)
                {
                    await _dialogService.ShowToast(AppResources.PickPhotoError);
                }
                else
                {
                    var file = await MediaPicker.PickPhotoAsync(new MediaPickerOptions()
                    {
                        Title = AppResources.MakePhoto
                    });

                    files.Add(file);

                }
            }
            catch (Exception ex)
            {
                LogHelper.LogException($"Exception occured PickPhotoAsync {0}", ex);
            }

            return files;
        }

        public async Task<List<MediaFile>> PickMultiPhotoAsync(int maxImagesCount)
        {
            List<MediaFile> files = null;

            try
            {
                await CheckPermissions();

                await CrossMedia.Current.Initialize();

                if (!CrossMedia.Current.IsPickPhotoSupported)
                {
                    await _dialogService.ShowToast(AppResources.PickPhotoError);
                }
                else
                {
                    files = await CrossMedia.Current.PickPhotosAsync(new PickMediaOptions
                    {
                        PhotoSize = PhotoSize.Medium,
                        RotateImage = true,
                        CompressionQuality = 80,
                        SaveMetaData = false,
                    },
                    new MultiPickerOptions()
                    {
                        AlbumSelectTitle = AppResources.ChooseFromPhotos,
                        BackButtonTitle = AppResources.Cancel,
                        DoneButtonTitle = AppResources.Done,
                        PhotoSelectTitle = AppResources.ChooseFromPhotos,
                        LoadingTitle = AppResources.WaitMsg,
                        MaximumImagesCount = maxImagesCount,
                        BarStyle = MultiPickerBarStyle.BlackTranslucent,
                    });
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogException($"Exception occured PickMultiPhotoAsync {0}", ex);
            }

            return files;
        }

        public async Task<FileResult> TakePhotoAsync()
        {
            FileResult file = null;

            try
            {
                await CrossMedia.Current.Initialize();

                if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
                {
                    await _dialogService.ShowToast(AppResources.PickPhotoError);
                }
                else
                {
                    file = await MediaPicker.CapturePhotoAsync(new MediaPickerOptions()
                    {
                        Title = AppResources.MakePhoto
                    });
                }
            }
            catch (Exception ex)
            {
                await _dialogService.ShowToast(AppResources.PickPhotoNotSupported);

                LogHelper.LogException($"Exception occured TakePhotoAsync {0}", ex);
            }

            return file;
        }

        private async Task CheckPermissions()
        {
            try
            {
                var cameraStatus = await CheckPermissionInternal<Permissions.Camera>();

                var photoStatus = await CheckPermissionInternal<Permissions.Photos>();

                var storageReadStatus = await CheckPermissionInternal<Permissions.StorageRead>();

                var storageWriteStatus = await CheckPermissionInternal<Permissions.StorageWrite>();

                if (DeviceInfo.Platform == DevicePlatform.iOS &&
                    (cameraStatus == PermissionStatus.Denied ||
                    photoStatus == PermissionStatus.Denied ||
                    storageReadStatus == PermissionStatus.Denied ||
                    storageWriteStatus == PermissionStatus.Denied))
                {
                    // On iOS once a permission has been denied it may not be requested again from the application
                    await _dialogService.ShowDialog(AppResources.TurnOnPermissionInSettings, AppResources.Caution, AppResources.OK);
                }

                if (DeviceInfo.Platform == DevicePlatform.Android &&
                   (cameraStatus != PermissionStatus.Granted ||
                   photoStatus != PermissionStatus.Granted ||
                   storageReadStatus != PermissionStatus.Granted ||
                   storageWriteStatus != PermissionStatus.Granted))
                {
                    //instead of showing 3 alerts
                    await _dialogService.ShowDialog(AppResources.TurnOnPermissionInSettingsDroid, AppResources.Caution, AppResources.OK);
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogException($"Exception occured CheckPermissions {0}", ex);

                await _dialogService.ShowToast(ex.Message);
            }
        }

        private async Task<PermissionStatus> CheckPermissionInternal<TPermission>() where TPermission : BasePermission, new()
        {
            var permissionStatus = await Permissions.CheckStatusAsync<TPermission>();

            if (permissionStatus != PermissionStatus.Granted)
            {
                if (permissionStatus == PermissionStatus.Denied && DeviceInfo.Platform == DevicePlatform.iOS)
                {
                    return permissionStatus;
                }

                permissionStatus = await Permissions.RequestAsync<TPermission>();
            }

            return permissionStatus;
        }
    }
}
