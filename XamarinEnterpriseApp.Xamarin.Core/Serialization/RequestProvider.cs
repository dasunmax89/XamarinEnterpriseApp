using XamarinEnterpriseApp.Xamarin.Core.Exceptions;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Polly;
using System;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Collections.Generic;
using XamarinEnterpriseApp.Xamarin.Core.Dependency;
using Xamarin.Forms;
using XamarinEnterpriseApp.Xamarin.Core.Models;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Reflection;

namespace XamarinEnterpriseApp.Xamarin.Core.Serialization
{
    public interface IRequestProvider
    {
        Task<TResult> GetAsync<TResult>(string uri, string token) where TResult : BaseResponse;

        Task<string> GetRowAsync<TResult>(string uri, string token = "") where TResult : BaseResponse;

        Task<TResult> PostAsync<TResult>(string uri, object data, string token) where TResult : BaseResponse;

        Task<TResult> PostAsyncRow<TResult>(string uri, object data, string token) where TResult : new();

        Task<TResult> PutAsync<TResult>(string uri, string data, string token) where TResult : BaseResponse;

        Task<TResult> DeleteAsync<TResult>(string uri, string data, string token) where TResult : BaseResponse;

        Task<TResult> CreateResponse<TResult>(string serialized) where TResult : BaseResponse;

        string GetJsonString(string endPoint);
    }

    public class RequestProvider : IRequestProvider
    {
        private readonly JsonSerializerSettings _serializerSettings;

        public RequestProvider()
        {
            _serializerSettings = RequestProvider.GetSerializerSettings();
        }

        public static JsonSerializerSettings GetSerializerSettings(bool isCultureIgnore = false)
        {
            var serializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                DateTimeZoneHandling = DateTimeZoneHandling.Utc,
                NullValueHandling = NullValueHandling.Ignore
            };

            //if (isCultureIgnore)
            //{
            //    var culture = new CultureInfo("en-US");

            //    culture.NumberFormat.NumberDecimalSeparator = ".";

            //    serializerSettings.Culture = culture;
            //}

            serializerSettings.Converters.Add(new StringEnumConverter());

            return serializerSettings;
        }

