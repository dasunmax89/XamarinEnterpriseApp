using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using XamarinEnterpriseApp.Xamarin.Core.Constants;
using XamarinEnterpriseApp.Xamarin.Core.Helpers;
using XamarinEnterpriseApp.Xamarin.Core.Ioc;
using XamarinEnterpriseApp.Xamarin.Core.Models;
using XamarinEnterpriseApp.Xamarin.Core.Services;
using XamarinEnterpriseApp.Xamarin.Core.ViewModels;
using XamarinEnterpriseApp.Xamarin.Core.ViewModels.Base;

using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;

namespace XamarinEnterpriseApp.Xamarin.Core.Controls
{
    public class ExtendedMapIOS : Map
    {
        private Pin _previousMarker;
        private Pin _positionMarker;
        private GeoReportsDataResponse _geoReportsData;
        private MDGeometryRenderer _prevLayer;
        private bool _ignoreMove;

        private readonly IGeoLocationService _geoLocationService;

        public static readonly BindableProperty MapPositionProperty =
        BindableProperty.Create("MapPosition",
                                typeof(GeographicLocation),
                                typeof(ExtendedMapIOS),
                                GeographicLocation.GetDefaultLocation(),
                                BindingMode.OneWay);

        public GeographicLocation MapPosition
        {
            get
            {
                return (GeographicLocation)GetValue(MapPositionProperty);
            }
            set
            {
                SetValue(MapPositionProperty, value);
            }
        }

        public ExtendedMapIOS()
        {
            UnSubscribeToEvents();
            SubscribeToEvents();

            _geoLocationService = DependencyResolver.Resolve<IGeoLocationService>();
            PropertyChanged += ExtendedMap_PropertyChanged;
            PinClicked += Map_PinClicked;
            MapClicked += ExtendedMapIOS_MapClicked;
            MapLongClicked += ExtendedMapIOS_MapLongClicked;
            CameraIdled += ExtendedMapIOS_CameraIdled;
            CameraMoveStarted += ExtendedMapIOS_CameraMoveStarted;
            PinDragEnd += ExtendedMapIOS_PinDragEnd;

            MyLocationEnabled = true;
            UiSettings.CompassEnabled = true;
            UiSettings.ZoomControlsEnabled = true;
            UiSettings.MyLocationButtonEnabled = false;

            SetCenter(GeographicLocation.GetDefaultLocation());
        }

        private void ExtendedMapIOS_MapLongClicked(object sender, MapLongClickedEventArgs e)
        {
            var vm = BindingContext as IMapViewModel;

            if (vm.IsDraggablePointer)
            {
                var position = e.Point;

                AddDraggableMarker(position);

                GeographicLocation center = new GeographicLocation(position.Latitude, position.Longitude);

                var currentZoom = CameraPosition.Zoom;

                MoveMapCamera(center, currentZoom);
            }
        }

        private void AddDraggableMarker(Position position)
        {
            if (_positionMarker != null)
            {
                Pins.Remove(_positionMarker);
            }

            var markerOptions = new Pin();

            markerOptions.Position = position;
            markerOptions.IsDraggable = true;
            markerOptions.Icon = BitmapDescriptorFactory.FromBundle("locationCenterPin.png");
            markerOptions.Label = "Center";
            Pins.Add(markerOptions);

            _positionMarker = markerOptions;
        }

        private void ExtendedMapIOS_CameraMoveStarted(object sender, CameraMoveStartedEventArgs e)
        {

        }

        private void ExtendedMapIOS_CameraIdled(object sender, CameraIdledEventArgs e)
        {
            var vm = BindingContext as IMapViewModel;

            var target = e.Position.Target;

            var currentZoom = e.Position.Zoom;

            if (vm.IsMapLayerAvailable || !vm.IsDraggablePointer)
            {
                GeographicLocation center = new GeographicLocation(target.Latitude, target.Longitude);

                MoveMapCamera(center, currentZoom);
            }
        }

        private void ExtendedMapIOS_PinDragEnd(object sender, PinDragEventArgs e)
        {
            var vm = BindingContext as IMapViewModel;

            if (vm.IsDraggablePointer)
            {
                var target = e.Pin.Position;

                GeographicLocation center = new GeographicLocation(target.Latitude, target.Longitude);

                center.IsDraggablePointer = vm.IsDraggablePointer;

                var currentZoom = CameraPosition.Zoom;

                MoveMapCamera(center, currentZoom);
            }
        }

        private void ExtendedMapIOS_MapClicked(object sender, MapClickedEventArgs e)
        {
            UnSelectPin();

            GeographicLocation center = new GeographicLocation(e.Point.Latitude, e.Point.Longitude);

            MessagingCenter.Send(GlobalSetting.Instance, Core.Constants.MessageKeys.MapClicked, center);
        }

        private void MoveMapCamera(GeographicLocation center, double zoom = 0)
        {
            var bounds = CoordinatesHelper.CalculateBounds(this.Region, true);

            MapCameraIdleEventArgs args = new MapCameraIdleEventArgs()
            {
                Center = center,
                Bounds = bounds,
                ZoomLevel = zoom
            };

            MessagingCenter.Send(GlobalSetting.Instance, Core.Constants.MessageKeys.Map_CameraIdled, args);

        }

