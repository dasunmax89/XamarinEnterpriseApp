using System;
using System.Collections.Generic;
using System.Windows.Input;
using XamarinEnterpriseApp.Xamarin.Core.Models;
using XamarinEnterpriseApp.Xamarin.Core.ViewModels.Base;
using Xamarin.Forms;

namespace XamarinEnterpriseApp.Xamarin.Core.Views
{
    public partial class ToggleSwitchView : ContentView
    {
        public static readonly BindableProperty CurrentStateProperty =
        BindableProperty.Create("CurrentState",
                                typeof(ListItemModel),
                                typeof(ToggleSwitchView),
                                null,
                                BindingMode.OneWay);

        public ListItemModel CurrentState
        {
            get
            {
                return (ListItemModel)GetValue(CurrentStateProperty);
            }
            set
            {
                SetValue(CurrentStateProperty, value);
            }
        }

        public static readonly BindableProperty SelectedCommandProperty =
        BindableProperty.Create("SelectedCommand",
                    typeof(ICommand),
                    typeof(ToggleSwitchView),
                    null,
                    BindingMode.OneWay);

        public ICommand SelectedCommand
        {
            get
            {
                return (ICommand)GetValue(SelectedCommandProperty);
            }
            set
            {
                SetValue(SelectedCommandProperty, value);
            }
        }


        public ToggleSwitchView()
        {
            InitializeComponent();

            PropertyChanged += ToggleSwitchView_PropertyChanged;

            SetProperties(CurrentState);
        }

        private void SetProperties(ListItemModel currentState)
        {
            bool isSelected = currentState != null && currentState.IsSelected;

            CheckedImageView.Source = isSelected ? "Toggle_on.png" : "Toggle_off.png";
        }

        private void CheckboxTapped(object sender, EventArgs args)
        {
            SetProperties(CurrentState);

            SelectedCommand?.Execute(CurrentState);
        }

        private void ToggleSwitchView_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(CurrentState))
            {
                SetProperties(CurrentState);
            }
        }
    }
}
