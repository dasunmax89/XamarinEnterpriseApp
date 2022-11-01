using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Acr.UserDialogs;
using XamarinEnterpriseApp.Xamarin.Core.Models;

namespace XamarinEnterpriseApp.Xamarin.Core.Services.Mocks
{
    public class DialogMockService : IDialogService
    {
        public DialogMockService()
        {
        }

        public List<ListItemModel> GetCameraActionsheetItems()
        {
            return new List<ListItemModel>();
        }

        public Task<bool> ShowConfirmation(string title, string message, string okLabel, string cancelLabel)
        {
            return Task.FromResult<bool>(true);
        }

        public Task ShowDialog(string message, string title, string buttonLabel)
        {
            return Task.FromResult<bool>(true);
        }

        public async Task ShowToast(string message, int duration = 3)
        {
            await Task.FromResult(true);
        }

        Task<PromptResult> IDialogService.ShowPromptAsyncDialog(InputType inputType, string title, string message)
        {
            return Task.FromResult<PromptResult>(new PromptResult(true, "OK"));
        }
    }
}
