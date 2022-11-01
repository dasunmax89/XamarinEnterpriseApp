using System;
using System.Globalization;
using Android.Graphics;
using Android.Util;
using Com.Google.Maps.Android.Data.Geojson;

namespace XamarinEnterpriseApp.Xamarin.Droid.Extensions
{
    public static class PlatformExtensions
    {
        public static string GetProperty(this GeoJsonFeature feature, string idKey)
        {
            string id = string.Empty;

            if (feature != null)
            {
                if (feature.HasProperty(idKey))
                {
                    id = feature.GetProperty(idKey);
                }
            }

            return id;
        }

        public static Bitmap ToBitmap(this string base64String)
        {
            byte[] imageAsBytes = Base64.Decode(base64String, Base64Flags.Default);

            return BitmapFactory.DecodeByteArray(imageAsBytes, 0, imageAsBytes.Length);
        }
    }
}
