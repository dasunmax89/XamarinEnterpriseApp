using System;
using XamarinEnterpriseApp.Xamarin.Core.Extensions;
using XamarinEnterpriseApp.Xamarin.Core.Ioc;
using XamarinEnterpriseApp.Xamarin.Core.Localization;
using XamarinEnterpriseApp.Xamarin.Core.Services;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace XamarinEnterpriseApp.Xamarin.Core.Views
{
    public class HyperlinkSpan : Span
    {
        public static readonly BindableProperty UrlProperty =
            BindableProperty.Create(nameof(Url), typeof(string), typeof(HyperlinkSpan), null);

        public string Url
        {
            get { return (string)GetValue(UrlProperty); }
            set { SetValue(UrlProperty, value); }
        }

        public HyperlinkSpan()
        {
            TextDecorations = TextDecorations.Underline;
            TextColor = Color.FromHex("#0775DB");

            GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(async () =>
                {
                    if (Url.IsURI())
                    {
                        string url = Url;

                        await Launcher.OpenAsync(Url);
                    }
                    else if (Url.IsValidPhoneNo())
                    {
                        string phone = Url;

                        IPhoneService phoneService = DependencyResolver.Resolve<IPhoneService>();

                        await phoneService.MakePhoneCall(phone);
                    }
                    else if (Url.IsValidEmail())
                    {
                        string email = Url;

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
                })
            });
        }
    }
}
