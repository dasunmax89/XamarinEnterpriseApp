using System;
using System.Collections.Generic;
using XamarinEnterpriseApp.Xamarin.Core.Constants;
using Xamarin.Forms;

namespace XamarinEnterpriseApp.Xamarin.Core.Views
{
    public partial class ImageButtonView : ContentView
    {
        public static readonly BindableProperty CaptionProperty =
        BindableProperty.Create("Caption",
                              typeof(string),
                              typeof(ImageButtonView),
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

        public static readonly BindableProperty IsEnabledCustomProperty =
        BindableProperty.Create("IsEnabledCustom",
                               typeof(bool),
                               typeof(ImageButtonView),
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
                             typeof(ImageButtonView),
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
                            typeof(ImageButtonView),
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
                           typeof(ImageButtonView),
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

        public static readonly BindableProperty LabelStyleProperty =
        BindableProperty.Create("LabelStyle",
                    typeof(Style),
                    typeof(ImageButtonView),
                    null,
                    BindingMode.OneWay);

        public Style LabelStyle
        {
            get
            {
                return (Style)GetValue(LabelStyleProperty);
            }
            set
            {
                SetValue(LabelStyleProperty, value);
            }
        }

        public ImageButtonView()
        {
            InitializeComponent();

            PropertyChanged += ImageButtonView_PropertyChanged;

            if (Device.Idiom == TargetIdiom.Tablet)
            {
                WidthRequest = UIAnimationConstants.BUTTON_WIDTH;
            }
            else
            {

            }
        }

        private void ImageButtonView_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Caption))
            {
                CaptionLabel.Text = Caption;
            }
            else if (e.PropertyName == nameof(Image))
            {
                if (Image == "plusIcon_white.png")
                {
                    ButtonImageView.WidthRequest = 16;
                    ButtonImageView.HeightRequest = 16;
                    ButtonFrame.CornerRadius = 20;
                    ButtonFrame.Padding = new Thickness(0, 5);
                }
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
            else if (e.PropertyName == nameof(LabelStyle))
            {
                CaptionLabel.Style = LabelStyle;
            }
        }

        private void SetStyle(bool isEnabledCustom)
        {
            if (isEnabledCustom)
            {
                if (ActiveStyle != null)
                {
                    ButtonFrame.Style = ActiveStyle;
                }
            }
            else
            {
                if (DisabledStyle != null)
                {
                    ButtonFrame.Style = DisabledStyle;
                }
            }
        }
    }
}
