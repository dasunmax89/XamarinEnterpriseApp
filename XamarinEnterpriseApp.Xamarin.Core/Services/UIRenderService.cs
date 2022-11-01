using System;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text.RegularExpressions;
using System.Threading;
using XamarinEnterpriseApp.Xamarin.Core.Behaviors;
using XamarinEnterpriseApp.Xamarin.Core.Constants;
using XamarinEnterpriseApp.Xamarin.Core.Models;
using XamarinEnterpriseApp.Xamarin.Core.Views;
using Xamarin.Forms;

namespace XamarinEnterpriseApp.Xamarin.Core.Services
{
    public interface IUIRenderService
    {
        void SetCulture(string culture);
        string FormatNumber(string numberString);
        FormattedString GetFormattedText(string caption, double fontSize);
        string Translate(string key);
    }

    public class UIRenderService : BaseService, IUIRenderService
    {
        public UIRenderService()
        {

        }

        public void SetCulture(string culture)  //example => culture ="en-US", "de-DE", "ar-TN" ...
        {
            CultureInfo cultureInfo = new CultureInfo(culture);
            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            Thread.CurrentThread.CurrentUICulture = cultureInfo;
        }

        public void AddTextEntryValidation(Entry textEntry, FormEntryValidation validations)
        {
            DetermineKeyBoard(textEntry, validations);
            AddBehaviours(textEntry, validations);
        }

        private void AddBehaviours(Entry textEntry, FormEntryValidation validations)
        {
            switch (validations.InputTextDataType)
            {
                case UIValidationValueTypes.INTEGER:
                case UIValidationValueTypes.INTEGER_POS:
                case UIValidationValueTypes.INTEGER_NEG:
                case UIValidationValueTypes.DOUBLE:
                case UIValidationValueTypes.DOUBLE_POS:
                case UIValidationValueTypes.DOUBLE_NEG:
                    var behavior = new InputViewValidationBehavior(validations);
                    textEntry.Behaviors.Add(behavior);
                    break;
            }
        }

        private void DetermineKeyBoard(Entry textEntry, FormEntryValidation validations)
        {
            Keyboard keyboard = Keyboard.Default;

            switch (validations.InputTextDataType)
            {
                case UIValidationValueTypes.TEXT:
                    keyboard = Keyboard.Text;
                    break;
                case UIValidationValueTypes.INTEGER:
                case UIValidationValueTypes.INTEGER_POS:
                case UIValidationValueTypes.DOUBLE:
                case UIValidationValueTypes.DOUBLE_POS:
                    keyboard = Keyboard.Numeric;
                    break;
                case UIValidationValueTypes.INTEGER_NEG:
                case UIValidationValueTypes.DOUBLE_NEG:
                    keyboard = Keyboard.Text;
                    break;
                case UIValidationValueTypes.PHONENUMBER:
                    keyboard = Keyboard.Telephone;
                    break;
                case UIValidationValueTypes.EMAILADDRESS:
                    keyboard = Keyboard.Email;
                    break;
                case UIValidationValueTypes.URL:
                    keyboard = Keyboard.Url;
                    break;
                case UIValidationValueTypes.NONE:
                    keyboard = Keyboard.Default;
                    break;
            }

            textEntry.Keyboard = keyboard;
        }

        public string FormatNumber(string numberString)
        {
            var currentCulture = GlobalSetting.Instance.CurrentCulture;

            var language = currentCulture.Name;

            if (string.IsNullOrEmpty(language))
            {
                language = "nl-NL";
            }

            NumberFormatInfo nfi = new CultureInfo(language).NumberFormat;
            nfi.NumberDecimalDigits = 2;

            string formattedValue = string.Empty;

            bool canParse = double.TryParse(numberString, NumberStyles.Any, CultureInfo.InvariantCulture, out double parsedVal);

            if (canParse)
            {
                if (Math.Abs(parsedVal % 1) <= (Double.Epsilon * 100))
                {
                    formattedValue = parsedVal.ToString("N0", nfi);
                }
                else
                {
                    formattedValue = parsedVal.ToString("N", nfi);
                }
            }
            else
            {
                formattedValue = numberString;
            }

            return formattedValue;
        }

        public FormattedString GetFormattedText(string caption, double fontSize)
        {
            FormattedString formattedString = new FormattedString();

            if (!string.IsNullOrEmpty(caption))
            {
                var pieces = Regex.Split(caption, "(<b>|</b>|<i>|</i>)").Where(p => !String.IsNullOrEmpty(p));

                bool isBold = false;
                bool isItalic = false;

                foreach (var item in pieces)
                {
                    if (item.Equals(@"<b>"))
                    {
                        isBold = true;
                        continue;
                    }
                    else if (item.Equals(@"</b>"))
                    {
                        isBold = false;
                        continue;
                    }
                    else if (item.Equals(@"<i>"))
                    {
                        isItalic = true;
                        continue;
                    }
                    else if (item.Equals(@"</i>"))
                    {
                        isItalic = false;
                        continue;
                    }

                    if (isBold)
                    {
                        formattedString.Spans.Add(new Span { Text = item, FontAttributes = FontAttributes.Bold, FontSize = fontSize });
                    }
                    else if (isItalic)
                    {
                        formattedString.Spans.Add(new Span { Text = item, FontAttributes = FontAttributes.Italic, FontSize = fontSize });
                    }
                    else
                    {
                        formattedString.Spans.Add(new Span { Text = item, FontAttributes = FontAttributes.None, FontSize = fontSize });
                    }
                }
            }

            return formattedString;
        }

        public string Translate(string key)
        {
            const string resourceId = "XamarinEnterpriseApp.Xamarin.Core.Localization.AppResources";

            ResourceManager resourceManager = new ResourceManager(resourceId, typeof(UIRenderService).GetTypeInfo().Assembly);

            return resourceManager.GetString(key, CultureInfo.CurrentCulture); ;
        }
    }
}
