using System;
using Xamarin.Forms;

namespace XamarinEnterpriseApp.Xamarin.Core.Services.Mocks
{
    public class UIRenderMockService : IUIRenderService
    {
        public UIRenderMockService()
        {
        }

        public string FormatNumber(string numberString)
        {
            return numberString;
        }

        public FormattedString GetFormattedText(string caption, double fontSize)
        {
            return new FormattedString();
        }

        public void SetCulture(string culture)
        {

        }

        public string Translate(string key)
        {
            return key;
        }
    }
}
