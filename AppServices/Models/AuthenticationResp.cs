using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace VegaIoTApi.AppServices.Models
{
    public class AuthenticationResp
    {
        [JsonPropertyName("cmd")]
        public string Cmd { get; set; } = string.Empty;

        [JsonPropertyName("status")]
        public bool Status { get; set; }

        [JsonPropertyName("err_string")]
        public string? ErrString { get; set; }

        [JsonPropertyName("token")]
        public string? Token { get; set; }

        [JsonPropertyName("device_access")]
        public string? DeviceAcess { get; set; }

        [JsonPropertyName("consoleEnable")]
        public bool ConsoleEnable { get; set; }

        [JsonPropertyName("command_list")]
        public IEnumerable<string> CommandList { get; set; } = Array.Empty<string>();
    }
}