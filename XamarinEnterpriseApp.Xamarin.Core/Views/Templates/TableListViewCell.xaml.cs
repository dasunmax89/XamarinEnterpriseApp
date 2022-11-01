using System;
using System.Collections.Generic;
using XamarinEnterpriseApp.Xamarin.Core.Extensions;
using XamarinEnterpriseApp.Xamarin.Core.Ioc;
using XamarinEnterpriseApp.Xamarin.Core.Localization;
using XamarinEnterpriseApp.Xamarin.Core.Models;
using XamarinEnterpriseApp.Xamarin.Core.Services;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace XamarinEnterpriseApp.Xamarin.Core.Views
{
    public partial class TableListViewCell : ContentView
    {
        public TableListViewCell()
        {
            InitializeComponent();

            PropertyChanged += Component_PropertyChanged;
        }

        private void Component_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(BindingContext))
            {
                AddAction(false);
            }
        }
        private void ItemTapped(object sender, EventArgs args)
        {
            AddAction(true);
        }

        private async void AddAction(bool isAction)
        {
            var binding = BindingContext as ListItemModel;

            if (binding != null)
            {
                if (!string.IsNullOrEmpty(binding.IconSource))
                {
                    DetailIcon.IsVisible = true;
                    DetailIcon.Source = binding.IconSource.ToImageSource();
                }
                else
                {
                    DetailIcon.IsVisible = false;
                }

                if (binding.Caption == AppResources.TelephoneNumber && binding.Header != AppResources.NotApplicable)
                {
                    if (isAction)
                    {
                        string phone = binding.Tag as string;

                        IPhoneService phoneService = DependencyResolver.Resolve<IPhoneService>();

                        await phoneService.MakePhoneCall(phone);
                    }
                    else
                    {
                        ValueLabel.TextColor = Color.FromHex("#4f79a2");
                    }
                }
                else if (binding.Caption == AppResources.Email && binding.Header != AppResources.NotApplicable)
                {
                    if (isAction)
                    {
                        string email = binding.Tag as string;

                        var uri = new Uri($"mailto:{email}");

                        if (await Launcher.CanOpenAsync(uri))
                        {
                            await Launcher.OpenAsync(uri);
                        }
                        else
                        {
                            var dialogService = DependencyResolver.Resolve<IDialogService>();

                            await dialogService.ShowToast(AppResources.InstallEmailViewerMessage);
                        }
                    }
                    else
                    {
                        ValueLabel.TextColor = Color.FromHex("#4f79a2");
                    }
                }
            }
        }
    }
}
