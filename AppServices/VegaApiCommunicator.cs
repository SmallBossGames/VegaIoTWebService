using System;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using VegaIoTApi.AppServices.Models;
using VegaIoTWebService.Data.Models;

namespace VegaIoTApi.AppServices
{
    /// <summary>
    /// ����� � ����� ����� ���-������
    /// </summary>
    public class VegaApiCommunicator : IVegaApiCommunicator 
    {
        private readonly Uri _webSocketUri;
        private readonly string login;
        private readonly string password;

        public VegaApiCommunicator(Uri webSocketUri, string login, string password)
        {
            _webSocketUri = webSocketUri ?? throw new ArgumentNullException(nameof(webSocketUri));
            this.login = login ?? throw new ArgumentNullException(nameof(login));
            this.password = password ?? throw new ArgumentNullException(nameof(password));
        }

        public async Task<AuthenticationResp> AuthenticateAsync
            (AuthenticationReq request, CancellationToken cancellationToken = default)
        {
            using var socket = new ClientWebSocket();
            await socket.ConnectAsync(_webSocketUri, cancellationToken);

            return await WebSocketRequest<AuthenticationReq, AuthenticationResp>(request, socket);
        }

        private Task<AuthenticationResp> AuthenticateAsync
            (WebSocket socket, CancellationToken cancellationToken = default)
        {
            var request = new AuthenticationReq()
            {
                Login = login,
                Password = password,
            };
            return WebSocketRequest<AuthenticationReq, AuthenticationResp>(request, socket);
        }

        public async Task<DeviceDataResp> GetDeviceDataAsync
            (DeviceDataReq request, CancellationToken cancellationToken = default)
        {
            using var socket = new ClientWebSocket();
            await socket.ConnectAsync(_webSocketUri, cancellationToken);

            var authRequest = await AuthenticateAsync(socket, cancellationToken);

            if (authRequest.Status != true)
                throw new InvalidOperationException();

            return await WebSocketRequest<DeviceDataReq, DeviceDataResp>(request, socket, 10000);
        }

        async Task<TResponse> WebSocketRequest<TRequest, TResponse>
            (TRequest request, WebSocket socket, int receiveBufferSize = 2048, CancellationToken cancellationToken = default)
        {
            var requestBytes = JsonSerializer.SerializeToUtf8Bytes(request);
            var receiveBytes = new byte[receiveBufferSize];

            await socket.SendAsync(requestBytes, WebSocketMessageType.Text, true, cancellationToken);
            var receiveResult = await socket.ReceiveAsync(receiveBytes, cancellationToken);

            return JsonSerializer.Deserialize<TResponse>(receiveBytes[..receiveResult.Count]);
        }

        public async Task<LinkedList<VegaTempDeviceData>> GetTemperatureDeviceDatasAsync
            (string eui, long deviceId, DateTimeOffset from, CancellationToken cancellationToken = default)
        {
            var unixTime = from.ToUnixTimeMilliseconds();

            var request = new DeviceDataReq()
            {
                DevEui = eui,
                Select = new DeviceDataReq.SelectModel()
                {
                    Direction = "UPLINK",
                    DateFrom = unixTime < 0 ? 0 : unixTime,
                }
            };

            DeviceDataResp result;
            try
            {
                result = await GetDeviceDataAsync(request, cancellationToken);
            }
            catch(WebSocketException)
            {
                return new LinkedList<VegaTempDeviceData>();
            }

            var list = new LinkedList<VegaTempDeviceData>();

            foreach (var a in result.DataList)
            {
                if (a.Type == "UNCONF_UP" && a.Data.Length >= 26 && a.Data[0] == '0' && a.Data[1] == '1')
                {
                    var processed = VegaTempDeviceData.Parse(a.Data);
                    if(processed.Uptime > from)
                    {
                        processed.DeviceId = deviceId;
                        list.AddLast(processed);
                    }
                }
            }
            return list;
        }

        public async Task<LinkedList<VegaMoveDeviceData>> GetMoveDeviceDataAsync
            (string eui, long deviceId, DateTimeOffset from, CancellationToken cancellationToken = default)
        {
            var request = new DeviceDataReq()
            {
                DevEui = eui,
                Select = new DeviceDataReq.SelectModel()
                {
                    Direction = "UPLINK",
                    DateFrom = from.ToUnixTimeMilliseconds(),
                }
            };

            var result = await GetDeviceDataAsync(request, cancellationToken);

            var list = new LinkedList<VegaMoveDeviceData>();

            foreach (var a in result.DataList)
            {
                if (a.Type == "UNCONF_UP" && a.Data.Length >= 20)
                {
                    var processed = VegaMoveDeviceData.Parse(a.Data);
                    processed.DeviceId = deviceId; // ������� ������ � �� ������ ��� �������, ��� ������ ����� ��� ���������?
                    list.AddLast(processed);
                }
            }
            return list;
        }

        public async Task<LinkedList<VegaMagnetDeviceData>> GetMagnetDeviceDataAsync
            (string eui, long deviceId, DateTimeOffset from, CancellationToken cancellationToken = default)
        {
            var request = new DeviceDataReq()
            {
                DevEui = eui,
                Select = new DeviceDataReq.SelectModel()
                {
                    Direction = "UPLINK",
                    DateFrom = from.ToUnixTimeMilliseconds(),
                }
            };

            var result = await GetDeviceDataAsync(request, cancellationToken);

            var list = new LinkedList<VegaMagnetDeviceData>();

            foreach (var a in result.DataList)
            {
                if (a.Type == "UNCONF_UP" && a.Data.Length >= 22)
                {
                    var processed = VegaMagnetDeviceData.Parse(a.Data);
                    processed.DeviceId = deviceId;
                    list.AddLast(processed);
                }
            }
            return list;
        }

        public async Task<LinkedList<VegaImpulsDeviceData>> GetImpulsDeviceDataAsync
            (string eui, long deviceId, DateTimeOffset from, CancellationToken cancellationToken = default)
        {
            // ��������, ����� �������� ���� � ������ ������� ��� ������ �������� ������
            var request = new DeviceDataReq()
            {
                DevEui = eui,
                Select = new DeviceDataReq.SelectModel()
                {
                    Direction = "UPLINK", // ��������� �� ��� ������� ��-11
                    DateFrom = from.ToUnixTimeMilliseconds(),
                }
            };

            var result = await GetDeviceDataAsync(request, cancellationToken);

            var list = new LinkedList<VegaImpulsDeviceData>();

            foreach (var a in result.DataList)
            {
                var processed = VegaImpulsDeviceData.Parse(a.Data);
                processed.DeviceId = deviceId;
                list.AddLast(processed);
            }

            return list;
        }
    }
}