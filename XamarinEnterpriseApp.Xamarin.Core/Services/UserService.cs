using System.Collections.Generic;
using System.Threading.Tasks;
using XamarinEnterpriseApp.Xamarin.Core.Constants;
using XamarinEnterpriseApp.Xamarin.Core.Helpers;
using XamarinEnterpriseApp.Xamarin.Core.Models;
using XamarinEnterpriseApp.Xamarin.Core.Serialization;

namespace XamarinEnterpriseApp.Xamarin.Core.Services
{
    public interface IUserService
    {
        Task<AuthenticationResponse> Authenticate(AuthenticationRequest authenticationRequest);
        Task<LogoutResponse> Logout(LogoutRequest logoutRequest);
    }

    public class UserService : BaseService, IUserService
    {
        private readonly IRequestProvider _requestProvider;
        private readonly ISettingsService _settingsService;

        public UserService(IRequestProvider requestProvider, ISettingsService settingsService)
        {
            _requestProvider = requestProvider;
            _settingsService = settingsService;
        }

        public async Task<AuthenticationResponse> Authenticate(AuthenticationRequest authenticationRequest)
        {
            AuthenticationResponse response = null;

            string baseApiUrl = GetBaseAPIUrl();

            var uri = UriHelper.CombineUri(baseApiUrl, ApiConstants.AuthenticateEndpoint);

            string ipAddress = !string.IsNullOrEmpty(authenticationRequest.IpAddress) ?
                authenticationRequest.IpAddress : "127.0.0.1";

            var data = new Dictionary<string, string>
            {
                { "username", authenticationRequest.UserName },
                { "password", authenticationRequest.Password },
                { "language", authenticationRequest.Language },
                { "ip", ipAddress },
                { "application", authenticationRequest.Application },
                { "appVersion", authenticationRequest.AppVersion },
                { "devicePlatform", authenticationRequest.DevicePlatform },
                { "DeviceId", authenticationRequest.DeviceToken }
            };

            try
            {
                response = await _requestProvider.PostAsync<AuthenticationResponse>(uri, data, string.Empty);
                response.IsSuccessful = true;
            }
            catch (System.Exception ex)
            {
                response = CreateErrorResponse<AuthenticationResponse>(ex);
            }

            return response;
        }

        public async Task<LogoutResponse> Logout(LogoutRequest request)
        {
            LogoutResponse response = null;

            string baseApiUrl = GetBaseAPIUrl();

            var uri = UriHelper.CombineUri(baseApiUrl, ApiConstants.LogoutEndpoint);

            var headers = new Dictionary<string, string>
            {
                { "sessionID", request.SessionId.ToString() },
            };

            try
            {
                string accessToken =  _settingsService.AuthAccessToken;

                response = await _requestProvider.DeleteAsync<LogoutResponse>(uri, string.Empty, accessToken);
            }
            catch (System.Exception ex)
            {
                response = CreateErrorResponse<LogoutResponse>(ex);
            }

            return response;
        }
    }
}
