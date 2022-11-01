using System;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;

namespace XamarinEnterpriseApp.Xamarin.Core.Views
{
    public partial class WizardProgressView : Grid
    {
        public static readonly BindableProperty CaptionProperty =
        BindableProperty.Create("Caption",
                              typeof(string),
                              typeof(WizardProgressView),
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

        public static readonly BindableProperty CloseCommandProperty =
        BindableProperty.Create("CloseCommand",
                       typeof(ICommand),
                       typeof(WizardProgressView),
                       null,
                       BindingMode.OneWay);

        public ICommand CloseCommand
        {
            get
            {
                return (ICommand)GetValue(CloseCommandProperty);
            }
            set
            {
                SetValue(CloseCommandProperty, value);
            }
        }

        public static readonly BindableProperty BackCommandProperty =
        BindableProperty.Create("BackCommand",
               typeof(ICommand),
               typeof(WizardProgressView),
               null,
               BindingMode.OneWay);

        public ICommand BackCommand
        {
            get
            {
                return (ICommand)GetValue(BackCommandProperty);
            }
            set
            {
                SetValue(BackCommandProperty, value);
            }
        }

        public WizardProgressView()
        {
            InitializeComponent();

            PropertyChanged += SearchEntryView_PropertyChanged;
        }

        private void SearchEntryView_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Caption))
            {
                CaptionLabel.Text = Caption;
            }
        }

        private void CloseButton_Clicked(System.Object sender, System.EventArgs e)
        {
            CloseCommand?.Execute(this);
        }

        private void BackButton_Clicked(object sender, EventArgs args)
        {
            BackCommand?.Execute(this);
        }

    }
}
