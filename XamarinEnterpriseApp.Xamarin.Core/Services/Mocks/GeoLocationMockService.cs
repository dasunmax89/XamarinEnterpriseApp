using System;
using System.Threading.Tasks;
using XamarinEnterpriseApp.Xamarin.Core.Models;
using XamarinEnterpriseApp.Xamarin.Core.Views;
using Xamarin.Essentials;

namespace XamarinEnterpriseApp.Xamarin.Core.Services.Mocks
{
    public class GeoLocationMockService : IGeoLocationService
    {
        public void ConfigureSearchBar(PlacesBar search_Bar)
        {

        }

        public Task<global::Xamarin.Essentials.Location> GetAddressPlacemark(string address)
        {
            return Task.FromResult(new global::Xamarin.Essentials.Location());
        }

        public Task<Placemark> GetLocationPlacemark(double lat, double lon)
        {
            return Task.FromResult(new Placemark());
        }

        public Task<Place> GetPlace(string placeID, bool isNationaalGeoRegisterAPI ,string apiKey, PlacesFieldList fields = null)
        {
            return Task.FromResult(new Place());
        }

        public Task<AutoCompleteResult> GetPlaces(string newTextValue, string apiKey, LocationBias bias, Components components, PlaceType type, GoogleAPILanguage language)
        {
            return Task.FromResult(new AutoCompleteResult());
        }

        public Task<AutoCompleteResult> GetPlaces(string newTextValue)
        {
            return Task.FromResult(new AutoCompleteResult());
        }

        public Task NavigateToLocation(GeographicLocation geographicLocation, string packageName)
        {
            return Task.FromResult(true);
        }
    }
}