        private void Map_PinClicked(object sender, object e)
        {

            PinClickedEventArgs args = e as PinClickedEventArgs;

            Pin pin = args.Pin;

            GeographicLocation center = null;

            if (pin != null)
            {
                if (pin.Tag == null)
                {
                    args.Handled = true;

                    return;
                }

                UnSelectPin();

                if (pin.Tag is WorkloadListItem)
                {
                    WorkloadListItem workloadListItem = pin.Tag as WorkloadListItem;

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

                    pin.Icon = bitmapDescriptor;

                    MessagingCenter.Send(GlobalSetting.Instance, MessageKeys.MarkerTapped, workloadListItem);

                    center = workloadListItem.Position;
                }
                else if (pin.Tag is GJFeature)
                {
                    bool objectSelectionEnabled = _geoReportsData.ObjectSelectionEnabled;

                    GJFeature featureData = pin.Tag as GJFeature;

                    if (objectSelectionEnabled)
                    {
                        pin.Icon = ImageHelper.GetFeatureFromView(featureData, true);
                    }

                    MessagingCenter.Send<GlobalSetting, GJFeature>(GlobalSetting.Instance, Core.Constants.MessageKeys.FeatureTapped, featureData);

                    center = featureData.Coordinates.ToGeographicLocation();
                }

                if (center != null)
                {
                    _ignoreMove = true;

                    SetCenter(center);

                    _ignoreMove = false;

                    _previousMarker = pin;
                }

                args.Handled = true;
            }
        }

        private void UnSelectPin()
        {
            if (_previousMarker != null)
            {
                if (_previousMarker.Tag is WorkloadListItem)
                {
                    WorkloadListItem workloadListItem = _previousMarker.Tag as WorkloadListItem;

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

                    _previousMarker.Icon = bitmapDescriptor;

                    _previousMarker = null;

                }
                else if (_previousMarker.Tag is GJFeature)
                {
                    GJFeature featureData = _previousMarker.Tag as GJFeature;

                    bool isSelected = featureData.FeatureId == _geoReportsData.LinkedFeatureId;

                    _previousMarker.Icon = ImageHelper.GetFeatureFromView(featureData, isSelected);

                    _previousMarker = null;
                }
            }
        }

        private void ExtendedMap_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            var vm = BindingContext as IMapViewModel;

            if (e.PropertyName == nameof(MapPosition))
            {
                SetCenter(MapPosition);
            }
            else if (e.PropertyName == nameof(Region))
            {
                if (Region != null)
                {

                }

                vm.LocationCurrentState = 2;
            }
        }

        public void SetCenter(GeographicLocation location)
        {
            Position position = location.ToGoogleMaps();

            double radious = 100;

            if (location.Radious > 0)
            {
                radious = location.Radious;
            }

            MoveToRegion(MapSpan.FromCenterAndRadius(
                    position,
                    Distance.FromMeters(radious)));
        }

        public async Task SetExtent(MapExtent extent)
        {
            GeographicLocation minLatLng = new GeographicLocation(extent.MinY, extent.MinX);
            GeographicLocation maxLatLng = new GeographicLocation(extent.MaxY, extent.MaxX);

            await MoveCamera(CameraUpdateFactory.NewBounds(
                 new Bounds(minLatLng.ToGoogleMaps(),
                            maxLatLng.ToGoogleMaps()),
                MapExtent.DefaultPaddinIOs));
        }

        public void UnSubscribeToEvents()
        {
            MessagingCenter.Unsubscribe<GlobalSetting, GeoReportsDataResponse>(GlobalSetting.Instance, MessageKeys.MapGeoJsonRetrieved);
            MessagingCenter.Unsubscribe<GlobalSetting, MapExtent>(GlobalSetting.Instance, MessageKeys.MapDefaultExtentRequested);
            MessagingCenter.Unsubscribe<GlobalSetting>(GlobalSetting.Instance, MessageKeys.MapClearLayerRequested);
            MessagingCenter.Unsubscribe<GlobalSetting, GeographicLocation>(GlobalSetting.Instance, MessageKeys.AddPinToMap);
        }

        public void SubscribeToEvents()
        {
            MessagingCenter.Subscribe<GlobalSetting, GeoReportsDataResponse>(GlobalSetting.Instance, MessageKeys.MapGeoJsonRetrieved, (sender, args) =>
            {
                if (args != null)
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        AddGeoJsonLayer(args);
                    });
                }
            });

            MessagingCenter.Subscribe<GlobalSetting, MapExtent>(GlobalSetting.Instance, MessageKeys.MapDefaultExtentRequested, async (sender, args) =>
            {
                if (args != null)
                {
                    await Task.Delay(500);

                    await SetExtent(args);
                }
            });

            MessagingCenter.Subscribe<GlobalSetting>(GlobalSetting.Instance, MessageKeys.MapClearLayerRequested, (sender) =>
            {
                UnSelectPin();

                RemovePreviousLayer();
            });


            MessagingCenter.Subscribe<GlobalSetting, GeographicLocation>(GlobalSetting.Instance, MessageKeys.AddPinToMap, (sender, args) =>
            {
                if (args != null)
                {
                    if (args.IsUserLocation)
                    {
                        //already in platform
                    }

                    var vm = BindingContext as IMapViewModel;

                    if (vm.IsDraggablePointer)
                    {
                        Position center = new Position(args.Latitude, args.Longitude);

                        AddDraggableMarker(center);

                        var currentZoom = CameraPosition.Zoom;

                        MoveMapCamera(args, currentZoom);
                    }
                }
            });
        }

        private void AddGeoJsonLayer(GeoReportsDataResponse geoReportsData)
        {
            try
            {
                _geoReportsData = geoReportsData;

                RemovePreviousLayer();

                var renderer = new MDGeometryRenderer(this, _geoReportsData);

                renderer.Render();

                _prevLayer = renderer;
            }
            catch (Exception ex)
            {
                LogHelper.LogException("AddGeoJsonLayer-Exception occured", ex);
            }
        }

        private void RemovePreviousLayer()
        {
            try
            {
                if (_prevLayer != null)
                {
                    _prevLayer.Clear();
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogException("RemovePreviousLayer-Exception occured", ex);
            }
        }
    }
}
