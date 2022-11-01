using System;
namespace XamarinEnterpriseApp.Xamarin.Core.Models
{
    public class AuthenticationRequest
    {
        public string Password { get; set; }

        public string UserName { get; set; }

        public string DeviceToken { get; set; }

        public string AppVersion { get; set; }

        public string DevicePlatform { get; set; }

        public string Language { get; set; }

        public string IpAddress { get; set; }

        public string Application { get; set; }
    }
}
