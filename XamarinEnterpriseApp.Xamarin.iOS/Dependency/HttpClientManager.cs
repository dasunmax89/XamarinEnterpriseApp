using System.Net.Http;
using XamarinEnterpriseApp.Xamarin.Core.Dependency;
using XamarinEnterpriseApp.Xamarin.iOS.Dependency;
using Xamarin.Forms;

[assembly: Dependency(typeof(HttpClientManager))]
namespace XamarinEnterpriseApp.Xamarin.iOS.Dependency
{
    public class HttpClientManager : IHttpClientManager
    {
        public HttpClientManager()
        {
        }

        public HttpClient GetHttpClientForAuthentication()
        {
            var client = new HttpClient(new NSUrlSessionHandler());

            return client;
        }

        public HttpClient GetHttpClient()
        {
            var client = new HttpClient(new NSUrlSessionHandler());

            return client;
        }
    }
}
