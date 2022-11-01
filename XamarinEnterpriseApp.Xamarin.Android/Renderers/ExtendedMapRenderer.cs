using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Android.Content;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.Graphics;
using Android.Util;
using Com.Google.Maps.Android.Data.Geojson;
using XamarinEnterpriseApp.Xamarin.Core;
using XamarinEnterpriseApp.Xamarin.Core.Constants;
using XamarinEnterpriseApp.Xamarin.Core.Controls;
using XamarinEnterpriseApp.Xamarin.Core.Helpers;
using XamarinEnterpriseApp.Xamarin.Core.Ioc;
using XamarinEnterpriseApp.Xamarin.Core.Models;
using XamarinEnterpriseApp.Xamarin.Core.Services;
using XamarinEnterpriseApp.Xamarin.Core.ViewModels;
using XamarinEnterpriseApp.Xamarin.Core.ViewModels.Base;
using XamarinEnterpriseApp.Xamarin.Droid.Extensions;
using XamarinEnterpriseApp.Xamarin.Droid.Helpers;
using XamarinEnterpriseApp.Xamarin.Droid.Renderers;
using Org.Json;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Maps.Android;
using Xamarin.Forms.Platform.Android;
using XFColor = Xamarin.Forms.Color;

[assembly: ExportRenderer(typeof(ExtendedMapDroid), typeof(ExtendedMapRenderer))]
namespace XamarinEnterpriseApp.Xamarin.Droid.Renderers
{
    public class ExtendedMapRenderer : MapRenderer
    {
        private IList<Pin> _extendedPins;
        private Marker _prevMarker;
        private GeoJsonFeature _prevFeature;
        private GeoJsonLayer _prevLayer;
        private bool _ignoreMove;
        private GeoReportsDataResponse _geoReportsData;
        private bool _isDisposed;
        private Marker _positionMarker;

        List<Marker> _markerList = new List<Marker>();

        public ExtendedMapRenderer(Context context) : base(context)
        {

        }

        protected override void Dispose(bool disposing)
        {
            _isDisposed = true;
            base.Dispose(disposing);
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Map> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null)
            {
                var gmap = NativeMap;

                ClearMapEvents(gmap);

                ClearMap();

                UnSubscribeToEvents();
            }

            if (e.NewElement != null)
            {
                var formsMap = (ExtendedMapDroid)e.NewElement;
                _extendedPins = formsMap.Pins;

                Control.GetMapAsync(this);

                SubscribeToEvents();
            }
        }

        private void ClearMapEvents(GoogleMap gmap)
        {
            gmap.MarkerClick -= OnPinClicked;
            gmap.MapClick -= NativeMap_MapClick;
            gmap.CameraIdle -= Map_CameraIdle;
            gmap.MapLongClick -= Map_LongClick;

        }

        private void ClearMap()
        {
            _prevMarker = null;

            _prevFeature = null;

            RemovePreviousLayer();
        }

        private void NativeMap_MapClick(object sender, GoogleMap.MapClickEventArgs e)
        {
            _ignoreMove = false;

            UnSelectPin();

            GeographicLocation position = new GeographicLocation(e.Point.Latitude, e.Point.Longitude);

            MessagingCenter.Send(GlobalSetting.Instance, Core.Constants.MessageKeys.MapClicked, position);
        }

        public void UnSubscribeToEvents()
        {
            MessagingCenter.Unsubscribe<GlobalSetting, List<ExtendedPinDroid>>(GlobalSetting.Instance, MessageKeys.MapExtentRequested);
            MessagingCenter.Unsubscribe<GlobalSetting, MapExtent>(GlobalSetting.Instance, MessageKeys.MapDefaultExtentRequested);
            MessagingCenter.Unsubscribe<GlobalSetting, GeoReportsDataResponse>(GlobalSetting.Instance, MessageKeys.MapGeoJsonRetrieved);
            MessagingCenter.Unsubscribe<GlobalSetting, GeoReportsDataResponse>(GlobalSetting.Instance, MessageKeys.MapClearLayerRequested);
            MessagingCenter.Unsubscribe<GlobalSetting, GeographicLocation>(GlobalSetting.Instance, MessageKeys.AddPinToMap);
        }

