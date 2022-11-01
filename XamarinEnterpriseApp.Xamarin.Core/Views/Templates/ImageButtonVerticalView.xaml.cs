using System;
using System.Collections.Generic;
using System.Windows.Input;
using XamarinEnterpriseApp.Xamarin.Core.Constants;
using XamarinEnterpriseApp.Xamarin.Core.Dependency;
using XamarinEnterpriseApp.Xamarin.Core.Localization;
using XamarinEnterpriseApp.Xamarin.Core.ViewModels.Base;
using Xamarin.Forms;

namespace XamarinEnterpriseApp.Xamarin.Core.Views
{
    public partial class ImageButtonVerticalView : ContentView
    {
        private readonly IPlatformManager _platformManager;

        public static readonly BindableProperty CaptionProperty =
        BindableProperty.Create("Caption",
                              typeof(string),
                              typeof(ImageButtonVerticalView),
                              string.Empty,
                              BindingMode.OneWay);

        public string Caption
        {
            get
            {
                return (string)GetValue(CaptionProperty);
            }
            set
            {
                SetValue(CaptionProperty, value);
            }
        }

        public static readonly BindableProperty CommandProperty =
        BindableProperty.Create("Command",
                              typeof(ICommand),
                              typeof(ImageButtonVerticalView),
                              null,
                              BindingMode.OneWay);

        public ICommand Command
        {
            get
            {
                return (ICommand)GetValue(CommandProperty);
            }
            set
            {
                SetValue(CommandProperty, value);
            }
        }

        public static readonly BindableProperty IsEnabledCustomProperty =
        BindableProperty.Create("IsEnabledCustom",
                               typeof(bool),
                               typeof(ImageButtonVerticalView),
                               false,
                               BindingMode.OneWay);

        public bool IsEnabledCustom
        {
            get
            {
                return (bool)GetValue(IsEnabledCustomProperty);
            }
            set
            {
                SetValue(IsEnabledCustomProperty, value);
            }
        }

        public static readonly BindableProperty ImageProperty =
        BindableProperty.Create("Image",
                             typeof(string),
                             typeof(ImageButtonVerticalView),
                             string.Empty,
                             BindingMode.OneWay);

        public string Image
        {
            get
            {
                return (string)GetValue(ImageProperty);
            }
            set
            {
                SetValue(ImageProperty, value);
            }
        }

        public static readonly BindableProperty ActiveStyleProperty =
        BindableProperty.Create("ActiveStyle",
                            typeof(Style),
                            typeof(ImageButtonVerticalView),
                            null,
                            BindingMode.OneWay);

        public Style ActiveStyle
        {
            get
            {
                return (Style)GetValue(ActiveStyleProperty);
            }
            set
            {
                SetValue(ActiveStyleProperty, value);
            }
        }


        public static readonly BindableProperty DisabledStyleProperty =
        BindableProperty.Create("DisabledStyle",
                           typeof(Style),
                           typeof(ImageButtonVerticalView),
                           null,
                           BindingMode.OneWay);

        public Style DisabledStyle
        {
            get
            {
                return (Style)GetValue(DisabledStyleProperty);
            }
            set
            {
                SetValue(DisabledStyleProperty, value);
            }
        }

        public ImageButtonVerticalView()
        {
            InitializeComponent();

            PropertyChanged += ImageButtonVerticalView_PropertyChanged;

            _platformManager = DependencyService.Get<IPlatformManager>();

            if (_platformManager.IsVoiceOverOn())
            {
                //SetValue(AutomationProperties.IsInAccessibleTreeProperty, false);

                ButtonImageView.SetValue(AutomationProperties.IsInAccessibleTreeProperty, true);

                ButtonImageView.SetValue(AutomationProperties.NameProperty, AppResources.Button);

                ButtonImageView.Clicked += ButtonImageView_Clicked;
            }
            else
            {
                var tapGestureRecognizer = new TapGestureRecognizer();
                tapGestureRecognizer.Tapped += (s, e) =>
                {
                    Command?.Execute(null);
                };

                GestureRecognizers.Add(tapGestureRecognizer);
            }

            if (Device.Idiom == TargetIdiom.Tablet)
            {
                WidthRequest = UIAnimationConstants.BUTTON_WIDTH;
            }
            else
            {

            }
        }

        private void ButtonImageView_Clicked(object sender, EventArgs e)
        {
            Command?.Execute(null);
        }

        private void ImageButtonVerticalView_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Caption))
            {
                CaptionLabel.Text = Caption;

                if (_platformManager.IsVoiceOverOn())
                {
                    ButtonImageView.SetValue(AutomationProperties.HelpTextProperty, Caption);
                }
            }
            else if (e.PropertyName == nameof(Image))
            {
                ButtonImageView.Source = Image;
            }
            else if (e.PropertyName == nameof(IsEnabledCustom))
            {
                SetStyle(IsEnabledCustom);
            }
            else if (e.PropertyName == nameof(ActiveStyle))
            {
                SetStyle(true);
            }

        }

        private void SetStyle(bool isEnabledCustom)
        {
            if (isEnabledCustom)
            {
                ButtonFrame.Style = ActiveStyle;
            }
            else
            {
                ButtonFrame.Style = DisabledStyle;
            }
        }
    }
}
