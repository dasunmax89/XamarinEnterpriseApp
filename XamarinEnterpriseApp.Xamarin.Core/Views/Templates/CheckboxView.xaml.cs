using System;
using System.Collections.Generic;
using XamarinEnterpriseApp.Xamarin.Core.Localization;
using Xamarin.Forms;

namespace XamarinEnterpriseApp.Xamarin.Core.Views
{
    public partial class CheckboxView : ContentView
    {
        public static readonly BindableProperty IsSelectedProperty =
        BindableProperty.Create("IsSelected",
                               typeof(bool),
                               typeof(CheckboxView),
                               false,
                               BindingMode.TwoWay);

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

        public static readonly BindableProperty CaptionProperty =
        BindableProperty.Create("Caption",
                               typeof(string),
                               typeof(CheckboxView),
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

        public static readonly BindableProperty ErrorMsgProperty =
        BindableProperty.Create("ErrorMsg",
                            typeof(string),
                            typeof(CheckboxView),
                            string.Empty,
                            BindingMode.OneWay);


        public string ErrorMsg
        {
            get
            {
                return (string)GetValue(ErrorMsgProperty);
            }
            set
            {
                SetValue(ErrorMsgProperty, value);
            }
        }

        public CheckboxView()
        {
            InitializeComponent();

            PropertyChanged += CheckboxView_PropertyChanged;

            IsSelected = false;

            SetProperties(IsSelected);
        }

        private void SetProperties(bool isSelected)
        {
            CheckedImageView.Source = isSelected ? "Checkbox_checked.png" : "Checkbox_outline.png";

            string selectedText = isSelected ? AppResources.Checked : AppResources.UnChecked;

            SetValue(AutomationProperties.HelpTextProperty, $"{Caption} {selectedText}");
        }

        private void CheckboxTapped(object sender, EventArgs args)
        {
            IsSelected = !IsSelected;
        }

        private void CheckboxView_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(IsSelected))
            {
                SetProperties(IsSelected);
            }
            else if (e.PropertyName == nameof(Caption))
            {
                CaptionLabel.Text = Caption;

                SetProperties(IsSelected);
            }
            else if (e.PropertyName == nameof(ErrorMsg))
            {
                ErrorLabel.Text = ErrorMsg;
                bool isError = !string.IsNullOrEmpty(ErrorMsg);
                ErrorLabel.IsVisible = isError;
                ErrorLabel.SetValue(AutomationProperties.HelpTextProperty, ErrorMsg);
                if (isError)
                {
                    ErrorLabel.Focus();
                }
                else
                {
                    ErrorLabel.Unfocus();
                }
            }
        }
    }
}
