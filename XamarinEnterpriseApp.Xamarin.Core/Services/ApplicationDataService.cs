using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using XamarinEnterpriseApp.Xamarin.Core.Constants;
using XamarinEnterpriseApp.Xamarin.Core.Dependency;
using XamarinEnterpriseApp.Xamarin.Core.Enums;
using XamarinEnterpriseApp.Xamarin.Core.Helpers;
using XamarinEnterpriseApp.Xamarin.Core.Ioc;
using XamarinEnterpriseApp.Xamarin.Core.Models;
using XamarinEnterpriseApp.Xamarin.Core.Serialization;
using System.Net.Mail;
using System.Text;

namespace XamarinEnterpriseApp.Xamarin.Core.Services
{
    public interface IApplicationDataService
    {
        Task<TResponse> PostRequest<TResponse, TRequest>(TRequest request) where TResponse : BaseResponse, new()
                                                                                              where TRequest : BaseRequest, new();

        Task<TResponse> GetRequest<TResponse, TRequest>(TRequest request) where TResponse : BaseResponse, new()
                                                                                              where TRequest : BaseRequest, new();

        Task<TResponse> GetRawRequest<TResponse, TRequest>(TRequest request) where TResponse : BaseResponse, new()
                                                                                             where TRequest : BaseRequest, new();

        Task<TResponse> GetLocalRequest<TResponse, TRequest>(TRequest request) where TResponse : BaseResponse, new()
                                                                                             where TRequest : BaseRequest, new();

        Task<TResponse> PostRowRequest<TResponse, TRequest>(TRequest request) where TResponse : new()
                                                                                              where TRequest : BaseRequest, new();

        Task<TResponse> PutRequest<TResponse, TRequest>(TRequest request) where TResponse : BaseResponse, new()
                                                                                              where TRequest : BaseRequest, new();

        Task<TResponse> DeleteRequest<TResponse, TRequest>(TRequest request) where TResponse : BaseResponse, new()
                                                                                           where TRequest : BaseRequest, new();


    }

    public class ApplicationDataService : BaseService, IApplicationDataService
    {
        private readonly IRequestProvider _requestProvider;
        private readonly ISettingsService _settingsService;

        public ApplicationDataService(IRequestProvider requestProvider, ISettingsService settingsService)
        {
            _requestProvider = requestProvider;
            _settingsService = settingsService;
        }

        public async Task<TResponse> PostRequest<TResponse, TRequest>(TRequest request) where TResponse : BaseResponse, new()
                                                                                              where TRequest : BaseRequest, new()
        {
            TResponse response = null;

            string baseApiUrl = GetBaseAPIUrl();

            string uri = UriHelper.CombineUri(baseApiUrl, request.EndPoint);

            string dataString = JsonConvert.SerializeObject(request);

            try
            {
                string accessToken = _settingsService.AuthAccessToken;

                response = await _requestProvider.PostAsync<TResponse>(uri, dataString, accessToken);
            }
            catch (System.Exception ex)
            {
                response = CreateErrorResponse<TResponse>(ex);
            }

            return response;
        }

        public async Task<TResponse> PostRowRequest<TResponse, TRequest>(TRequest request) where TResponse : new()
                                                                                             where TRequest : BaseRequest, new()
        {
            TResponse response = default(TResponse);

            string baseApiUrl = GetBaseAPIUrl();

            string uri = UriHelper.CombineUri(baseApiUrl, request.EndPoint);

            string dataString = JsonConvert.SerializeObject(request);

            var data = JsonConvert.DeserializeObject<Dictionary<string, string>>(dataString);

            try
            {
                string accessToken = _settingsService.AuthAccessToken;

                response = await _requestProvider.PostAsyncRow<TResponse>(uri, data, accessToken);
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex);
            }

            return response;
        }

        public async Task<TResponse> GetRequest<TResponse, TRequest>(TRequest request) where TResponse : BaseResponse, new()
                                                                                              where TRequest : BaseRequest, new()
        {
            TResponse response = null;

            string baseApiUrl = GetBaseAPIUrl();

            string uri = UriHelper.CombineUri(baseApiUrl, request.EndPoint);

            try
            {
                string accessToken = _settingsService.AuthAccessToken;

                response = await _requestProvider.GetAsync<TResponse>(uri, accessToken);
            }
            catch (System.Exception ex)
            {
                response = CreateErrorResponse<TResponse>(ex);
            }

            return response;
        }

        public async Task<TResponse> GetRawRequest<TResponse, TRequest>(TRequest request) where TResponse : BaseResponse, new()
                                                                                      where TRequest : BaseRequest, new()
        {
            TResponse response = null;

            string baseApiUrl = GetBaseAPIUrl();

            string uri = request.EndPoint;

            try
            {
                string accessToken = _settingsService.AuthAccessToken;

                string raw = await _requestProvider.GetRowAsync<TResponse>(uri, accessToken);

                response = JsonConvert.DeserializeObject<TResponse>(raw);

                response.IsSuccessful = true;
            }
            catch (System.Exception ex)
            {
                response = CreateErrorResponse<TResponse>(ex);
            }

            return response;
        }

        public async Task<TResponse> GetLocalRequest<TResponse, TRequest>(TRequest request) where TResponse : BaseResponse, new()
                                                                                             where TRequest : BaseRequest, new()
        {
            TResponse response = null;

            string jsonString = _requestProvider.GetJsonString(request.EndPoint);

            try
            {
                response = await _requestProvider.CreateResponse<TResponse>(jsonString);

                response.IsSuccessful = true;
            }
            catch (System.Exception ex)
            {
                response = CreateErrorResponse<TResponse>(ex);
            }

            return response;
        }

        public async Task<TResponse> PutRequest<TResponse, TRequest>(TRequest request) where TResponse : BaseResponse, new()
                                                                                             where TRequest : BaseRequest, new()
        {
            TResponse response = null;

            string baseApiUrl = GetBaseAPIUrl();

            string uri = UriHelper.CombineUri(baseApiUrl, request.EndPoint);

            string dataString = JsonConvert.SerializeObject(request);

            try
            {
                string accessToken = _settingsService.AuthAccessToken;

                response = await _requestProvider.PutAsync<TResponse>(uri, dataString, accessToken);
            }
            catch (System.Exception ex)
            {
                response = CreateErrorResponse<TResponse>(ex);
            }

            return response;
        }

        public async Task<TResponse> DeleteRequest<TResponse, TRequest>(TRequest request) where TResponse : BaseResponse, new()
                                                                                            where TRequest : BaseRequest, new()
        {
            TResponse response = null;

            string baseApiUrl = GetBaseAPIUrl();

            string uri = UriHelper.CombineUri(baseApiUrl, request.EndPoint);

            string dataString = JsonConvert.SerializeObject(request);

            try
            {
                string accessToken = _settingsService.AuthAccessToken;

                response = await _requestProvider.DeleteAsync<TResponse>(uri, dataString, accessToken);
            }
            catch (System.Exception ex)
            {
                response = CreateErrorResponse<TResponse>(ex);
            }

            return response;
        }

    }
}
