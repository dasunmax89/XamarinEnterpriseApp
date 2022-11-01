using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace XamarinEnterpriseApp.Xamarin.Core.Views
{
    public partial class TransparentImageButtonView : ContentView
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

        public TransparentImageButtonView()
        {
            InitializeComponent();

            PropertyChanged += ImageButtonView_PropertyChanged;
        }

        private void ImageButtonView_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Caption))
            {
                CaptionLabel.Text = Caption;
            }
            else if (e.PropertyName == nameof(Image))
            {
                ButtonImageView.Source = Image;
            }
        }
    }
}
