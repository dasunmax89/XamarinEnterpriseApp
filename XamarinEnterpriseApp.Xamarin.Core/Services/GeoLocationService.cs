using System;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using XamarinEnterpriseApp.Xamarin.Core.Constants;
using XamarinEnterpriseApp.Xamarin.Core.Dependency;
using XamarinEnterpriseApp.Xamarin.Core.Helpers;
using XamarinEnterpriseApp.Xamarin.Core.Models;
using XamarinEnterpriseApp.Xamarin.Core.Views;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xamarin.Essentials;
using Xamarin.Forms;
using XE = Xamarin.Essentials;

namespace XamarinEnterpriseApp.Xamarin.Core.Services
{
    public interface IGeoLocationService
    {
        Task<XE.Placemark> GetLocationPlacemark(double lat, double lon);
        void ConfigureSearchBar(PlacesBar search_Bar);
        Task<Place> GetPlace(string placeID, bool isNationaalGeoRegisterAPI, string apiKey = null, PlacesFieldList fields = null);
        Task<AutoCompleteResult> GetPlaces(string newTextValue, string apiKey, LocationBias bias, Components components, PlaceType type, GoogleAPILanguage language);
        Task<AutoCompleteResult> GetPlaces(string newTextValue);

        Task NavigateToLocation(GeographicLocation geographicLocation, string packageName);
        Task<XE.Location> GetAddressPlacemark(string address);
    }

    public class GeoLocationService : IGeoLocationService
    {
        private readonly IDialogService _dialogService;
        private readonly ISettingsService _settingsService;

        public GeoLocationService(IDialogService dialogService, ISettingsService settingsService)
        {
            _dialogService = dialogService;
            _settingsService = settingsService;
        }

        public void ConfigureSearchBar(PlacesBar search_Bar)
        {
            search_Bar.ApiKey = ApiConstants.GooglePlacesApiKey;
            search_Bar.Type = PlaceType.All;
            search_Bar.Components = new Components("country:nl");
            search_Bar.MinimumSearchText = 2;
        }

        public async Task<XE.Placemark> GetLocationPlacemark(double lat, double lon)
        {
            XE.Placemark placemark;

            try
            {
                var placemarksResult = await XE.Geocoding.GetPlacemarksAsync(lat, lon);

                var placemarksList = placemarksResult?.ToList();

                placemark = placemarksList?.FirstOrDefault();

                if (placemark == null)
                {
                    placemark = GetDefaultPlacemark(lat, lon);
                }
            }
            catch (Exception ex)
            {
                placemark = GetDefaultPlacemark(lat, lon);

                LogHelper.LogException("Exception occurred in GetLocationPlacemark", ex);
            }

            return placemark;
        }

        public async Task<XE.Location> GetAddressPlacemark(string address)
        {
            XE.Location placemark = null;

            try
            {
                var placemarksResult = await XE.Geocoding.GetLocationsAsync(address);

                var placemarksList = placemarksResult?.ToList();

                placemark = placemarksList?.FirstOrDefault();

                if (placemark == null)
                {

                }
            }
            catch (Exception ex)
            {
                LogHelper.LogException("Exception occurred in GetAddressPlacemark", ex);
            }

            return placemark;
        }


        private XE.Placemark GetDefaultPlacemark(double lat, double lon)
        {
            XE.Placemark placemark = new XE.Placemark();
            placemark.SubThoroughfare = $"{lat},{lon}";
            return placemark;
        }

        /// <summary>
		/// Gets the place.
		/// </summary>
		/// <returns>The place.</returns>
		/// <param name="placeID">Place identifier.</param>
		/// <param name="isNationaalGeoRegisterAPI">NationaalGeoRegister API indicator.</param>
        /// <param name="apiKey">API key.</param>
		/// <param name="fields">The fields to query (see https://developers.google.com/places/web-service/details#fields )</param>
		public async Task<Place> GetPlace(string placeID, bool isNationaalGeoRegisterAPI, string apiKey = null, PlacesFieldList fields = null)
        {
            fields = fields ?? PlacesFieldList.ALL; // default = ALL fields

            try
            {
                var requestURI = string.Empty;

                if (isNationaalGeoRegisterAPI)
                {
                    requestURI = CreateNationaalGeoRegisterDetailsRequestUri(placeID);
                }
                else
                {
                    requestURI = CreateDetailsRequestUri(placeID, apiKey, fields);
                }

                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Get, requestURI);
                var response = await client.SendAsync(request);

                if (!response.IsSuccessStatusCode)
                {
                    await _dialogService.ShowToast("PlacesBar HTTP request denied.");
                    return null;
                }

                var result = await response.Content.ReadAsStringAsync();

                JObject jObject = (JObject)JsonConvert.DeserializeObject(result);

                PlacesQueryResult queryResult = await Task.Run(() =>
                jObject.ToObject<PlacesQueryResult>());

                if (!string.IsNullOrEmpty(queryResult?.ErrorMessage))
                {
                    await _dialogService.ShowToast(queryResult.ErrorMessage);
                    return null;
                }

                return new Place(JObject.Parse(result), isNationaalGeoRegisterAPI);
            }
            catch (Exception ex)
            {
                await _dialogService.ShowToast($"PlacesBar HTTP issue: {ex.Message}, {ex}");
                return null;
            }
        }

