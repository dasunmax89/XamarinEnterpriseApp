using System;
using System.Collections.Generic;
using Xamarin.Forms;
using XamarinEnterpriseApp.Xamarin.Core.Extensions;
using XamarinEnterpriseApp.Xamarin.Core.Localization;

namespace XamarinEnterpriseApp.Xamarin.Core.Views
{
    public partial class DatePickerView : ContentView
    {
        public static readonly BindableProperty CaptionProperty =
        BindableProperty.Create("Caption",
                               typeof(string),
                               typeof(DatePickerView),
                               string.Empty,
                               BindingMode.OneWay);

        public static readonly BindableProperty DateValueProperty =
        BindableProperty.Create("DateValue",
                              typeof(DateTime),
                              typeof(DatePickerView),
                              DateTime.MinValue,
                              BindingMode.TwoWay);

        public static readonly BindableProperty ErrorMsgProperty =
        BindableProperty.Create("ErrorMsg",
                              typeof(string),
                              typeof(DatePickerView),
                              string.Empty,
                              BindingMode.OneWay);

        public static readonly BindableProperty IsReadOnlyProperty =
        BindableProperty.Create("IsReadOnly",
                             typeof(bool),
                             typeof(DatePickerView),
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

        public DateTime DateValue
        {
            get
            {
                return (DateTime)GetValue(DateValueProperty);
            }
            set
            {
                SetValue(DateValueProperty, value);
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

        public DatePickerView()
        {
            InitializeComponent();

            PropertyChanged += DatePickerView_PropertyChanged;
            PickerImage.Clicked += PickerImage_Clicked;
            ErrorLine.IsVisible = false;
            ErrorLabel.IsVisible = false;
            DateValue = DateTime.MinValue;
            SetReadOnlyUI(false);
        }

        private void SetReadOnlyUI(bool isReadOnly)
        {
            DatePickerEntry.IsEnabled = !isReadOnly;
            PickerImage.IsEnabled = !isReadOnly;

            if (isReadOnly)
            {
                var color = Color.FromHex("#f7f7f7");
                DatePickerEntry.BackgroundColor = color;
                ExtendedFrame.BackgroundColor = color;
            }
            else
            {
                var color = Color.White;
                DatePickerEntry.BackgroundColor = color;
                ExtendedFrame.BackgroundColor = color;
            }
        }

        private void PickerImage_Clicked(object sender, EventArgs e)
        {
            DatePickerEntry.Focus();
        }

        private void DatePickerView_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Caption))
            {
                CaptionLabel.Text = Caption;
                CaptionLabel.SetValue(AutomationProperties.HelpTextProperty, Caption);
                DatePickerEntry.SetValue(AutomationProperties.HelpTextProperty, string.Format(AppResources.EnterValueFor, Caption));
            }
            else if (e.PropertyName == nameof(DateValue))
            {
                DatePickerEntry.Date = DateValue;
                bool isInvalid = DateValue == DateTime.MinValue;
                ErrorLine.IsVisible = isInvalid;

                if (!isInvalid)
                {
                    ErrorLabel.Text = string.Empty;
                }
            }
            else if (e.PropertyName == nameof(ErrorMsg))
            {
                ErrorLabel.Text = ErrorMsg;
                bool isError = !string.IsNullOrEmpty(ErrorMsg);
                ErrorLine.IsVisible = isError;
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
            else if (e.PropertyName == nameof(IsReadOnly))
            {
                SetReadOnlyUI(IsReadOnly);
            }
        }

        private void OnDateSelected(object sender, DateChangedEventArgs args)
        {
            DateValue = args.NewDate;
        }
    }
}
