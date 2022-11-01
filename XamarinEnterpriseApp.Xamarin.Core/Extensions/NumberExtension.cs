using System;
using System.Globalization;

namespace XamarinEnterpriseApp.Xamarin.Core.Extensions
{
    public static class NumberExtension
    {
        public static string FormatInvariant(this double value)
        {
            string formattedValue = string.Empty;

            formattedValue = value.ToString(CultureInfo.InvariantCulture);

            return formattedValue;
        }

        public static double FormatDouble(this string value)
        {
            double formattedValue = 0d;

            double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out formattedValue);

            return formattedValue;
        }

        public static string FormatNumberToDutch(this double doubleVal)
        {
            var language = "nl-NL";

            NumberFormatInfo nfi = new CultureInfo(language).NumberFormat;

            string formattedValue = doubleVal.ToString(nfi);

            return formattedValue;
        }
    }
}