        /// <summary>
        /// Creates the details request URI.
        /// </summary>
        /// <returns>The details request URI.</returns>
        /// <param name="place_id">Place identifier.</param>
        /// <param name="apiKey">API key.</param>
        /// <param name="fields">The fields to query (see https://developers.google.com/places/web-service/details#fields )</param>
        private string CreateDetailsRequestUri(string place_id, string apiKey, PlacesFieldList fields)
        {
            var url = "https://maps.googleapis.com/maps/api/place/details/json";
            url += $"?placeid={Uri.EscapeUriString(place_id)}";
            url += $"&key={apiKey}";
            if (!fields.IsEmpty()) url += $"&fields={fields}";
            return url;
        }


        /// <summary>
        /// Creates the Nationaal Geo Register details request URI.
        /// </summary>
        /// <returns>The details request URI.</returns>
        /// <param name="placeID">Search String</param>
        private string CreateNationaalGeoRegisterDetailsRequestUri(string placeID)
        {
            var url = "https://geodata.nationaalgeoregister.nl/locatieserver/v3/lookup?wt=json";
            url += $"?&id={Uri.EscapeUriString(placeID)}";
            return url;
        }

        /// <summary>
        /// Calls the Nationaal Geo Register API to retrieve autofill suggestions
        /// </summary>
        /// <returns>The places.</returns>
        /// <param name="newTextValue">New text value.</param>
        public async Task<AutoCompleteResult> GetPlaces(string newTextValue)
        {
            try
            {
                var requestURI = CreateNationaalGeoRegisterPredictionsUri(newTextValue);
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Get, requestURI);
                var response = await client.SendAsync(request);

                if (!response.IsSuccessStatusCode)
                {
                    await _dialogService.ShowToast("PlacesBar HTTP request denied.");
                    return null;
                }

                var result = await response.Content.ReadAsStringAsync();

                JObject jObject = (JObject)JsonConvert.DeserializeObject(result);

                PlacesQueryResult queryResult = await Task.Run(() =>
                jObject.ToObject<PlacesQueryResult>());

                if (!string.IsNullOrEmpty(queryResult?.ErrorMessage))
                {
                    await _dialogService.ShowToast(queryResult.ErrorMessage);
                    return null;
                }

                return AutoCompleteResult.FromJson(JObject.Parse(result));
            }
            catch (Exception ex)
            {
                await _dialogService.ShowToast($"PlacesBar HTTP issue: {ex.Message}, {ex}");
                return null;
            }
        }

        /// <summary>
        /// Calls the Google Places API to retrieve autofill suggestions
        /// </summary>
        /// <returns>The places.</returns>
        /// <param name="newTextValue">New text value.</param>
        /// <param name="apiKey">The API key</param>
        /// <param name="bias">The location bias (can be NULL)</param>
        /// <param name="components">The components (can be NULL)</param>
        /// <param name="type">Filter for the returning types </param>
        /// <param name="language">The language of the results</param>
        public async Task<AutoCompleteResult> GetPlaces(string newTextValue, string apiKey, LocationBias bias, Components components, PlaceType type, GoogleAPILanguage language)
        {
            if (string.IsNullOrEmpty(apiKey))
            {
                throw new Exception("You have not assigned a Google API key to PlacesBar");
            }

            try
            {
                var requestURI = CreatePredictionsUri(newTextValue, apiKey, bias, components, type, language);
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Get, requestURI);
                var response = await client.SendAsync(request);

                if (!response.IsSuccessStatusCode)
                {
                    await _dialogService.ShowToast("PlacesBar HTTP request denied.");
                    return null;
                }

                var result = await response.Content.ReadAsStringAsync();

                JObject jObject = (JObject)JsonConvert.DeserializeObject(result);

                PlacesQueryResult queryResult = await Task.Run(() =>
                jObject.ToObject<PlacesQueryResult>());

                if (!string.IsNullOrEmpty(queryResult?.ErrorMessage))
                {
                    await _dialogService.ShowToast(queryResult.ErrorMessage);
                    return null;
                }

                return AutoCompleteResult.FromJson(JObject.Parse(result));
            }
            catch (Exception ex)
            {
                await _dialogService.ShowToast($"PlacesBar HTTP issue: {ex.Message}, {ex}");
                return null;
            }
        }

        /// <summary>
        /// Creates the predictions URI.
        /// </summary>
        /// <returns>The predictions URI.</returns>
        /// <param name="newTextValue">New text value.</param>
        /// <param name="apiKey">The API key</param>
        /// <param name="bias">The location bias (can be NULL)</param>
        /// <param name="components">The components (can be NULL)</param>
        /// <param name="type">Filter for the returning types </param>
        /// <param name="language">The language of the results</param>
        private string CreatePredictionsUri(string newTextValue, string apiKey, LocationBias bias, Components components, PlaceType type, GoogleAPILanguage language)
        {
            var url = "https://maps.googleapis.com/maps/api/place/autocomplete/json";
            var input = Uri.EscapeUriString(newTextValue);
            var pType = PlaceTypeValue(type);

            var constructedUrl = $"{url}?input={input}&types={pType}&key={apiKey}";

            if (bias != null)
                constructedUrl = constructedUrl + bias;

            if (components != null)
                constructedUrl += components;

            if (language != GoogleAPILanguage.Unset)
                constructedUrl += "&Language=" + GoogleAPILanguageHelper.ToAPIString(language);

            return constructedUrl;
        }

        private bool ContainsNumber(string value)
        {
            return Regex.IsMatch(value, ".*\\d+.*");
        }

        /// <summary>
        /// Creates the Nationaal Geo Register predictions URI.
        /// </summary>
        /// <returns>The predictions URI.</returns>
        /// <param name="newTextValue">New text value.</param>
        private string CreateNationaalGeoRegisterPredictionsUri(string newTextValue)
        {
            var url = "https://geodata.nationaalgeoregister.nl/locatieserver/v3/suggest";

            // Adding search text filter to URL
            var input = Uri.EscapeUriString(newTextValue);

            var constructedUrl = string.Empty;
            if (ContainsNumber(newTextValue))
            {
                // URL without municipality filter for addresses with house number
                constructedUrl = $"{url}?q={input}%20and%20type:%22adres%22";
            }
            else
            {   // URL without municipality filter without house number
                constructedUrl = $"{url}?q={input}%20and%20type:%22weg%22";
            }

            if (!string.IsNullOrEmpty(_settingsService.SearchMunicipality))
            {
                // Adding municipality filter to URL
                constructedUrl = $"{constructedUrl}%20and%20gemeentenaam:%22{_settingsService.SearchMunicipality}%22";
            }
            return constructedUrl;
        }

        /// <summary>
        /// Returns a string representation of the specified place type.
        /// </summary>
        /// <returns>The type value.</returns>
        /// <param name="type">Type.</param>
        private string PlaceTypeValue(PlaceType type)
        {
            switch (type)
            {
                case PlaceType.All:
                    return "";
                case PlaceType.Geocode:
                    return "geocode";
                case PlaceType.Address:
                    return "address";
                case PlaceType.Establishment:
                    return "establishment";
                case PlaceType.Regions:
                    return "(regions)";
                case PlaceType.Cities:
                    return "(cities)";
                default:
                    return "";
            }
        }

        public async Task NavigateToLocation(GeographicLocation position, string packageName)
        {
            try
            {
                var platformManager = DependencyService.Get<IPlatformManager>();

                switch (packageName)
                {
                    case "com.google.android.apps.maps":
                    case "com.google.android.apps.mapslite":
                    case "comgooglemaps://":
                        platformManager.OpenGoogleMaps(position);
                        break;
                    case "com.ubercab":
                    case "com.ubercab.uberlite":
                    case "uber://":
                        platformManager.OpenUber(position);
                        break;
                    case "com.waze":
                    case "waze://":
                        platformManager.OpenWaze(position);
                        break;
                    case "iosmaps://":
                        var location = new XE.Location(position.Latitude, position.Longitude);
                        var options = new XE.MapLaunchOptions { NavigationMode = XE.NavigationMode.Driving };
                        await XE.Map.OpenAsync(location, options);
                        break;
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogException("Exception occured while picking a navigation app", ex);
                await _dialogService.ShowToast($"NavigateToLocation error: {ex.Message}, {ex}");
            }
        }
    }
}