        public void SubscribeToEvents()
        {
            UnSubscribeToEvents();

            MessagingCenter.Subscribe<GlobalSetting, List<ExtendedPinDroid>>(GlobalSetting.Instance, MessageKeys.MapExtentRequested, (sender, args) =>
            {
                if (args != null && !_isDisposed)
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        ClearMap();

                        List<Pin> pins = args.Cast<Pin>().ToList();

                        SetExtent(pins);
                    });
                }
            });

            MessagingCenter.Subscribe<GlobalSetting, MapExtent>(GlobalSetting.Instance, MessageKeys.MapDefaultExtentRequested, (sender, args) =>
            {
                if (args != null && !_isDisposed)
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        ClearMap();

                        SetDefaultExtent(args);
                    });
                }
            });

            MessagingCenter.Subscribe<GlobalSetting, GeoReportsDataResponse>(GlobalSetting.Instance, MessageKeys.MapGeoJsonRetrieved, (sender, args) =>
            {
                if (args != null && !_isDisposed)
                {
                    AddGeoJsonLayer(args);
                }
            });

            MessagingCenter.Subscribe<GlobalSetting>(GlobalSetting.Instance, MessageKeys.MapClearLayerRequested, (sender) =>
            {
                if (!_isDisposed)
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        UnSelectFeature();

                        UnSelectPin();

                        RemovePreviousLayer();
                    });
                }
            });

            MessagingCenter.Subscribe<GlobalSetting, GeographicLocation>(GlobalSetting.Instance, MessageKeys.AddPinToMap, (sender, args) =>
            {
                if (args != null && !_isDisposed)
                {
                    if (args.IsUserLocation)
                    {
                        AddMyLocationMarkerToMap(args);

                        return;
                    }

                    var vm = Map.BindingContext as IMapViewModel;

                    if (vm.IsDraggablePointer)
                    {
                        Device.BeginInvokeOnMainThread(() =>
                        {
                            AddDraggableMarker(args);

                            SetCenter(args);

                            var currentZoom = Map.VisibleRegion.Radius.Meters;

                            MoveMapCamera(args, currentZoom);

                        });
                    }
                }
            });
        }

        public void SetCenter(GeographicLocation location)
        {
            Position position = new Position(location.Latitude, location.Longitude);

            double radious = 100;

            if (location.Radious > 0)
            {
                radious = location.Radious;
            }

            var formsMap = this.Element as ExtendedMapDroid;

            if (formsMap != null)
            {
                formsMap.MoveToRegion(MapSpan.FromCenterAndRadius(
                    position,
                    Distance.FromMeters(radious)));
            }
        }

        private void AddMyLocationMarkerToMap(GeographicLocation args)
        {
            try
            {
                if (args != null)
                {
                    if (_markerList.Any())
                    {
                        foreach (Marker m in _markerList)
                        {
                            m.Remove();
                        }

                        _markerList.Clear();
                    }

                    MarkerOptions markerOptions = new MarkerOptions();
                    markerOptions.Draggable(false);

                    LatLng cent = new LatLng(args.Latitude, args.Longitude);
                    markerOptions.SetPosition(cent);

                    BitmapDescriptor ic = BitmapDescriptorFactory.FromResource(Resource.Drawable.MyLocationCircle);

                    markerOptions.SetIcon(ic);

                    Marker marker = NativeMap.AddMarker(markerOptions);
                    _markerList.Add(marker);
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogException("AddMyLocationMarkerToMap-Exception occured", ex);
            }
        }

        protected override MarkerOptions CreateMarker(Pin pin)
        {
            var extendedPin = pin as ExtendedPinDroid;

            var marker = new MarkerOptions();
            marker.SetPosition(new LatLng(pin.Position.Latitude, pin.Position.Longitude));
            marker.SetTitle(pin.Label);
            marker.SetSnippet(pin.Address);
            SetMarkerIcon(null, false, marker);

            return marker;
        }

        private void SetMarkerIcon(Marker marker, bool isSelected, MarkerOptions markerOptions = null)
        {
            BitmapDescriptor bitmapDescriptor = null;

            Position position = new Position();

            if (marker != null)
            {
                position = new Position(marker.Position.Latitude, marker.Position.Longitude);
            }
            else if (markerOptions != null)
            {
                position = new Position(markerOptions.Position.Latitude, markerOptions.Position.Longitude);
            }

            var extendedPin = GetPin(position);

            if (extendedPin != null)
            {
                WorkloadListItem workloadListItem = extendedPin.WorkloadItem;

                //TODO
                if (workloadListItem.Siblings.Any())
                {
                    bitmapDescriptor = GetCustomBitmapDescriptor($"{workloadListItem.MarkerText}", 40, Resource.Drawable.RadioButton_Checked, XFColor.FromHex("#0775DB").ToAndroid(), 0f, true);
                }
                else
                {
                    Bitmap bitmap = workloadListItem.MarkerIconSource.ToBitmap();

                    bitmapDescriptor = BitmapDescriptorFactory.FromBitmap(bitmap);
                }
            }

            if (marker != null)
            {
                marker.SetIcon(bitmapDescriptor);
            }
            else if (markerOptions != null)
            {
                markerOptions.SetIcon(bitmapDescriptor);
            }
        }

        private BitmapDescriptor GetFeatureIcon(string featureId, string text, bool isSelected)
        {
            BitmapDescriptor bitmapDescriptor = null;

            int resourceId = 0;

            if (featureId == _geoReportsData.SelectedFeatureId || featureId == _geoReportsData.LinkedFeatureId)
            {
                resourceId = Resource.Drawable.gjpoint_icon_selected;
            }
            else
            {
                resourceId = isSelected ? Resource.Drawable.gjpoint_icon_selected : Resource.Drawable.gjpoint_icon;
            }

            float vertical = 18;

            bitmapDescriptor = GetCustomBitmapDescriptor($"{text}", 16.0f, resourceId, XFColor.FromHex("#000000").ToAndroid(), vertical, false);

            return bitmapDescriptor;
        }

        protected async override void OnMapReady(GoogleMap map)
        {
            try
            {
                base.OnMapReady(map);

                map.MarkerClick += OnPinClicked;
                map.MapClick += NativeMap_MapClick;
                map.CameraIdle += Map_CameraIdle;
                map.MapLongClick += Map_LongClick;
                map.MarkerDragEnd += Map_MarkerDragEnd;
                map.MyLocationEnabled = false;
                map.UiSettings.MyLocationButtonEnabled = false;
                map.UiSettings.ZoomControlsEnabled = false;
            }
            catch (Exception ex)
            {
                await ShowError(ex);

                LogHelper.LogException("OnMapReady-Exception occured", ex);
            }
        }

        private void Map_MarkerDragEnd(object sender, GoogleMap.MarkerDragEndEventArgs e)
        {
            var vm = Map.BindingContext as IMapViewModel;

            if (vm.IsDraggablePointer)
            {
                var target = e.Marker.Position;

                GeographicLocation center = new GeographicLocation(target.Latitude, target.Longitude);

                center.IsDraggablePointer = vm.IsDraggablePointer;

                var currentZoom = Map.VisibleRegion.Radius.Meters;

                MoveMapCamera(center, currentZoom);
            }
        }

        private void Map_LongClick(object sender, GoogleMap.MapLongClickEventArgs e)
        {
            var vm = Map.BindingContext as IMapViewModel;

            if (vm.IsDraggablePointer)
            {
                var position = e.Point;

                GeographicLocation center = new GeographicLocation(position.Latitude, position.Longitude);

                AddDraggableMarker(center);

                var currentZoom = Map.VisibleRegion.Radius.Meters;

                MoveMapCamera(center, currentZoom);
            }
        }

        private async void AddDraggableMarker(GeographicLocation center)
        {
            var context = Android.App.Application.Context;

            bool isGooglePlayAvailable = await PushHelper.CheckPlayServicesAvailability(context);

            if (center != null && isGooglePlayAvailable)
            {
                if (_positionMarker != null)
                {
                    _positionMarker.Remove();
                }

                var markerOptions = new MarkerOptions();

                markerOptions.SetPosition(new LatLng(center.Latitude, center.Longitude));
                markerOptions.Draggable(true);

                //TODO
                markerOptions.SetIcon(BitmapDescriptorFactory.FromResource(Resource.Drawable.MyLocationCircle));
                markerOptions.SetTitle("Center");

                var marker = NativeMap.AddMarker(markerOptions);

                _positionMarker = marker;
            }
        }

        private void AddGeoJsonLayer(GeoReportsDataResponse geoReportsData)
        {
            try
            {
                _geoReportsData = geoReportsData;

                RemovePreviousLayer();

                JSONObject jSONObject = new JSONObject(geoReportsData.GeoJson);

                GeoJsonLayer layer = new GeoJsonLayer(NativeMap, jSONObject);

                layer.FeatureClick += Layer_FeatureClick;

                var features = layer.Features.ToEnumerable();

                foreach (GeoJsonFeature feature in features)
                {
                    SetFeatureIcon(feature, false);
                }

                layer.AddLayerToMap();

                _prevLayer = layer;
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
                    _prevLayer.FeatureClick -= Layer_FeatureClick;

                    _prevLayer.RemoveLayerFromMap();
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogException("RemovePreviousLayer-Exception occured", ex);
            }
        }

        private void Layer_FeatureClick(object sender, Com.Google.Maps.Android.Data.Layer.FeatureClickEventArgs e)
        {
            UnSelectFeature();

            if (_geoReportsData != null)
            {
                var feature = e.P0 as GeoJsonFeature;

                SetFeatureIcon(feature, true);

                _prevFeature = feature;
            }
        }

        private void SetFeatureIcon(GeoJsonFeature feature, bool isSelected)
        {
            if (_geoReportsData != null)
            {
                string idField = _geoReportsData.CahdMapLayerDef?.Id;

                string captionField = _geoReportsData.CahdMapLayerDef?.DropdownSelect;

                bool objectSelectionEnabled = _geoReportsData.ObjectSelectionEnabled;

                string featureId = PlatformExtensions.GetProperty(feature, idField);

                string featureText = PlatformExtensions.GetProperty(feature, captionField);

                var featureData = _geoReportsData.Features.FirstOrDefault(x => x.FeatureId == featureId);

                GeoJsonPointStyle pointStyle = feature.PointStyle;

                if (pointStyle == null)
                {
                    pointStyle = new GeoJsonPointStyle();

                    feature.PointStyle = pointStyle;
                }

                if (objectSelectionEnabled)
                {
                    pointStyle.Icon = GetFeatureIcon(featureId, featureText, isSelected);
                }
                else
                {
                    pointStyle.Icon = GetFeatureIcon(featureId, featureText, false);
                }

                if (isSelected)
                {
                    MessagingCenter.Send<GlobalSetting, GJFeature>(GlobalSetting.Instance, Core.Constants.MessageKeys.FeatureTapped, featureData);
                }
            }
        }

        private async Task ShowError(Exception ex)
        {
            IDialogService dialogService = DependencyResolver.Resolve<IDialogService>();
            await dialogService.ShowToast(ex.Message);
        }

        private void Map_CameraIdle(object sender, EventArgs e)
        {
            if (_ignoreMove)
            {
                return;
            }

            var vm = Map.BindingContext as IMapViewModel;

            if (Map.VisibleRegion != null)
            {
                var mapCenter = Map.VisibleRegion.Center;

                if ((int)mapCenter.Latitude == 41 && (int)mapCenter.Longitude == 12)
                {
                    return;
                }

                GeographicLocation center = new GeographicLocation(mapCenter.Latitude, mapCenter.Longitude);

                var currentZoom = Map.VisibleRegion.Radius.Meters;

                if (vm.IsMapLayerAvailable || !vm.IsDraggablePointer)
                {
                    MoveMapCamera(center, currentZoom);
                }
            }
        }

        private void MoveMapCamera(GeographicLocation center, double zoom = 0)
        {
            var bounds = CoordinatesHelper.CalculateBounds(Map.VisibleRegion, true);

            MapCameraIdleEventArgs args = new MapCameraIdleEventArgs()
            {
                Center = center,
                Bounds = bounds,
                ZoomLevel = zoom,
            };

            MessagingCenter.Send(GlobalSetting.Instance, Core.Constants.MessageKeys.Map_CameraIdled, args);
        }

        private void OnPinClicked(object sender, GoogleMap.MarkerClickEventArgs e)
        {
            var marker = e.Marker;

            if (marker?.Id == _positionMarker?.Id)
            {
                e.Handled = true;
                return;
            }

            _ignoreMove = false;

            _prevMarker = e.Marker;

            Position position = new Position(e.Marker.Position.Latitude, e.Marker.Position.Longitude);

            var pin = GetPin(position);

            if (pin != null)
            {
                SetMarkerIcon(e.Marker, true);

                WorkloadListItem workloadListItem = pin.WorkloadItem;

                MessagingCenter.Send(GlobalSetting.Instance, Core.Constants.MessageKeys.MarkerTapped, workloadListItem);
            }

            e.Handled = true;
        }

        private void UnSelectPin()
        {
            if (_prevMarker != null)
            {
                try
                {
                    SetMarkerIcon(_prevMarker, false);
                }
                catch (System.Exception ex)
                {
                    LogHelper.LogException("OnPinClicked-Exception occured", ex);
                }
            }
        }

        private void UnSelectFeature()
        {
            if (_prevFeature != null)
            {
                try
                {
                    SetFeatureIcon(_prevFeature, false);
                }
                catch (System.Exception ex)
                {
                    LogHelper.LogException("UnSelectFeature-Exception occured", ex);
                }
            }
        }

        ExtendedPinDroid GetPin(Position position)
        {
            ExtendedPinDroid extendedPin = null;

            var pin = _extendedPins.FirstOrDefault(x => x.Position == position);

            if (pin != null)
            {
                extendedPin = (ExtendedPinDroid)pin;
            }

            return extendedPin;
        }

        public void SetExtent(IList<Pin> pins)
        {
            if (pins != null && pins.Any())
            {
                LatLngBounds.Builder builder = new LatLngBounds.Builder();

                foreach (var pin in pins)
                {
                    LatLng markerLatLng = new LatLng(pin.Position.Latitude, pin.Position.Longitude);

                    builder.Include(markerLatLng);
                }

                LatLngBounds bounds = builder.Build();

                MoveToBounds(bounds);
            }
        }

        public void SetDefaultExtent(MapExtent extent)
        {
            if (extent != null)
            {
                LatLngBounds.Builder builder = new LatLngBounds.Builder();

                LatLng minLatLng = new LatLng(extent.MinY, extent.MinX);
                LatLng maxLatLng = new LatLng(extent.MaxY, extent.MaxX);
                builder.Include(minLatLng);
                builder.Include(maxLatLng);

                LatLngBounds bounds = builder.Build();

                MoveToBounds(bounds);
            }
        }

        private void MoveToBounds(LatLngBounds bounds)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                _ignoreMove = true;

                try
                {
                    var latLngBounds = CameraUpdateFactory.NewLatLngBounds(bounds, MapExtent.DefaultPaddingDroid);

                    NativeMap?.MoveCamera(latLngBounds);

                    var vm = Map.BindingContext as IMapViewModel;

                    if (vm.IsMapLayerAvailable || !vm.IsDraggablePointer)
                    {
                        var location = new GeographicLocation(bounds.Center.Latitude, bounds.Center.Longitude);

                        var mapSpan = MapSpan.FromCenterAndRadius(location.ToFormMaps(), Distance.FromMeters(1000));

                        var calculatedBounds = CoordinatesHelper.CalculateBounds(mapSpan, true);

                        MapCameraIdleEventArgs args = new MapCameraIdleEventArgs()
                        {
                            Center = location,
                            Bounds = calculatedBounds,
                            ZoomLevel = mapSpan.Radius.Meters,
                        };

                        MessagingCenter.Send(GlobalSetting.Instance, Core.Constants.MessageKeys.Map_CameraIdled, args);
                    }
                }
                catch (System.Exception ex)
                {
                    LogHelper.LogException("MoveToBounds-Exception occured", ex);
                }

                _ignoreMove = false;
            });
        }

        private BitmapDescriptor GetCustomBitmapDescriptor(string text, float textSize, int resourceId, Android.Graphics.Color color, float customY, bool isBold)
        {
            if (string.IsNullOrEmpty(text))
            {
                text = string.Empty;
            }

            DisplayMetrics metrics = Context.Resources.DisplayMetrics;

            using (Typeface typeface = Typeface.DefaultBold)
            {
                using (Paint paint = new Paint(PaintFlags.AntiAlias))
                {
                    paint.Color = color;
                    paint.TextSize = textSize;
                    //paint.TextAlign = Paint.Align.Center;
                    paint.SetTypeface(typeface);
                    paint.FakeBoldText = isBold;

                    using (Android.Graphics.Rect bounds = new Android.Graphics.Rect())
                    {
                        using (Bitmap baseBitmap = BitmapFactory.DecodeResource(Resources, resourceId))
                        {
                            Bitmap bitmap = baseBitmap.Copy(Bitmap.Config.Argb8888, true);

                            paint.GetTextBounds(text, 0, text.Length, bounds);

                            float x = (float)(bitmap.Width - bounds.Width()) / 2;
                            float y;

                            if ((customY == 0f))
                            {
                                y = (float)(bitmap.Height + bounds.Height()) / 2;
                            }
                            else
                            {
                                y = metrics.Density * customY;
                            }

                            Canvas canvas = new Canvas(bitmap);

                            canvas.DrawText(text, x - bounds.Left, y - bounds.Bottom, paint);

                            BitmapDescriptor icon = BitmapDescriptorFactory.FromBitmap(bitmap);

                            return (icon);
                        }
                    }
                }
            }
        }
    }
}

