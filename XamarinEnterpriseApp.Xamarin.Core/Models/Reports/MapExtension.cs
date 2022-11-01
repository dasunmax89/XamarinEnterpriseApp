using System;
using System.Globalization;
using System.Text;
using XamarinEnterpriseApp.Xamarin.Core.Constants;
using Xamarin.Essentials;

namespace XamarinEnterpriseApp.Xamarin.Core.Extensions
{
    public static class MapExtension
    {
        public static string GetPlaceName(this Placemark value)
        {
            string locationCity = string.Empty;

            if (value != null)
            {
                if (!string.IsNullOrEmpty(value.Locality))
                {
                    locationCity = value.Locality;
                }
                else if (!string.IsNullOrEmpty(value.SubLocality))
                {
                    locationCity = value.SubLocality;
                }
                else if (!string.IsNullOrEmpty(value.SubAdminArea))
                {
                    locationCity = value.SubAdminArea;
                }
                else if (!string.IsNullOrEmpty(value.AdminArea))
                {
                    locationCity = value.AdminArea;
                }
                else
                {
                    locationCity = value.CountryName;
                }
            }

            return locationCity;
        }

        public static string GetLocationDescription(this Placemark value)
        {
            string locationName = string.Empty;

            if (value != null)
            {
                if (!string.IsNullOrEmpty(value.SubThoroughfare))
                {
                    if (!value.SubThoroughfare.Search(value.Thoroughfare))
                    {
                        locationName = $"{value.SubThoroughfare},{value.Thoroughfare}";
                    }
                    else
                    {
                        locationName = $"{value.SubThoroughfare},{value.AdminArea}";
                    }
                }
                else
                {
                    locationName = $"{value.SubThoroughfare},{value.AdminArea}";
                }
            }

            return locationName;
        }

        public static string GetLocationInfo(this Placemark value)
        {
            StringBuilder locationName = new StringBuilder();

            if (value != null)
            {
                if (!string.IsNullOrEmpty(value.Thoroughfare))
                {
                    locationName.Append($"{value.Thoroughfare}");
                }

                if (!string.IsNullOrEmpty(value.SubThoroughfare))
                {
                    locationName.Append(StringConstants.Space);
                    locationName.Append($"{value.SubThoroughfare}");
                }

                if ((!string.IsNullOrEmpty(value.Thoroughfare) || !string.IsNullOrEmpty(value.SubThoroughfare)) && !string.IsNullOrEmpty(value.Locality))
                {
                    locationName.Append(StringConstants.Comma);
                    locationName.Append(StringConstants.Space);
                }

                if (!string.IsNullOrEmpty(value.Locality))
                {
                    locationName.Append($"{value.Locality}");
                }
            }

            return locationName.ToString();
        }

        public static string GetStreetName(this Placemark value)
        {
            string locationName = string.Empty;

            if (value != null)
            {
                locationName = $"{value.Thoroughfare}";
            }

            return locationName;
        }

        public static string GetHouseNumber(this Placemark value)
        {
            string houseNo = string.Empty;

            if (value != null)
            {
                houseNo = $"{value.SubThoroughfare}";
            }

            return houseNo;
        }

        public static string GetCityName(this Placemark value)
        {
            string cityName = string.Empty;

            if (value != null)
            {
                cityName = $"{value.Locality}";
            }

            return cityName;
        }
    }
}
