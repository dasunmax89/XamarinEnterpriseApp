using System.Net;
using System.Net.Http;
using XamarinEnterpriseApp.Xamarin.Core.Dependency;
using XamarinEnterpriseApp.Xamarin.Droid.Dependency;
using Xamarin.Android.Net;
using Xamarin.Forms;

[assembly: Dependency(typeof(HttpClientManager))]
namespace XamarinEnterpriseApp.Xamarin.Droid.Dependency
{
    public class HttpClientManager : IHttpClientManager
    {
        public HttpClientManager()
        {
        }

        public HttpClient GetHttpClientForAuthentication()
        {
            var client = new HttpClient();

            return client;
        }

        public HttpClient GetHttpClient()
        {
            var client = new HttpClient();

            if (Android.OS.Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.Lollipop)
            {
                var handler = new AndroidClientHandler
                {
                    UseCookies = true,
                    AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip,
                };

                client = new HttpClient(new AndroidClientHandler());
            }
            return client;
        }
    }
}
