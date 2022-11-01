using System;
using System.Collections.Generic;
using System.Windows.Input;
using XamarinEnterpriseApp.Xamarin.Core.Dependency;
using XamarinEnterpriseApp.Xamarin.Core.Ioc;
using XamarinEnterpriseApp.Xamarin.Core.ViewModels;
using Xamarin.Forms;

namespace XamarinEnterpriseApp.Xamarin.Core.Views
{
    public partial class LocationToggleView : ContentView
    {
        public static readonly BindableProperty CurrentStateProperty =
        BindableProperty.Create("CurrentState",
                                typeof(int),
                                typeof(LocationToggleView),
                                1,
                                BindingMode.OneWay);

        public int CurrentState
        {
            get
            {
                return (int)GetValue(CurrentStateProperty);
            }
            set
            {
                SetValue(CurrentStateProperty, value);
            }
        }

        public static readonly BindableProperty GPSStateProperty =
        BindableProperty.Create("GPSState",
                               typeof(int),
                               typeof(LocationToggleView),
                               1,
                               BindingMode.OneWay);

        public int GPSState
        {
            get
            {
                return (int)GetValue(GPSStateProperty);
            }
            set
            {
                SetValue(GPSStateProperty, value);
            }
        }

        public static readonly BindableProperty MyLocationCommandProperty =
        BindableProperty.Create("MyLocationCommand",
                        typeof(ICommand),
                        typeof(LocationToggleView),
                        null,
                        BindingMode.OneWay);

        public ICommand MyLocationCommand
        {
            get
            {
                return (ICommand)GetValue(MyLocationCommandProperty);
            }
            set
            {
                SetValue(MyLocationCommandProperty, value);
            }
        }

        public LocationToggleView()
        {
            InitializeComponent();

            if (Device.RuntimePlatform == Device.iOS)
            {

                Image_Button.WidthRequest = 52;
                Image_Button.HeightRequest = 52;
            }
            else
            {
                Image_Button.WidthRequest = 32;
                Image_Button.HeightRequest = 32;
            }

            CurrentState = 1;

            PropertyChanged += LocationToggleView_PropertyChanged;

            SetCurrentState();
        }

        private void SetCurrentState()
        {
            string imageSource = string.Empty;

            if (Device.RuntimePlatform == Device.iOS)
            {

                switch (CurrentState)
                {
                    case 1:
                        imageSource = "currentLocationSelectedButton.png";
                        break;
                    case 2:
                        imageSource = "currentLocationButton.png";
                        break;
                    default:
                        imageSource = "currentLocationButton.png";
                        break;
                }

            }
            else
            {
                switch (GPSState)
                {
                    case 1:
                        imageSource = "currentLocationAndroidIcon_black.png";
                        break;
                    case 2:
                        imageSource = "currentLocationAndroidIcon_blue.png";
                        break;
                    default:
                        imageSource = "currentLocationAndroidIcon_black.png";
                        break;
                }
            }

            Image_Button.Source = imageSource;

        }

        private void LocationToggleView_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(CurrentState))
            {
                SetCurrentState();
            }
            else if (e.PropertyName == nameof(GPSState))
            {
                SetCurrentState();
            }
        }

        private void Image_Button_Clicked(object sender, EventArgs e)
        {
            MyLocationCommand?.Execute(null);
        }
    }
}
