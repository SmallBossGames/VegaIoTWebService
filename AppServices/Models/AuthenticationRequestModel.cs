using System.Text.Json.Serialization;

namespace VegaIoTWebService.AppServices.Models
{
    public class AuthenticationRequestModel
    {
        [JsonPropertyName("cmd")]
        public string Cmd => "auth_req";

        [JsonPropertyName("login")]
        public string Login { get; set; }

        [JsonPropertyName("password")]
        public string Password { get; set; }
    }
}