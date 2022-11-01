using System;
using System.Globalization;
using XamarinEnterpriseApp.Xamarin.Core.Extensions;
using Xamarin.Forms;

namespace XamarinEnterpriseApp.Xamarin.Core.Converters
{
    public class ImageSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string imageString = value as string;

            ImageSource imageSource = imageString?.ToImageSource();

            return imageSource;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}

