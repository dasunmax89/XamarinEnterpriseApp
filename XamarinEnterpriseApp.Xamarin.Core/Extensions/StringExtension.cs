using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Xamarin.Forms;
using XamarinEnterpriseApp.Xamarin.Core.Helpers;

namespace XamarinEnterpriseApp.Xamarin.Core.Extensions
{
    public static class StringExtension
    {
        public static string RemoveNewLines(this string stringVal)
        {
            string formattedText = string.Empty;

            formattedText = stringVal?.Replace(System.Environment.NewLine, string.Empty);

            return formattedText;
        }

        public static long ToNumber(this string stringVal)
        {
            bool canParse = long.TryParse(stringVal, out long numberValue);

            return numberValue;
        }

        public static bool Search(this string stringVal, string text)
        {
            if (string.IsNullOrEmpty(stringVal) || string.IsNullOrEmpty(text))
            {
                return false;
            }

            bool contains = stringVal.ToLower().Contains(text.ToLower());

            return contains;
        }

        public static bool Match(this string stringVal, string text)
        {
            if (string.IsNullOrEmpty(stringVal) || string.IsNullOrEmpty(text))
            {
                return false;
            }

            bool equals = stringVal.Equals(text, StringComparison.InvariantCultureIgnoreCase);

            return equals;
        }

        public static bool IsURI(this string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return false;
            }

            Uri uriResult;
            bool isURI = Uri.TryCreate(text, UriKind.Absolute, out uriResult)
                && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);

            return isURI;
        }

        public static bool IsValidPhoneNo(this string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return false;
            }

            var r1 = new Regex(@"^[0-9]{10}$");
            var r2 = new Regex(@"^(0031|031|\+31|\+\+31|31)?(0|\(0\))?([1-9]\d{1})(\d{3})(\d{2})(\d{2})$");
            var r3 = new Regex(@"^[0-9]{4}[-](\d{1,5})[\s.-]?");

            return r1.IsMatch(text) || r2.IsMatch(text) || r3.IsMatch(text);
        }


        public static bool IsValidHouseNumber(this string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return false;
            }

            var r = new Regex(@"^[0-9]*$");

            return r.IsMatch(text);
        }

        public static bool IsValidEmail(this string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return false;
            }

            text = text.ToLower();

            var r = new Regex(@"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
              @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$");

            return r.IsMatch(text);
        }

        public static string ReplaceFileNames(this string source)
        {
            if (string.IsNullOrEmpty(source)) return source;

            var replacements = new Dictionary<string, string> {
                { ".sql", ".txt" },
                { ".torrent", ".txt" },
                { ".Gold", ".txt"},
                { ".dfm", ".txt" },
            };

            return Regex.Replace(source, string.Join("|", replacements.Keys
            .Select(k => k.ToString()).ToArray()), m => replacements[m.Value]);
        }

        public static ImageSource ToImageSource(this string imageString)
        {
            ImageSource imageSource = null;

            try
            {
                if (!string.IsNullOrEmpty(imageString))
                {
                    if (imageString.IsURI())
                    {
                        imageSource = ImageSource.FromUri(new Uri(imageString));
                    }
                    else
                    {
                        imageSource = ImageSource.FromStream(
                           () => new MemoryStream(Convert.FromBase64String(imageString)));
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogException($"ToImageSource-Exception occured", ex);
            }


            return imageSource;
        }


    }
}
