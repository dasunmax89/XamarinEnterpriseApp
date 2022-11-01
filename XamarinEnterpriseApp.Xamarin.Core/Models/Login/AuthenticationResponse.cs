using Newtonsoft.Json;

namespace XamarinEnterpriseApp.Xamarin.Core.Models
{
    public class AuthenticationResponse : BaseResponse
    {
        [JsonProperty("Ushd_id")]
        public long UshdId { get; set; }

        [JsonProperty("Ushd_rehd_id")]
        public long UshdRehdId { get; set; }

        [JsonProperty("Ushd_login")]
        public string UserName { get; set; }

        [JsonProperty("Ushd_desc")]
        public string UshdDesc { get; set; }

        [JsonProperty("Ushd_email")]
        public string UshdEmail { get; set; }

        [JsonProperty("Session_id")]
        public long SessionId { get; set; }

        [JsonProperty("Api_key")]
        public string AccessToken { get; set; }

        [JsonProperty("Ushd_change_login")]
        public bool UshdChangeLogin { get; set; }

        [JsonProperty("Ushd_ind_modify_plandates")]
        public bool UshdIndModifyPlandates { get; set; }

        [JsonProperty("MDAppConfig")]
        public MDAppConfig MDAppConfig { get; set; }

        [JsonProperty("Result")]
        public APIResult Result { get; set; }

        [JsonIgnore]
        public bool RememberCredentials { get; internal set; }

        [JsonIgnore]
        public string Password { get; internal set; }

        public AuthenticationResponse()
        {
        }
    }
}
