using System.Text.Json.Serialization;

namespace VegaIoTApi.AppServices.Models
{
    public class AuthenticationRequestModel
    {
        [JsonPropertyName("cmd")]
        public string Cmd => "auth_req";

        [JsonPropertyName("login")]
        public string Login { get; set; } = string.Empty;

        [JsonPropertyName("password")]
        public string Password { get; set; } = string.Empty;
    }
}