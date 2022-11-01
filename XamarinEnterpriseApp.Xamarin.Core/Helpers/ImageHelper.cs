using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using XamarinEnterpriseApp.Xamarin.Core.Converters;
using XamarinEnterpriseApp.Xamarin.Core.Models;
using XamarinEnterpriseApp.Xamarin.Core.Views;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;

namespace XamarinEnterpriseApp.Xamarin.Core.Helpers
{
    public static class ImageHelper
    {
        public static ImageSource FromBase64String(string base64String)
        {
            ImageSource imageSource = null;

            try
            {
                byte[] base64Stream = Convert.FromBase64String(base64String);
                imageSource = ImageSource.FromStream(() => new MemoryStream(base64Stream));


            }
            catch (Exception ex)
            {
                LogHelper.LogException("Exception occured while decoding the image", ex);
            }

            return imageSource;
        }

        public static BitmapDescriptor GetMarker(string iconName)
        {
            var assembly = typeof(ImageHelper).GetTypeInfo().Assembly;

            var mainDisplayInfo = DeviceDisplay.MainDisplayInfo;

            iconName = iconName.Replace(".png", string.Empty);

            switch (mainDisplayInfo.Density)
            {
                case 1:
                    iconName = $"{iconName}.png";
                    break;
                case 2:
                    iconName = $"{iconName}@2x.png";
                    break;
                case 3:
                    iconName = $"{iconName}@3x.png";
                    break;
                case 4:
                    iconName = $"{iconName}@3x.png";
                    break;
                default:
                    iconName = $"{iconName}@2x.png";
                    break;
            }

            var stream = assembly.GetManifestResourceStream($"XamarinEnterpriseApp.Xamarin.Core.Assets.{iconName}");
            return BitmapDescriptorFactory.FromStream(stream, id: iconName);
        }

        public static BitmapDescriptor GetMarkerFromView(WorkloadListItem item, bool isSelected = false)
        {
            MarkerView markerView = new MarkerView(item);
            markerView.SetProperties(isSelected);
            return BitmapDescriptorFactory.FromView(markerView, item.ReportItem.Statetype);
        }

        public static BitmapDescriptor GetFeatureFromView(GJFeature item, bool isSelected = false)
        {
            FeatureView featureView = new FeatureView();
            featureView.SetProperties(item, isSelected);
            return BitmapDescriptorFactory.FromView(featureView);
        }
    }
}
