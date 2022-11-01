using System;
using System.Net.Http;

namespace XamarinEnterpriseApp.Xamarin.Core.Dependency
{
    public interface IHttpClientManager
    {
        HttpClient GetHttpClient();
        HttpClient GetHttpClientForAuthentication();
    }
}
