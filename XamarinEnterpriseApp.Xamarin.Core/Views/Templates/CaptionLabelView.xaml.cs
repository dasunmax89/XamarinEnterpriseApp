using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace XamarinEnterpriseApp.Xamarin.Core.Views
{
    public partial class CaptionLabelView : ContentView
    {
        public static readonly BindableProperty CaptionProperty =
        BindableProperty.Create("Caption",
                               typeof(string),
                               typeof(CaptionLabelView),
                               string.Empty,
                               BindingMode.OneWay);

        public static readonly BindableProperty TextProperty =
        BindableProperty.Create("Text",
                              typeof(string),
                              typeof(CaptionLabelView),
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

        public string Text
        {
            get
            {
                return (string)GetValue(TextProperty);
            }
            set
            {
                SetValue(TextProperty, value);
            }
        }

        public CaptionLabelView()
        {
            InitializeComponent();

            PropertyChanged += CaptionLabelView_PropertyChanged;
        }

        private void CaptionLabelView_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Caption))
            {
                CaptionLabel.Text = Caption;
            }
            else if (e.PropertyName == nameof(Text))
            {
                ValueLabel.Text = Text;
            }
        }
    }
}
