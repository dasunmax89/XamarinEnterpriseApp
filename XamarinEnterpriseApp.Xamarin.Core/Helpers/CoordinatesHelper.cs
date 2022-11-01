using System;
using XamarinEnterpriseApp.Xamarin.Core.Models;
using FM = Xamarin.Forms.Maps;
using GM = Xamarin.Forms.GoogleMaps;

namespace XamarinEnterpriseApp.Xamarin.Core.Helpers
{
    public static class CoordinatesHelper
    {
        // The city “Amsterfoort” is used as reference “Rijksdriehoek” coordinate.
        private const double referenceRdX = 155000;
        private const double referenceRdY = 463000;
        // The city “Amsterfoort” is used as reference “WGS84” coordinate.

        private const double referenceWgs84Lat = 52.15517440;
        private const double referenceWgs84Lon = 5.38720621;

        public static Coordinates Wgs84ToRD(double wgs84_longitude, double wgs84_lattitude)
        {
            var Rpq = new double[4, 5];

            Rpq[0, 1] = 190094.945;
            Rpq[1, 1] = -11832.228;
            Rpq[2, 1] = -114.221;
            Rpq[0, 3] = -32.391;
            Rpq[1, 0] = -0.705;
            Rpq[3, 1] = -2.340;
            Rpq[0, 2] = -0.008;
            Rpq[1, 3] = -0.608;
            Rpq[2, 3] = 0.148;

            var Spq = new double[4, 5];
            Spq[0, 1] = 0.433;
            Spq[0, 2] = 3638.893;
            Spq[0, 4] = 0.092;
            Spq[1, 0] = 309056.544;
            Spq[2, 0] = 73.077;
            Spq[1, 2] = -157.984;
            Spq[3, 0] = 59.788;
            Spq[2, 2] = -6.439;
            Spq[1, 1] = -0.032;
            Spq[1, 4] = -0.054;

            var deltaX = wgs84_lattitude - referenceWgs84Lat;
            var deltaY = wgs84_longitude - referenceWgs84Lon;
            var d_lattitude = (0.36 * deltaX);
            var d_longitude = (0.36 * deltaY);

            double calc_latt = 0;
            double calc_long = 0;

            for (int p = 0; p < 4; p++)
            {
                for (int q = 0; q < 5; q++)
                {
                    calc_latt += Rpq[p, q] * Math.Pow(d_lattitude, p) * Math.Pow(d_longitude, q);
                    calc_long += Spq[p, q] * Math.Pow(d_lattitude, p) * Math.Pow(d_longitude, q);
                }
            }

            var rd_x_coordinate = (referenceRdX + calc_latt);
            var rd_y_coordinate = (referenceRdY + calc_long);

            var location = new Coordinates(rd_y_coordinate, rd_x_coordinate);

            return location;
        }

        public static bool ValidateLatLng(Coordinates latLong)
        {
            bool isValid = false;

            if (latLong.Lat >= -90 && latLong.Lat <= 90 &&
                latLong.Lon >= -180 && latLong.Lon <= 180)
            {
                isValid = true;
            }

            return isValid;
        }

        public static Coordinates RDToWgs84(double x, double y)
        {
            var Kp = new[] { 0, 2, 0, 2, 0, 2, 1, 4, 2, 4, 1 };
            var Kq = new[] { 1, 0, 2, 1, 3, 2, 0, 0, 3, 1, 1 };
            var Kpq = new[] { 3235.65389, -32.58297, -0.24750, -0.84978, -0.06550, -0.01709, -0.00738, 0.00530, -0.00039, 0.00033, -0.00012 };

            var Lp = new[] { 1, 1, 1, 3, 1, 3, 0, 3, 1, 0, 2, 5 };
            var Lq = new[] { 0, 1, 2, 0, 3, 1, 1, 2, 4, 2, 0, 0 };
            var Lpq = new[] { 5260.52916, 105.94684, 2.45656, -0.81885, 0.05594, -0.05607, 0.01199, -0.00256, 0.00128, 0.00022, -0.00022, 0.00026 };

            var dX = 1E-5 * (x - referenceRdX);
            var dY = 1E-5 * (y - referenceRdY);


            double phi = 0.0;
            double lam = 0.0;

            for (int k = 0; k < Kpq.Length; k++)
            {
                phi = phi + (Kpq[k] * Math.Pow(dX, Kp[k]) * Math.Pow(dY, Kq[k]));
            }

            phi = referenceWgs84Lat + phi / 3600;

            for (int l = 0; l < Lpq.Length; l++)
            {
                lam = lam + (Lpq[l] * Math.Pow(dX, Lp[l]) * Math.Pow(dY, Lq[l]));
            }

            lam = referenceWgs84Lon + lam / 3600;

            var location = new Coordinates(phi, lam);

            if ((x != 0) && (y != 0))
            {

            }
            else
            {

            }

            return location;

        }