        public async Task<TResult> GetAsync<TResult>(string uri, string token = "") where TResult : BaseResponse
        {
            HttpClient httpClient = CreateHttpClient(token);

            var response = await Policy.Handle<WebException>(ex =>
            {
                Debug.WriteLine($"{ex.GetType().Name + " : " + ex.Message}");
                return true;
            })
            .WaitAndRetryAsync
            (
                5,
                retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))
             )
             .ExecuteAsync(async () => await httpClient.GetAsync(uri));

            await HandleResponse(response);

            string serialized = await response.Content.ReadAsStringAsync();

            TResult result = await CreateResponse<TResult>(serialized);

            return result;
        }

        public async Task<string> GetRowAsync<TResult>(string uri, string token = "") where TResult : BaseResponse
        {
            HttpClient httpClient = CreateHttpClient(token);

            var response = await Policy.Handle<WebException>(ex =>
            {
                return true;
            })
            .WaitAndRetryAsync
            (
                5,
                retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))
             )
             .ExecuteAsync(async () => await httpClient.GetAsync(uri));

            await HandleResponse(response);

            string serialized = await response.Content.ReadAsStringAsync();

            return serialized;
        }

        public async Task<TResult> PostAsync<TResult>(string uri, object data, string token) where TResult : BaseResponse
        {
            HttpClient httpClient = CreateHttpClient(token);

            HttpContent content = null; ;

            if (data is string)
            {
                string dataString = data as string;

                content = new StringContent(dataString);
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            }
            else if (data is Dictionary<string, string>)
            {
                var dataDictionary = data as Dictionary<string, string>;

                var multipartFormDataContent = new MultipartFormDataContent();
                multipartFormDataContent.Headers.ContentType.MediaType = "multipart/form-data";

                foreach (var item in dataDictionary)
                {
                    multipartFormDataContent.Add(new StringContent(item.Value), item.Key);
                }

                content = multipartFormDataContent;
            }

            HttpResponseMessage response = await httpClient.PostAsync(uri, content);

            await HandleResponse(response);

            string serialized = await response.Content.ReadAsStringAsync();

            TResult result = await CreateResponse<TResult>(serialized);

            return result;
        }

        public async Task<TResult> PostAsyncRow<TResult>(string uri, object data, string token) where TResult : new()
        {
            HttpClient httpClient = CreateHttpClient(token);

            HttpContent content = null; ;

            if (data is string)
            {
                string dataString = data as string;

                content = new StringContent(dataString);
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            }
            else if (data is Dictionary<string, string>)
            {
                var dataDictionary = data as Dictionary<string, string>;

                var multipartFormDataContent = new MultipartFormDataContent();
                multipartFormDataContent.Headers.ContentType.MediaType = "multipart/form-data";

                foreach (var item in dataDictionary)
                {
                    multipartFormDataContent.Add(new StringContent(item.Value), item.Key);
                }

                content = multipartFormDataContent;
            }

            HttpResponseMessage response = await httpClient.PostAsync(uri, content);

            await HandleResponse(response);

            string serialized = await response.Content.ReadAsStringAsync();

            TResult result = await Task.Run(() =>
               JsonConvert.DeserializeObject<TResult>(serialized, _serializerSettings));

            return result;
        }

        public async Task<TResult> PutAsync<TResult>(string uri, string data, string token) where TResult : BaseResponse
        {
            HttpClient httpClient = CreateHttpClient(token);

            var content = new StringContent(data);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            HttpResponseMessage response = await httpClient.PutAsync(uri, content);

            await HandleResponse(response);

            string serialized = await response.Content.ReadAsStringAsync();

            TResult result = await CreateResponse<TResult>(serialized);

            return result;
        }

        public async Task<TResult> DeleteAsync<TResult>(string uri, string data, string token) where TResult : BaseResponse
        {
            HttpClient httpClient = CreateHttpClient(token);

            var content = new StringContent(data);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            HttpRequestMessage request = new HttpRequestMessage
            {
                Content = content,
                Method = HttpMethod.Delete,
                RequestUri = new Uri(uri)
            };

            HttpResponseMessage response = await httpClient.SendAsync(request);

            await HandleResponse(response);

            string serialized = await response.Content.ReadAsStringAsync();

            TResult result = await CreateResponse<TResult>(serialized);

            return result;
        }

        public async Task<TResult> CreateResponse<TResult>(string serialized) where TResult : BaseResponse
        {
            JObject jObject = (JObject)JsonConvert.DeserializeObject(serialized, _serializerSettings);

            TResult result;

            if (jObject.ContainsKey("data"))
            {
                result = await Task.Run(() =>
                jObject["data"].ToObject<TResult>());
            }
            else
            {
                result = await Task.Run(() =>
                jObject.ToObject<TResult>());
            }

            string status = string.Empty;

            if (result == null)
            {
                result = Activator.CreateInstance<TResult>();
            }

            if (jObject.ContainsKey("status"))
            {
                status = await Task.Run(() =>
                jObject["status"].ToObject<string>());

                result.IsSuccessful = status == "success" || status == "succes";
            }
            else
            {
                result.IsSuccessful = true;
            }

            Debug.WriteLine("_______________________  data _________________________________");
            Debug.WriteLine(serialized);
            Debug.WriteLine("________________________________________________________");

            return result;
        }

        #region HttpClient

        private HttpClient CreateHttpClient(string token = "")
        {
            IHttpClientManager manager = DependencyService.Get<IHttpClientManager>();

            var httpClient = manager != null ? manager.GetHttpClient() : new HttpClient();

            if (httpClient == null)
            {
                httpClient = new HttpClient();
            }

            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            if (!string.IsNullOrEmpty(token))
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
            else
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", "YXBpX0VhZ2xlMzpHaWRqSm8pOSM0NCoyMDIw");
            }

            httpClient.DefaultRequestHeaders.Add("X-API-KEY", "22398191");

            return httpClient;
        }

        private void AddHeaderParameter(HttpClient httpClient, string parameter, string value)
        {
            if (httpClient == null)
                return;

            if (string.IsNullOrEmpty(parameter) || string.IsNullOrEmpty(value))
                return;

            httpClient.DefaultRequestHeaders.Add(parameter, value);
        }

        private void AddBasicAuthenticationHeader(HttpClient httpClient, string clientId, string clientSecret)
        {
            if (httpClient == null)
                return;

            if (string.IsNullOrWhiteSpace(clientId) || string.IsNullOrWhiteSpace(clientSecret))
                return;

            httpClient.DefaultRequestHeaders.Authorization = new BasicAuthenticationHeaderValue(clientId, clientSecret);
        }

        private async Task HandleResponse(HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                if (response.StatusCode == HttpStatusCode.Forbidden ||
                    response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    throw new ServiceAuthenticationException(content);
                }

                throw new HttpRequestExceptionEx(response.StatusCode, content);
            }
        }

        public string GetJsonString(string endPoint)
        {
            string jsonString = string.Empty;

            string jsonFile = $"{endPoint}.json";

            var assembly = typeof(RequestProvider).GetTypeInfo().Assembly;

            Stream stream = assembly.GetManifestResourceStream($"{assembly.GetName().Name}.Serialization.Data.{jsonFile}");

            using (var reader = new System.IO.StreamReader(stream))
            {
                jsonString = reader.ReadToEnd();
            }

            return jsonString;
        }

        #endregion
    }
}
