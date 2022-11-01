using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Acr.UserDialogs;
using XamarinEnterpriseApp.Xamarin.Core.Helpers;
using XamarinEnterpriseApp.Xamarin.Core.Localization;
using XamarinEnterpriseApp.Xamarin.Core.Models;

namespace XamarinEnterpriseApp.Xamarin.Core.Services
{
    public interface IDialogService
    {
        Task ShowDialog(string message, string title, string buttonLabel);

        Task<bool> ShowConfirmation(string title, string message, string okLabel, string cancelLabel);

        Task<PromptResult> ShowPromptAsyncDialog(InputType inputType, string title, string message);

        Task ShowToast(string message, int duration = 3);

        List<ListItemModel> GetCameraActionsheetItems();
    }

    public class DialogService : BaseService, IDialogService
    {
        public Task<bool> ShowConfirmation(string title, string message, string okLabel, string cancelLabel)
        {
            ConfirmConfig config = new ConfirmConfig()
            {
                Title = title,
                Message = message,
                OkText = okLabel,
                CancelText = cancelLabel,
            };

            var result = UserDialogs.Instance.ConfirmAsync(config);

            return result;
        }

        public Task ShowDialog(string message, string title, string buttonLabel)
        {
            return UserDialogs.Instance.AlertAsync(message, title, buttonLabel);
        }

        public Task<PromptResult> ShowPromptAsyncDialog(InputType inputType, string title, string message)
        {
            return UserDialogs.Instance.PromptAsync(new PromptConfig
            {
                InputType = inputType,
                Title = title,
                Message = message,
                CancelText = AppResources.Cancel,
                OkText = AppResources.OK,
            });
        }

        public async Task ShowToast(string message, int duration = 3)
        {
            try
            {
                var timeSpan = TimeSpan.FromSeconds(duration);

                ToastConfig config = new ToastConfig(message)
                {
                    Position = ToastPosition.Bottom,
                    Duration = timeSpan,
                };

                UserDialogs.Instance.Toast(config);
            }
            catch (Exception ex)
            {
                LogHelper.LogException($"Exception occured while showing the Toast {0}", ex);
            }

            await Task.FromResult(true);
        }

        public List<ListItemModel> GetCameraActionsheetItems()
        {
            List<ListItemModel> listItems = new List<ListItemModel>()
            {
                new ListItemModel()
                {
                    ViewModel = null,
                    Header = AppResources.MakePhoto,
                    Identifier = 1,
                    IconSource = "Add_a_photo_blue.png",
                },
                new ListItemModel()
                {
                    ViewModel = null,
                    Header = AppResources.ChooseFromPhotos,
                    Identifier = 2,
                    IconSource = "Galery.png",
                }
            };

            return listItems;
        }
    }
}
