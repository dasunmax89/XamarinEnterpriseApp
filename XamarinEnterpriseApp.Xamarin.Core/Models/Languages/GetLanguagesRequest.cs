using System;
namespace XamarinEnterpriseApp.Xamarin.Core.Models
{
    public class GetLanguagesRequest
    {
        public long SessionId { get; set; }

        public string LanguageId { get; internal set; }
    }
}
