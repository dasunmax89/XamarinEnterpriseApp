using System.Linq;
using XamarinEnterpriseApp.Xamarin.Core.Extensions;
using XamarinEnterpriseApp.Xamarin.Core.Helpers;
using XamarinEnterpriseApp.Xamarin.Core.Models;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;

namespace XamarinEnterpriseApp.Xamarin.Core.Controls
{
    public class MDGeometryRenderer
    {
        private ExtendedMapIOS _nativeMap;
        private GeoReportsDataResponse _geoReportsData;

        public MDGeometryRenderer(ExtendedMapIOS nativeMap, GeoReportsDataResponse geoReportsData)
        {
            _nativeMap = nativeMap;
            _geoReportsData = geoReportsData;
        }

        public void Render()
        {
            if (_nativeMap != null && _geoReportsData != null && !_geoReportsData.Features.IsEmpty())
            {
                int index = 0;

                foreach (var item in _geoReportsData.Features)
                {
                    string featureId = item.GetId();
                    string caption = item.GetCaption();

                    bool isSelected = featureId == _geoReportsData.SelectedFeatureId || featureId == _geoReportsData.LinkedFeatureId;

                    if (item.Coordinates != null)
                    {
                        var icon = ImageHelper.GetFeatureFromView(item, isSelected);

                        Pin pin = new Pin()
                        {
                            Position = item.Coordinates.ToGoogleMaps(),
                            Address = item.Properties?.Street,
                            Label = index++.ToString(),
                            Tag = item,
                            Icon = icon,
                            IsVisible = true,
                        };

                        pin.SetValue(AutomationProperties.IsInAccessibleTreeProperty, true);
                        pin.SetValue(AutomationProperties.HelpTextProperty, caption);

                        _nativeMap.Pins.Add(pin);
                    }
                }
            }
        }

        public void Clear()
        {
            if (_nativeMap != null)
            {
                _nativeMap.Pins.Clear();
            }
        }
    }
}
