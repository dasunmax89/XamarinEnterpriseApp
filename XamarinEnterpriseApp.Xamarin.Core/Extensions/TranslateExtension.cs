using System;
using System.Globalization;
using System.Reflection;
using System.Resources;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XamarinEnterpriseApp.Xamarin.Core.Extensions
{
    [ContentProperty("Text")]
    public class TranslateExtension : IMarkupExtension
    {
        const string RESOURCE_ID = "XamarinEnterpriseApp.Xamarin.Core.Localization.AppResources";

        public string Text { get; set; }

        public string Optional { get; set; }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (Text == null)
                return null;
            ResourceManager resourceManager = new ResourceManager(RESOURCE_ID, typeof(TranslateExtension).GetTypeInfo().Assembly);

            string text = string.Empty;

            string optionalText = string.Empty;

            string toTranslate = string.Empty;

            string suffix = string.Empty;

            if (Text.EndsWith("*"))
            {
                toTranslate = Text.Remove(Text.Length - 1);

                suffix = "*";
            }
            else
            {
                toTranslate = Text;
            }

            text = resourceManager.GetString(toTranslate, CultureInfo.CurrentCulture);

            if (Optional != null)
            {
                optionalText = resourceManager.GetString(Optional, CultureInfo.CurrentCulture);
            }

            return $"{text} {suffix} {optionalText}";
        }
    }
}
