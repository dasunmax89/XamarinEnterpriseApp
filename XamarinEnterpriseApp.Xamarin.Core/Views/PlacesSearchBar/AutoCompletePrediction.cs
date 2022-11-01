//
// AutoCompletePrediction.cs
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

using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace XamarinEnterpriseApp.Xamarin.Core.Views
{
    /// <summary>
    /// Auto complete prediction.
    /// </summary>
    public class AutoCompletePrediction
    {
        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the identifier.
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
        /// Gets the main text for UI display
        /// </summary>
        /// <value>The main text.</value>
        public string MainText { get; set; }

        /// <summary>
        /// Gets the secondary text for UI display
        /// </summary>
        /// <value>The secondary text.</value>
        public string SecondaryText { get; set; }

        /// <summary>
        /// Gets the individual terms of this prediction
        /// </summary>
        /// <value>The terms.</value>
        public List<string> Terms { get; set; }

        /// <summary>
        /// Gets the types of this prediction
        /// see https://developers.google.com/places/web-service/supported_types
        /// </summary>
        /// <value>The types of the prediction.</value>
        public List<string> Types { get; set; }


        #region Property accessors of Nationaal Geo Register

        /// <summary>
        /// Gets or sets the Type.
        /// </summary>
        /// <value>The Type.</value>
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the main text for UI display.
        /// </summary>
        /// <value>The Weergavenaam.</value>
        public string Weergavenaam { get; set; }

        /// <summary>
        /// Gets or sets the Score.
        /// </summary>
        /// <value>The Score.</value>
        public long Score { get; set; }

        public object Sender { get; set; }

        #endregion

        public static AutoCompletePrediction FromJson(JObject json)
        {
            var r = new AutoCompletePrediction
            {
                Description = json.ContainsKey("description") ? json["description"].Value<string>() : string.Empty,
                ID = json.ContainsKey("id") ? json["id"].Value<string>() : string.Empty,
                Place_ID = json.ContainsKey("place_id") ? json["place_id"].Value<string>() : string.Empty,
                Reference = json.ContainsKey("reference") ? json["reference"].Value<string>() : string.Empty,

                // Nationaal Geo Register API related property value setting
                MainText = json.ContainsKey("weergavenaam") ? (string)json["weergavenaam"] : string.Empty,
                Type = json.ContainsKey("type") ? (string)json["type"] : string.Empty,
                Score = json.ContainsKey("score") ? (long)json["score"] : 0,
            };

            if (json.ContainsKey("structured_formatting"))
            {
                var structuredFormatting = json["structured_formatting"].Value<JObject>();

                if (structuredFormatting.ContainsKey("main_text"))
                {
                    r.MainText = structuredFormatting["main_text"].Value<string>();
                }

                if (structuredFormatting.ContainsKey("secondary_text"))
                {
                    r.SecondaryText = structuredFormatting["secondary_text"].Value<string>();
                }
            }

            r.Terms = new List<string>();

            if (json.ContainsKey("terms"))
            {
                var jArray = json["terms"].Value<JArray>();

                if (jArray != null)
                {
                    foreach (var item in jArray)
                    {
                        if (item["value"] != null)
                        {
                            r.Terms.Add(item["value"].Value<string>());
                        }
                    }
                }
            }

            r.Types = new List<string>();

            if (json.ContainsKey("types"))
            {
                var jArray = json["types"].Value<JArray>();

                if (jArray != null)
                {
                    foreach (var item in jArray)
                    {
                        if (item != null)
                        {
                            r.Types.Add(item.Value<string>());
                        }
                    }
                }
            }

            return r;
        }
    }
}



