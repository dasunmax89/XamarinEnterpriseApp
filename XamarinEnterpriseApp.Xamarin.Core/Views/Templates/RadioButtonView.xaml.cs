using System;
using XamarinEnterpriseApp.Xamarin.Core.Localization;
using XamarinEnterpriseApp.Xamarin.Core.Models;
using XamarinEnterpriseApp.Xamarin.Core.Models.Base;
using Xamarin.Forms;

namespace XamarinEnterpriseApp.Xamarin.Core.Views
{
    public partial class RadioButtonView : ContentView
    {
        public static readonly BindableProperty CaptionProperty =
        BindableProperty.Create("Caption",
                               typeof(string),
                               typeof(RadioButtonView),
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

        public static readonly BindableProperty IsSelectedProperty =
        BindableProperty.Create("IsSelected",
                               typeof(bool),
                               typeof(RadioButtonView),
                               false,
                               BindingMode.OneWay);

        public bool IsSelected
        {
            get
            {
                return (bool)GetValue(IsSelectedProperty);
            }
            set
            {
                SetValue(IsSelectedProperty, value);
            }
        }

        public RadioButtonView()
        {
            InitializeComponent();

            PropertyChanged += RadioButtonView_PropertyChanged;
        }

        private void RadioButtonView_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Caption))
            {
                CaptionLabel.Text = Caption;

                //string selectedText = IsSelected ? AppResources.Checked : AppResources.UnChecked;

                //SetValue(AutomationProperties.HelpTextProperty, $"{Caption} {selectedText}");
            }
            else if (e.PropertyName == nameof(IsSelected))
            {
                CaptionLabel.Text = Caption;

                //string selectedText = IsSelected ? AppResources.Checked : AppResources.UnChecked;

                //SetValue(AutomationProperties.HelpTextProperty, $"{Caption} {selectedText}");
            }
        }
    }
}