        public static GeographicBounds CalculateBounds(FM.MapSpan region, bool convertToRd)
        {
            GeographicBounds bounds = new GeographicBounds();

            var center = region.Center;
            var halfheightDegrees = region.LatitudeDegrees / 2;
            var halfwidthDegrees = region.LongitudeDegrees / 2;

            var left = center.Longitude - (1 * halfwidthDegrees);
            var right = center.Longitude + (1 * halfwidthDegrees);
            var top = center.Latitude + (1 * halfheightDegrees);
            var bottom = center.Latitude - (1 * halfheightDegrees);

            if (left < -180) left = 180 + (180 + left);
            if (right > 180) right = (right - 180) - 180;

            if (convertToRd)
            {
                var southWest = Wgs84ToRD(left, bottom);

                var northEast = Wgs84ToRD(right, top);

                bounds.MinX = southWest.Lon;
                bounds.MaxX = northEast.Lon;
                bounds.MaxY = northEast.Lat;
                bounds.MinY = southWest.Lat;
            }
            else
            {
                bounds.MinX = left;
                bounds.MaxX = right;
                bounds.MaxY = top;
                bounds.MinY = bottom;
            }

            return bounds;
        }

        public static GeographicBounds CalculateBounds(GM.MapRegion region, bool convertToRd)
        {
            GeographicBounds bounds = new GeographicBounds();

            var left = region.NearLeft.Longitude;
            var right = region.FarRight.Longitude;
            var top = region.FarRight.Latitude;
            var bottom = region.NearLeft.Latitude;

            if (convertToRd)
            {
                var southWest = Wgs84ToRD(left, bottom);

                var northEast = Wgs84ToRD(right, top);

                bounds.MinX = southWest.Lon;
                bounds.MaxX = northEast.Lon;
                bounds.MaxY = northEast.Lat;
                bounds.MinY = southWest.Lat;
            }
            else
            {
                bounds.MinX = left;
                bounds.MaxX = right;
                bounds.MaxY = top;
                bounds.MinY = bottom;
            }

            return bounds;
        }

        public static GeographicBounds CalculateBoundsFromCenter(GeographicLocation center, bool convertToRd)
        {
            GeographicBounds bounds = new GeographicBounds();

            double longitudeDegrees = 0.00786121934652284d;
            double latitudeDegrees = 0.00658867401994456d;

            var halfheightDegrees = latitudeDegrees / 2;
            var halfwidthDegrees = longitudeDegrees / 2;

            var left = center.Longitude - (1 * halfwidthDegrees);
            var right = center.Longitude + (1 * halfwidthDegrees);
            var top = center.Latitude + (1 * halfheightDegrees);
            var bottom = center.Latitude - (1 * halfheightDegrees);

            if (left < -180) left = 180 + (180 + left);
            if (right > 180) right = (right - 180) - 180;

            if (convertToRd)
            {
                var southWest = Wgs84ToRD(left, bottom);

                var northEast = Wgs84ToRD(right, top);

                bounds.MinX = southWest.Lon;
                bounds.MaxX = northEast.Lon;
                bounds.MaxY = northEast.Lat;
                bounds.MinY = southWest.Lat;
            }
            else
            {
                bounds.MinX = left;
                bounds.MaxX = right;
                bounds.MaxY = top;
                bounds.MinY = bottom;
            }

            return bounds;
        }
    }
}
