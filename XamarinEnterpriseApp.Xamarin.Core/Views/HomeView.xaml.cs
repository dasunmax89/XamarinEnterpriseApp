using System;
using System.Collections.Generic;
using XamarinEnterpriseApp.Xamarin.Core.ViewModels.Base;
using XamarinEnterpriseApp.Xamarin.Core.Views.Base;
using Xamarin.Forms;

namespace XamarinEnterpriseApp.Xamarin.Core.Views
{
    public partial class HomeView : BaseContentPage
    {
        public HomeView()
        {
            InitializeComponent();
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

            //to receive push notification all the time
            //vm?.UnSubscribeToEvents();

            vm?.OnDisappearing();
        }
    }
}
