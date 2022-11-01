using System;
using XamarinEnterpriseApp.Xamarin.Core.Constants;
using XamarinEnterpriseApp.Xamarin.Core.Localization;
using Xamarin.Forms;

namespace XamarinEnterpriseApp.Xamarin.Core.Controls
{
    public class ExtendedButton : Button
    {
        public string TextAlignment { get; set; }

        public static readonly BindableProperty IsEnabledCustomProperty =
        BindableProperty.Create("IsEnabledCustom",
                                typeof(bool),
                                typeof(ExtendedButton),
                                false,
                                BindingMode.OneWay
                                );

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

        public static readonly BindableProperty ActiveStyleProperty =
        BindableProperty.Create("ActiveStyle",
                            typeof(Style),
                            typeof(ExtendedButton),
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
                           typeof(ExtendedButton),
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

        public static readonly BindableProperty HelpTextProperty =
        BindableProperty.Create("HelpText",
                           typeof(string),
                           typeof(ExtendedButton),
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

        public ExtendedButton()
        {
            var resources = Application.Current.Resources;

            PropertyChanged += ExtendedButton_PropertyChanged;

            SetStyle(IsEnabledCustom);

        }

        private void SetStyle(bool isEnabledCustom)
        {
            if (isEnabledCustom)
            {
                Style = ActiveStyle;
            }
            else
            {
                Style = DisabledStyle;
            }

            SetHelpText(isEnabledCustom);
        }

        private void SetHelpText(bool isEnabledCustom)
        {
            string helpText = string.Empty;

            if (isEnabledCustom)
            {
                helpText = $"{Text}.{HelpText}.Status {AppResources.Active}";
            }
            else
            {
                helpText = $"{Text}.{HelpText}.Status {AppResources.InActive}";
            }

            SetValue(AutomationProperties.HelpTextProperty, helpText);
        }

        private void ExtendedButton_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(IsEnabledCustom))
            {
                SetStyle(IsEnabledCustom);
            }
            else if (e.PropertyName == nameof(Text))
            {
                SetHelpText(IsEnabledCustom);
            }
            else if (e.PropertyName == nameof(HelpText))
            {
                SetHelpText(IsEnabledCustom);
            }
        }
    }
}
