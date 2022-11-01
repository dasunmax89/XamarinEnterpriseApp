using System.Collections.Concurrent;
using UIKit;
using Xamarin.Forms.GoogleMaps;
using Xamarin.Forms.GoogleMaps.iOS.Factories;

namespace XamarinEnterpriseApp.Xamarin.iOS.Helpers
{
    public class CachingImageFactory : IImageFactory
    {
        private readonly ConcurrentDictionary<string, UIImage> _cache
            = new ConcurrentDictionary<string, UIImage>();

        public UIImage ToUIImage(BitmapDescriptor descriptor)
        {
            var defaultFactory = DefaultImageFactory.Instance;

            UIImage uIImage;

            if (!string.IsNullOrEmpty(descriptor.Id))
            {
                var cacheEntry = _cache.GetOrAdd(descriptor.Id, _ => defaultFactory.ToUIImage(descriptor));

                uIImage = cacheEntry;
            }

            uIImage = defaultFactory.ToUIImage(descriptor);

            return uIImage;
        }
    }
}
