using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using XamarinEnterpriseApp.Xamarin.Core.Helpers;
using XamarinEnterpriseApp.Xamarin.Core.Models;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;

namespace XamarinEnterpriseApp.Xamarin.Core.Views
{
    public partial class MapViewIOS : ContentView, IMapView
    {
        public static readonly BindableProperty ItemsSourceProperty =
        BindableProperty.Create("ItemsSource",
                              typeof(ObservableCollection<WorkloadListItem>),
                              typeof(MapViewIOS),
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
                            typeof(MapViewIOS),
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
                           typeof(MapViewIOS),
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


        public MapViewIOS()
        {
            InitializeComponent();
            PropertyChanged += Component_PropertyChanged;
        }

        private void Component_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ItemsSource))
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    PopulatePins(ItemsSource);
                });
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

        public async Task SetMapExtent(MapExtent mapExtent)
        {
            if (mapExtent != null)
            {
                await Map.SetExtent(mapExtent);
            }
        }

        private void PopulatePins(ObservableCollection<WorkloadListItem> itemsSource)
        {
            if (itemsSource != null)
            {
                Map.Pins.Clear();

                foreach (var workloadListItem in itemsSource)
                {
                    if (workloadListItem.Position != null)
                    {
                        BitmapDescriptor bitmapDescriptor = null;

                        if (workloadListItem.IsImageMarkerIcon)
                        {
                            bitmapDescriptor = BitmapDescriptorFactory.FromBundle(workloadListItem.MarkerIconSource);
                        }
                        else
                        {
                            if (workloadListItem.Siblings.Any())
                            {
                                bitmapDescriptor = ImageHelper.GetMarkerFromView(workloadListItem);
                            }
                            else
                            {
                                var stream = new MemoryStream(Convert.FromBase64String(workloadListItem.MarkerIconSource));

                                bitmapDescriptor = BitmapDescriptorFactory.FromStream(stream, workloadListItem.ReportItem.Statetype);
                            }
                        }

                        Pin pin = new Pin()
                        {
                            Position = workloadListItem.Position.ToGoogleMaps(),
                            Address = workloadListItem.Header1,
                            Label = workloadListItem.Index.ToString(),
                            Tag = workloadListItem,
                            Icon = bitmapDescriptor,
                            Type = PinType.Place,
                            Flat = true,
                        };

                        Map.Pins.Add(pin);
                    }
                }
            }
        }
    }
}
