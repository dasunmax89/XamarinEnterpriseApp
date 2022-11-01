using System;
using System.Diagnostics;
using System.Threading.Tasks;
using XamarinEnterpriseApp.Xamarin.Core.Ioc;
using XamarinEnterpriseApp.Xamarin.Core.Models;
using XamarinEnterpriseApp.Xamarin.Core.Services;
using XamarinEnterpriseApp.Xamarin.Core.ViewModels;
using XamarinEnterpriseApp.Xamarin.Core.ViewModels.Base;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace XamarinEnterpriseApp.Xamarin.Core.Controls
{
    public class ExtendedMapDroid : Map
    {
        private readonly IGeoLocationService _geoLocationService;

        public static readonly BindableProperty MapPositionProperty =
        BindableProperty.Create("MapPosition",
                                typeof(GeographicLocation),
                                typeof(ExtendedMapDroid),
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

        public ExtendedMapDroid()
        {
            _geoLocationService = DependencyResolver.Resolve<IGeoLocationService>();
            PropertyChanged += ExtendedMap_PropertyChanged;
            this.SetValue(AutomationProperties.IsInAccessibleTreeProperty, false);
        }

        private void ExtendedMap_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(MapPosition))
            {
                SetCenter(MapPosition);
            }
            else if (e.PropertyName == nameof(VisibleRegion))
            {
                var vm = BindingContext as IMapViewModel;

                vm.LocationCurrentState = 2;
            }
        }

        public void SetCenter(GeographicLocation location)
        {
            Position position = location.ToFormMaps();

            double radious = 100;

            if (location.Radious > 0)
            {
                radious = location.Radious;
            }
            else if (VisibleRegion != null && VisibleRegion.Radius.Meters < 600)
            {
                radious = VisibleRegion.Radius.Meters;
            }

            MoveToRegion(MapSpan.FromCenterAndRadius(
                    position,
                    Distance.FromMeters(radious)));
        }
    }
}
