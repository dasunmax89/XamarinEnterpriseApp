using System;
using XamarinEnterpriseApp.Xamarin.Core.Helpers;
using Newtonsoft.Json;

namespace XamarinEnterpriseApp.Xamarin.Core.Models
{
    public class MapExtent
    {
        [JsonProperty("MaxX")]
        public double MaxX { get; set; }

        [JsonProperty("MaxY")]
        public double MaxY { get; set; }

        [JsonProperty("MinX")]
        public double MinX { get; set; }

        [JsonProperty("MinY")]
        public double MinY { get; set; }

        [JsonProperty("LonLatMin")]
        public object LonLatMin { get; set; }

        [JsonProperty("LonLatMax")]
        public object LonLatMax { get; set; }

        [JsonProperty("CoordinatesMin")]
        public Coordinates CoordinatesMin { get; set; }

        [JsonProperty("CoordinatesMax")]
        public Coordinates CoordinatesMax { get; set; }

        [JsonIgnore]
        public const int DefaultPaddingDroid = 50;

        [JsonIgnore]
        public const int DefaultPaddinIOs = 50;

        public static MapExtent GetDefault()
        {
            var minLatLng = GeographicLocation.GetDefaultLocation();

            var maxLatLng = GeographicLocation.GetDefaultLocation();

            MapExtent mapExtent = new MapExtent()
            {
                MinX = minLatLng.Longitude,
                MinY = minLatLng.Latitude,
                MaxY = maxLatLng.Latitude,
                MaxX = maxLatLng.Longitude,
            };

            return mapExtent;
        }

        public MapExtent GetFromCoordinates()
        {
            var minLatLng = CoordinatesHelper.RDToWgs84(CoordinatesMin.Lon, CoordinatesMin.Lat);

            var maxLatLng = CoordinatesHelper.RDToWgs84(CoordinatesMax.Lon, CoordinatesMax.Lat);

            MapExtent mapExtent = null;

            if (minLatLng != null && maxLatLng != null &&
                CoordinatesHelper.ValidateLatLng(minLatLng) &&
                CoordinatesHelper.ValidateLatLng(maxLatLng))
            {
                mapExtent = new MapExtent()
                {
                    MinX = minLatLng.Lon,
                    MinY = minLatLng.Lat,
                    MaxY = maxLatLng.Lat,
                    MaxX = maxLatLng.Lon,
                };
            }

            return mapExtent;
        }

        public GeographicLocation GetMiddleLocation()
        {
            var lat = (MinY + MaxY) / 2;

            var lon = (MinX + MaxX) / 2;

            GeographicLocation geographicLocation = new GeographicLocation(lat, lon);

            return geographicLocation;
        }

        public MapExtent ToWGSCoordinates()
        {
            var minLatLng = CoordinatesHelper.RDToWgs84(MinX, MinY);

            var maxLatLng = CoordinatesHelper.RDToWgs84(MaxX, MaxY);

            MapExtent mapExtent = null;

            if (minLatLng != null && maxLatLng != null &&
               CoordinatesHelper.ValidateLatLng(minLatLng) &&
               CoordinatesHelper.ValidateLatLng(maxLatLng))
            {
                mapExtent = new MapExtent()
                {
                    MinX = minLatLng.Lon,
                    MinY = minLatLng.Lat,
                    MaxY = maxLatLng.Lat,
                    MaxX = maxLatLng.Lon,
                };
            }

            return mapExtent;
        }
    }
}
