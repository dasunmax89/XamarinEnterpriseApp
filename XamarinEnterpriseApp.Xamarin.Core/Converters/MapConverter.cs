using System;
using System.Globalization;
using XamarinEnterpriseApp.Xamarin.Core.Helpers;
using XamarinEnterpriseApp.Xamarin.Core.Models;
using Xamarin.Forms;
namespace XamarinEnterpriseApp.Xamarin.Core.Converters
{
    public class MapIconConverter : IValueConverter
    {
        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            WorkloadListItem item = value as WorkloadListItem;

            if (item != null)
            {
                return ImageHelper.GetMarkerFromView(item);
            }
            else
            {
                return null;
            }
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }

    public class GMapsPositionConverter : IValueConverter
    {
        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                GeographicLocation location = (GeographicLocation)value;

                return location.ToGoogleMaps();
            }
            else
            {
                return null;
            }
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }

    public class FormMapsPositionConverter : IValueConverter
    {
        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                GeographicLocation location = (GeographicLocation)value;

                return location.ToFormMaps();
            }
            else
            {
                return null;
            }
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
