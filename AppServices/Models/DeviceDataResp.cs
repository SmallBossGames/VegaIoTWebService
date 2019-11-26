using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace VegaIoTApi.AppServices.Models
{
    public class DeviceDataResp
    {
        public class DataListModel
        {
            [JsonPropertyName("ts")]
            public long TS { get; set; }
            [JsonPropertyName("gatewayId")]
            public string GatewayId { get; set; } = string.Empty;
            [JsonPropertyName("ack")]
            public int Ack { get; set; }
            [JsonPropertyName("fcnt")]
            public int FrameCounter { get; set; }
            [JsonPropertyName("port")]
            public int Port { get; set; }
            [JsonPropertyName("data")]
            public string Data { get; set; } = string.Empty;
            [JsonPropertyName("macData")]
            public string? MacData { get; set; }
            [JsonPropertyName("freq")]
            public int Frequency { get; set; }
            [JsonPropertyName("dr")]
            public string Dr { get; set; } = string.Empty;
            [JsonPropertyName("rssi")]
            public int Rssi { get; set; }
            [JsonPropertyName("snr")]
            public float Snr { get; set; }
            [JsonPropertyName("type")]
            public string Type { get; set; } = string.Empty;
            [JsonPropertyName("packetStatus")]
            public string? PacketStatus { get; set; }
        }
        [JsonPropertyName("cmd")]
        public string Cmd { get; set; } = string.Empty;
        [JsonPropertyName("status")]
        public bool Status { get; set; }
        [JsonPropertyName("err_string")]
        public string? ErrString { get; set; } 
        [JsonPropertyName("devEui")]
        public string DevEui { get; set; } = string.Empty;
        [JsonPropertyName("appEui")]
        public string AppEui { get; set; } = string.Empty;
        [JsonPropertyName("direction")]
        public string? Direction { get; set; }
        [JsonPropertyName("totalNum")]
        public int? TotalNum { get; set; }
        [JsonPropertyName("data_list")]
        public IEnumerable<DataListModel> DataList { get; set; } = Array.Empty<DataListModel>();
    }
}