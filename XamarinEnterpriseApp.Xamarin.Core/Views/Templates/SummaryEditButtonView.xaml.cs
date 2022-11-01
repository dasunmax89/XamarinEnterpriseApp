using System;
using System.Collections.Generic;
using System.Windows.Input;
using XamarinEnterpriseApp.Xamarin.Core.Constants;
using Xamarin.Forms;

namespace XamarinEnterpriseApp.Xamarin.Core.Views
{
    public partial class SummaryEditButtonView : ContentView
    {
        public static readonly BindableProperty CaptionProperty =
        BindableProperty.Create("Caption",
                              typeof(string),
                              typeof(SummaryEditButtonView),
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

        public static readonly BindableProperty HelpTextProperty =
        BindableProperty.Create("HelpText",
                              typeof(string),
                              typeof(SummaryEditButtonView),
                              string.Empty,
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

        public static readonly BindableProperty EditCommandProperty =
        BindableProperty.Create("EditCommand",
               typeof(ICommand),
               typeof(SummaryEditButtonView),
               null,
               BindingMode.OneWay);

        public ICommand EditCommand
        {
            get
            {
                return (ICommand)GetValue(EditCommandProperty);
            }
            set
            {
                SetValue(EditCommandProperty, value);
            }
        }


        public SummaryEditButtonView()
        {
            InitializeComponent();

            PropertyChanged += SummaryEditButtonView_PropertyChanged;

            if (Device.Idiom == TargetIdiom.Tablet)
            {
                WidthRequest = UIAnimationConstants.BUTTON_WIDTH;
            }
            else
            {

            }
        }

        private void SummaryEditButtonView_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Caption))
            {
                CaptionLabel.Text = Caption;
                CaptionLabel.SetValue(AutomationProperties.HelpTextProperty, Caption);
            }
            else if (e.PropertyName == nameof(HelpText))
            {
                EditButton.SetValue(AutomationProperties.HelpTextProperty, HelpText);
            }
        }

        private void Edit_ButtonClcked(object sender, System.EventArgs e)
        {
            EditCommand?.Execute(this);
        }
    }
}
