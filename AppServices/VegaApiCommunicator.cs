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
    public class VegaApiCommunicator : IVegaApiCommunicator // связь с вего через веб-сокеты
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
            (AuthenticationReq request, CancellationToken cancellationToken)
        {
            using var socket = new ClientWebSocket();
            await socket.ConnectAsync(_webSocketUri, cancellationToken);
            return await WebSocketRequest<AuthenticationReq, AuthenticationResp>(request, socket);
        }

        private Task<AuthenticationResp> AuthenticateAsync
            (WebSocket socket, CancellationToken cancellationToken)
        {
            var request = new AuthenticationReq()
            {
                Login = login,
                Password = password,
            };
            return WebSocketRequest<AuthenticationReq, AuthenticationResp>(request, socket);
        }

        public async Task<DeviceDataResp> GetDeviceDataAsync
            (DeviceDataReq request, CancellationToken cancellationToken)
        {
            using var socket = new ClientWebSocket();
            await socket.ConnectAsync(_webSocketUri, cancellationToken);

            var authRequest = await AuthenticateAsync(socket, cancellationToken);

            if (authRequest.Status != true)
                throw new InvalidOperationException();

            return await WebSocketRequest<DeviceDataReq, DeviceDataResp>(request, socket, 10000);
        }

        async Task<TResponse> WebSocketRequest<TRequest, TResponse> // сделать брата
            (TRequest request, WebSocket socket, int reciveBufferSize = 2048)
        {
            var requestBytes = JsonSerializer.SerializeToUtf8Bytes(request);
            var reciveBytes = new byte[reciveBufferSize];

            await socket.SendAsync(requestBytes, WebSocketMessageType.Text, true, CancellationToken.None);
            var receiveResult = await socket.ReceiveAsync(reciveBytes, CancellationToken.None);

            return JsonSerializer.Deserialize<TResponse>(reciveBytes[..receiveResult.Count]);
        }

        bool TryRequest<TRequest, TResponse>(TRequest request, out TResponse response)
        {
            throw new NotImplementedException();

            return true;
        }

        public async Task<LinkedList<VegaTempDeviceData>> GetTemperatureDeviceDatasAsync
            (string eui, long deviceId, DateTime from)
        {
            var request = new DeviceDataReq()
            {
                DevEui = eui,
                Select = new DeviceDataReq.SelectModel()
                {
                    Direction = "UPLINK",
                    DateFrom = GetUnixTime(from)
                }
            };

            var result = await GetDeviceDataAsync(request, CancellationToken.None);

            var list = new LinkedList<VegaTempDeviceData>();

            foreach (var a in result.DataList)
            {
                if (a.Type == "UNCONF_UP" && a.Data.Length >= 26 && a.Data[0] == '0' && a.Data[1] == '1')
                {
                    var processed = VegaTempDeviceData.Parse(a.Data);
                    processed.DeviceId = deviceId;
                    var temperature = processed.Temperature / 10.0;
                    list.AddLast(processed);
                }
            }
            return list;
        }

        private static ulong GetUnixTime(DateTime time)
        {
            DateTime unixAge = new DateTime(1970, 1, 1).ToUniversalTime();

            if (time < unixAge)
                return 0;

            return (ulong)(time - unixAge).TotalMilliseconds;
        }

        public async Task<LinkedList<VegaMoveDeviceData>> GetMoveDeviceDataAsync(string eui, long deviceId, DateTime from)
        {
            var request = new DeviceDataReq()
            {
                DevEui = eui,
                Select = new DeviceDataReq.SelectModel()
                {
                    Direction = "UPLINK",
                    DateFrom = GetUnixTime(from)
                }
            };

            var result = await GetDeviceDataAsync(request, CancellationToken.None);

            var list = new LinkedList<VegaMoveDeviceData>();

            foreach (var a in result.DataList)
            {
                if (a.Type == "UNCONF_UP" && a.Data.Length >= 20)
                {
                    var processed = VegaMoveDeviceData.Parse(a.Data);
                    processed.DeviceId = deviceId; // сделать запись в БД только при условии, что датчик менял своё состояние?
                    list.AddLast(processed);
                }
            }
            return list;
        }

        public async Task<LinkedList<VegaMagnetDeviceData>> GetMagnetDeviceDataAsync(string eui, long deviceId, DateTime from)
        {
            var request = new DeviceDataReq()
            {
                DevEui = eui,
                Select=new DeviceDataReq.SelectModel() 
                {
                    Direction = "UPLINK",
                    DateFrom = GetUnixTime(from)
                }
            };

            var result = await GetDeviceDataAsync(request, CancellationToken.None);

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

        public async Task<LinkedList<VegaImpulsDeviceData>> GetImpulsDeviceDataAsync(string eui, long deviceId, DateTime from)
        {
            throw new NotImplementedException();
        }
    }
}