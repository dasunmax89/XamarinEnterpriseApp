//
// Place.cs
//
// Author:
//       Alex Smith <alex@duriancode.com>
//
// Copyright (c) 2017 (c) Alexander Smith
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using XamarinEnterpriseApp.Xamarin.Core.Extensions;
using Newtonsoft.Json.Linq;

namespace XamarinEnterpriseApp.Xamarin.Core.Views
{
    /// <summary>
    /// Place.
    /// </summary>
    public class Place
    {
        /// <summary>
        /// Gets or sets the identifier (can be NULL).
        /// </summary>
        /// <value>The identifier.</value>
        public string ID { get; set; }

        /// <summary>
        /// Gets or sets the place identifier.
        /// </summary>
        /// <value>The place identifier.</value>
        public string Place_ID { get; set; }

        /// <summary>
        /// Gets or sets the reference.
        /// </summary>
        /// <value>The reference.</value>
        public string Reference { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the latitude.
        /// </summary>
        /// <value>The latitude.</value>
        public double? Latitude { get; set; }

        /// <summary>
        /// Gets or sets the longitude.
        /// </summary>
        /// <value>The longitude.</value>
        public double? Longitude { get; set; }

        /// <summary>
        /// Gets or sets the individual address components.
        /// </summary>
        /// <value>The address components.</value>
        public List<AddressComponent> AddressComponents { get; set; }

        /// <summary>
        /// Gets or sets the address (formatted)
        /// </summary>
        /// <value>The formatted address.</value>
        public string FormattedAddress { get; set; }

        /// <summary>
        /// Gets or sets the (formatted) phone number
        /// </summary>
        /// <value>The phone number.</value>
        public string PhoneNumberFormatted { get; set; }

        /// <summary>
        /// Gets or sets the (international) phone number
        /// </summary>
        /// <value>The phone number.</value>
        public string PhoneNumberInternational { get; set; }

        /// <summary>
        /// Gets the types of this prediction
        /// see https://developers.google.com/places/web-service/supported_types
        /// </summary>
        /// <value>The types of the prediction.</value>
        public List<string> Types { get; set; }

        /// <summary>
        /// Gets or sets the Website URL.
        /// </summary>
        /// <value>The Website URL.</value>
        public string Website { get; set; }

        /// <summary>
        /// Gets or sets the Vicinity.
        /// </summary>
        /// <value>The Vicinity.</value>
        public string Vicinity { get; set; }

        /// <summary>
        /// Gets or sets the UTC Offset (NULL if not set).
        /// </summary>
        /// <value>The UTC Offset.</value>
        public int? UTCOffset { get; set; }

        /// <summary>
        /// Gets or sets the raw json value.
        /// </summary>
        /// <value>json string.</value>
        public string Raw { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:XamarinEnterpriseApp.Xamarin.Core.Views.Place"/> class.
        /// </summary>
        /// <param name="jsonObject">Json object.</param>
        public Place(JObject jsonObject, bool isNationaalGeoRegisterAPI)
        {
            if (!isNationaalGeoRegisterAPI)
            {
                ID = jsonObject["result"]["id"]?.Value<string>();
                Place_ID = jsonObject["result"]["place_id"]?.Value<string>() ?? string.Empty;
                Reference = jsonObject["result"]["reference"]?.Value<string>() ?? string.Empty;
                Name = jsonObject["result"]["name"]?.Value<string>() ?? string.Empty;
                AddressComponents = jsonObject["result"]["address_components"]?.Value<JArray>()?.Select(p => AddressComponent.FromJSON(p.Value<JObject>()))?.ToList() ?? new List<AddressComponent>();
                FormattedAddress = jsonObject["result"]["formatted_address"]?.Value<string>() ?? string.Empty;
                PhoneNumberFormatted = jsonObject["result"]["formatted_phone_number"]?.Value<string>() ?? string.Empty;
                PhoneNumberInternational = jsonObject["result"]["international_phone_number"]?.Value<string>() ?? string.Empty;
                Types = jsonObject["result"]["types"]?.Value<JArray>()?.Select(p => p.Value<string>())?.ToList() ?? new List<string>();
                Website = jsonObject["result"]["website"]?.Value<string>() ?? string.Empty;
                Vicinity = jsonObject["result"]["vicinity"]?.Value<string>() ?? string.Empty;
                UTCOffset = jsonObject["result"]["utc_offset"]?.Value<int>();

                string lng = jsonObject["result"]?["geometry"]?["location"]?["lng"]?.Value<string>();

                Longitude = lng.FormatDouble();

                string lat = jsonObject["result"]?["geometry"]?["location"]?["lat"]?.Value<string>();

                Latitude = lat.FormatDouble();
            }
            else
            {
                ID = (string)jsonObject["response"]?["docs"]?[0]["id"];
                string s = (string)jsonObject["response"]?["docs"]?[0]["centroide_ll"];

                if (!string.IsNullOrEmpty(s))
                {
                    s = s.Replace("POINT(", string.Empty).Replace(")", string.Empty);
                    string[] ar = s.Split(' ');

                    if (ar.Count() > 1)
                    {
                        string lon = ar[0];

                        Longitude = lon.FormatDouble();

                        string lat = ar[1];

                        Latitude = lat.FormatDouble();
                    }
                }
            }

            Raw = jsonObject.ToString();
        }

        public Place()
        {

        }

        public AddressComponent GetAddressComponentOrNull(string type)
        {
            foreach (var component in AddressComponents)
            {
                if (component.Types.Contains(type)) return component;
            }
            return null;
        }

        public AddressComponent AdminArea => GetAddressComponentOrNull("administrative_area_level_1");
        public AddressComponent SubAdminArea => GetAddressComponentOrNull("administrative_area_level_2");
        public AddressComponent SubSubAdminArea => GetAddressComponentOrNull("administrative_area_level_3");
        public AddressComponent Locality => GetAddressComponentOrNull("locality");
        public AddressComponent SubLocality => GetAddressComponentOrNull("sublocality_level_1") ?? GetAddressComponentOrNull("sublocality");
        public AddressComponent Thoroughfare => GetAddressComponentOrNull("route");
        public AddressComponent SubThoroughfare => GetAddressComponentOrNull("street_number");
        public AddressComponent PostalCode => GetAddressComponentOrNull("postal_code");
        public AddressComponent Country => GetAddressComponentOrNull("country");
        public AddressComponent StreetName => GetAddressComponentOrNull("route");
        public AddressComponent StreetNumber => GetAddressComponentOrNull("street_number");
    }
}
