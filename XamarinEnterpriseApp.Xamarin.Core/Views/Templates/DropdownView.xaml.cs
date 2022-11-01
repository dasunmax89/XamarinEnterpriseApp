using System;
using System.Collections.Generic;
using XamarinEnterpriseApp.Xamarin.Core.Localization;
using Xamarin.Forms;

namespace XamarinEnterpriseApp.Xamarin.Core.Views
{
    public partial class DropdownView : ContentView
    {
        public static readonly BindableProperty CaptionProperty =
        BindableProperty.Create("Caption",
                               typeof(string),
                               typeof(DropdownView),
                               string.Empty,
                               BindingMode.OneWay);

        public static readonly BindableProperty TextProperty =
        BindableProperty.Create("Text",
                              typeof(string),
                              typeof(DropdownView),
                              null,
                              BindingMode.OneWay);

        public static readonly BindableProperty ErrorMsgProperty =
        BindableProperty.Create("ErrorMsg",
                              typeof(string),
                              typeof(DropdownView),
                              string.Empty,
                              BindingMode.OneWay);

        public static readonly BindableProperty IsReadOnlyProperty =
        BindableProperty.Create("IsReadOnly",
                          typeof(bool),
                          typeof(DropdownView),
                          false,
                          BindingMode.OneWay);

        public bool IsReadOnly
        {
            get
            {
                return (bool)GetValue(IsReadOnlyProperty);
            }
            set
            {
                SetValue(IsReadOnlyProperty, value);
            }
        }

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

        public static readonly BindableProperty HelpTextProperty =
        BindableProperty.Create("HelpText",
                          typeof(string),
                          typeof(DropdownView),
                          null,
                          BindingMode.OneWay);

        public string HelpText
        {
            get
            {
                return (string)GetValue(HelpTextProperty);
            }
            set
            {
                SetValue(HelpTextProperty, value);
            }
        }

        public DropdownView()
        {
            InitializeComponent();

            PropertyChanged += DropdownView_PropertyChanged;
            ErrorLabel.IsVisible = false;
            SetReadOnlyUI(false);
        }

        private void DropdownView_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Caption))
            {
                CaptionLabel.Text = Caption;
                CaptionLabel.SetValue(AutomationProperties.HelpTextProperty, Caption);
            }
            else if (e.PropertyName == nameof(Text))
            {
                ValueLabel.Text = Text;
                bool isValid = !string.IsNullOrEmpty(Text);

                ContainerFrame.BackgroundColor = !isValid ? Color.FromHex("#ffe0e0") : Color.FromHex("#F1F3F5");

                if (isValid)
                {
                    ErrorLabel.Text = string.Empty;
                }

                if (string.IsNullOrEmpty(HelpText))
                {
                    string helpText;

                    string selected = string.IsNullOrEmpty(Text) || Text == AppResources.SelectItem ? AppResources.None : Text;

                    helpText = $"{string.Format(AppResources.EnterValueFor, Caption)}.{string.Format(AppResources.ThisSelected, selected)}";

                    SetValue(AutomationProperties.HelpTextProperty, helpText);
                }
            }
            else if (e.PropertyName == nameof(ErrorMsg))
            {
                ErrorLabel.Text = ErrorMsg;
                bool isError = !string.IsNullOrEmpty(ErrorMsg);
                ErrorLabel.IsVisible = isError;
                ContainerFrame.BackgroundColor = isError ? Color.FromHex("#ffe0e0") : Color.FromHex("#F1F3F5");
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
            else if (e.PropertyName == nameof(IsReadOnly))
            {
                SetReadOnlyUI(IsReadOnly);
            }
            else if (e.PropertyName == nameof(HelpText))
            {
                SetHelpText(IsReadOnly || IsEnabled);
            }
        }

        private void SetReadOnlyUI(bool isReadOnly)
        {
            if (isReadOnly)
            {
                ExtendedFrame.BackgroundColor = Color.FromHex("#f7f7f7");
            }
            else
            {
                ExtendedFrame.BackgroundColor = Color.White;
            }
        }

        private void SetHelpText(bool isEnabledCustom)
        {
            string helpText = string.Empty;

            if (isEnabledCustom)
            {
                helpText = $"{HelpText}.Status {AppResources.Active}";
            }
            else
            {
                helpText = $"{HelpText}.Status {AppResources.InActive}";
            }

            SetValue(AutomationProperties.HelpTextProperty, helpText);
        }
    }
}
