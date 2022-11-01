using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using XamarinEnterpriseApp.Xamarin.Core.Controls;
using XamarinEnterpriseApp.Xamarin.Core.Models;
using Xamarin.Forms;

namespace XamarinEnterpriseApp.Xamarin.Core.Views
{
    public partial class MapViewAndroid : ContentView, IMapView
    {
        public static readonly BindableProperty ItemsSourceProperty =
        BindableProperty.Create("ItemsSource",
                              typeof(ObservableCollection<WorkloadListItem>),
                              typeof(MapViewAndroid),
                              null,
                              BindingMode.OneWay);

        public ObservableCollection<WorkloadListItem> ItemsSource
        {
            get
            {
                return (ObservableCollection<WorkloadListItem>)GetValue(ItemsSourceProperty);
            }
            set
            {
                SetValue(ItemsSourceProperty, value);
            }
        }

        public static readonly BindableProperty MapCenterProperty =
        BindableProperty.Create("MapCenter",
                             typeof(GeographicLocation),
                             typeof(MapViewAndroid),
                             null,
                             BindingMode.OneWay);

        public GeographicLocation MapCenter
        {
            get
            {
                return (GeographicLocation)GetValue(MapCenterProperty);
            }
            set
            {
                SetValue(MapCenterProperty, value);
            }
        }

        public static readonly BindableProperty MyCenterProperty =
        BindableProperty.Create("MyCenter",
                           typeof(GeographicLocation),
                           typeof(MapViewAndroid),
                           null,
                           BindingMode.OneWay);

        public GeographicLocation MyCenter
        {
            get
            {
                return (GeographicLocation)GetValue(MyCenterProperty);
            }
            set
            {
                SetValue(MyCenterProperty, value);
            }
        }

        public MapViewAndroid()
        {
            InitializeComponent();
            PropertyChanged += Component_PropertyChanged;
        }

        private void Component_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ItemsSource))
            {
                PopulatePins(ItemsSource);
            }
            else if (e.PropertyName == nameof(MapCenter))
            {
                SetCenter(MapCenter);
            }
            else if (e.PropertyName == nameof(MyCenter))
            {
                SetCenter(MyCenter);
            }
        }

        public void SetCenter(GeographicLocation positionObj)
        {
            if (positionObj != null && positionObj.UpdateMap)
            {
                Map.SetCenter(positionObj);
            }
        }

        private void PopulatePins(ObservableCollection<WorkloadListItem> itemsSource)
        {
            if (itemsSource != null)
            {
                Map.Pins.Clear();

                foreach (var item in itemsSource)
                {
                    if (item.Position != null)
                    {
                        ExtendedPinDroid pin = new ExtendedPinDroid()
                        {
                            Position = item.Position.ToFormMaps(),
                            Address = item.Header1,
                            Label = item.Index.ToString(),
                            WorkloadItem = item,
                            PinCount = itemsSource.Count,
                        };

                        Map.Pins.Add(pin);
                    }
                }
            }
        }
    }
}
