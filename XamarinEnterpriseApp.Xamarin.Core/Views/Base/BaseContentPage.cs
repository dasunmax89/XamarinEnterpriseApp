using System;
using System.Diagnostics;
using System.Threading.Tasks;
using XamarinEnterpriseApp.Xamarin.Core.Helpers;
using XamarinEnterpriseApp.Xamarin.Core.ViewModels.Base;
using Xamarin.Forms;

namespace XamarinEnterpriseApp.Xamarin.Core.Views.Base
{
    public partial class BaseContentPage : ContentPage
    {
        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        protected async Task DetectOrientation(double width, double height)
        {
            try
            {
                bool isPortrait = width < height;

                int orientation = isPortrait ? 1 : 2;

                var cachedDisplayOrientation = GlobalSetting.Instance.CachedDisplayOrientation;

                GlobalSetting.Instance.CachedDisplayOrientation = orientation;

                if (cachedDisplayOrientation != orientation)
                {
                    var vm = BindingContext as ViewModelBase;

                    await vm.OnAppearing();
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogException("DetectOrientation", ex);
            }
        }
    }
}
