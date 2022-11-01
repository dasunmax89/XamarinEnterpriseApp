using System;
using System.Collections.Generic;
using XamarinEnterpriseApp.Xamarin.Core.ViewModels.Base;
using XamarinEnterpriseApp.Xamarin.Core.Views.Base;
using Xamarin.Forms;

namespace XamarinEnterpriseApp.Xamarin.Core.Views
{
    public partial class InitialView : BaseContentPage
    {
        public InitialView()
        {
            try
            {
                InitializeComponent();
            }
            catch (Exception ex)
            {

            }
        }

        protected async override void OnAppearing()
        {
            var vm = BindingContext as ViewModelBase;

            if (vm != null)
            {
                vm.UnSubscribeToEvents();
                vm.SubscribeToEvents();
                await vm.OnAppearing();
            }
        }

        protected override void OnDisappearing()
        {
            var vm = BindingContext as ViewModelBase;

            vm?.UnSubscribeToEvents();

            vm?.OnDisappearing();
        }
    }
}
