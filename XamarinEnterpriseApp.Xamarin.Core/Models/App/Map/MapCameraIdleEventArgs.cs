using System;
using Xamarin.Forms;

namespace XamarinEnterpriseApp.Xamarin.Core.Models
{
    public class MapCameraIdleEventArgs
    {
        public const int AndroidZoomRadious = 150;

        public GeographicLocation Center { get; set; }

        public GeographicBounds Bounds { get; set; }

        public double ZoomLevel { get; set; }

        public bool ValidateDetailLoadingZoomLevel(int iOSZoom, int androidZoom)
        {
            bool isLoading = Device.RuntimePlatform == Device.iOS ? ZoomLevel >= iOSZoom : ZoomLevel <= androidZoom;

            return isLoading;
        }
    }
}
