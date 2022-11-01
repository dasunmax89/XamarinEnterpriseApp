using System;
using XamarinEnterpriseApp.Xamarin.Core.Converters.JsonConverters;
using Newtonsoft.Json;
using FormMaps = Xamarin.Forms.Maps;
using GoogleMaps = Xamarin.Forms.GoogleMaps;

namespace XamarinEnterpriseApp.Xamarin.Core.Models
{
    public class Coordinates
    {
        [JsonProperty("Lon")]
        [JsonConverter(typeof(ParseDoubleConverter))]
        public double Lon { get; set; }

        [JsonProperty("Lat")]
        [JsonConverter(typeof(ParseDoubleConverter))]
        public double Lat { get; set; }

        public Coordinates()
        {

        }

        public Coordinates(double lat, double lon)
        {
            Lat = lat;
            Lon = lon;
        }

        public FormMaps.Position ToFormMaps()
        {
            return new FormMaps.Position(Lat, Lon);
        }

        public GoogleMaps.Position ToGoogleMaps()
        {
            return new GoogleMaps.Position(Lat, Lon);
        }

        public GeographicLocation ToGeographicLocation()
        {
            var location = new GeographicLocation(Lat, Lon);
            location.Radious = 100;
            return location;
        }
    }
}
