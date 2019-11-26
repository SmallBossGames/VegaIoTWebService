using System.Text.Json.Serialization;

namespace VegaIoTApi.AppServices.Models
{
    public class DeviceDataReq
    {
        public class SelectModel
        {
            [JsonPropertyName("date_from")]
            public int? DateFrom { get; set; }
            [JsonPropertyName("date_to")]
            public int? DateTo { get; set; }
            [JsonPropertyName("begin_index")]
            public int? BeginIndex { get; set; }
            [JsonPropertyName("limit")]
            public int? Limit { get; set; }
            [JsonPropertyName("port")]
            public int? Port { get; set; }
            [JsonPropertyName("direction")]
            public string? Direction { get; set; }
            [JsonPropertyName("withMacCommands")]
            public bool? WithMacCommands { get; set; }
        }

        [JsonPropertyName("cmd")]
        public string Cmd => "get_data_req";

        [JsonPropertyName("devEui")]
        public string DevEui { get; set; } = string.Empty;

        [JsonPropertyName("select")]
        public SelectModel? Select { get; set; }
    }
}