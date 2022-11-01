using System;
using FormMaps = Xamarin.Forms.Maps;
using GoogleMaps = Xamarin.Forms.GoogleMaps;

namespace XamarinEnterpriseApp.Xamarin.Core.Models
{
    public class GeographicLocation
    {
        public double Latitude { private set; get; }

        public double Longitude { private set; get; }

        public int Radious { set; get; }

        public object IsDraggablePointer { get; set; }

        public override string ToString()
        {
            return String.Format("{0}{1} {2}{3}", DMS(Math.Abs(Latitude)),
                                                  Latitude >= 0 ? "N" : "S",
                                                  DMS(Math.Abs(Longitude)),
                                                  Longitude >= 0 ? "E" : "W");
        }

        public bool IsValidCordinate
        {
            get
            {
                bool isInvalid = false;

                if (Latitude == 0 || Longitude == 0)
                {
                    //NL data specific
                    isInvalid = true;
                }
                else if (Latitude > 90 || Longitude > 180)
                {
                    isInvalid = true;
                }
                else if (Latitude < -90 || Longitude < -180)
                {
                    isInvalid = true;
                }

                return !isInvalid;
            }
        }

        public bool UpdateMap { get; set; }
        public bool IsUserLocation { get; set; }
        public DateTime UpdatedTime { get; set; }

        string DMS(double decimalDegrees)
        {
            int degrees = (int)decimalDegrees;
            decimalDegrees -= degrees;
            decimalDegrees *= 60;
            int minutes = (int)decimalDegrees;
            decimalDegrees -= minutes;
            decimalDegrees *= 60;
            int seconds = (int)Math.Round(decimalDegrees);

            // Fix rounding issues.
            if (seconds == 60)
            {
                seconds = 0;
                minutes += 1;

                if (minutes == 60)
                {
                    minutes = 0;
                    degrees += 1;
                }
            }

            return String.Format("{0}°{1}′{2}″", degrees, minutes, seconds);
        }

        public GeographicLocation(double latitude, double longitude)
        {
            // Normalize values.
            Latitude = latitude % 90;
            Longitude = longitude % 180;
        }

        public FormMaps.Position ToFormMaps()
        {
            return new FormMaps.Position(Latitude, Longitude);
        }

        public GoogleMaps.Position ToGoogleMaps()
        {
            return new GoogleMaps.Position(Latitude, Longitude);
        }

        public Coordinates ToCoordinates()
        {
            return new Coordinates(Latitude, Longitude);
        }

        public static GeographicLocation GetDefaultLocation()
        {
            GeographicLocation geographicLocation;

            var mapExtent = GlobalSetting.Instance.MGAppConfig?.Extent;

            if (mapExtent != null)
            {
                geographicLocation = mapExtent.GetMiddleLocation();
            }
            else
            {
                geographicLocation = new GeographicLocation(52.417935, 4.894812);
            }

            return geographicLocation;
        }
    }
}
