using System;
using System.Collections.Generic;
using XamarinEnterpriseApp.Xamarin.Core.Ioc;
using XamarinEnterpriseApp.Xamarin.Core.Models;
using XamarinEnterpriseApp.Xamarin.Core.Services;
using XamarinEnterpriseApp.Xamarin.Core.ViewModels.Base;
using Xamarin.Forms;

namespace XamarinEnterpriseApp.Xamarin.Core.Views
{
    public partial class FooterView : ContentView
    {
        public FooterView()
        {
            InitializeComponent();

            if (Device.RuntimePlatform == Device.Android)
            {
                LeftButton.Padding = new Thickness(1, 5);
                RightButton.Padding = new Thickness(1, 5);
            }

            INavigationService navigationService = DependencyResolver.Resolve<INavigationService>();

            AddReportRequest addReportRequest= GlobalSetting.Instance.AddReportRequest;


            if (addReportRequest!=null && !addReportRequest.IsReportEditMode && navigationService.NavigationCount > 1)
            {
                LeftButton.IsVisible = true;
            }
            else
            {
                LeftButton.IsVisible = false;
            }

        }
    }
}
