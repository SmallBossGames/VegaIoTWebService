using System;
using System.Net.WebSockets;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using VegaIoTApi.AppServices.Models;

namespace VegaIoTApi.AppServices
{
    public class VegaApiCommunicator : IVegaApiCommunicator
    {
        private readonly Uri _webSocketUri;

        public VegaApiCommunicator(Uri webSocketUri)
        {
            _webSocketUri = webSocketUri;
        }

        public VegaRequestBuilder BuildRequest() => new VegaRequestBuilder(_webSocketUri);

        public async Task<AuthenticationResp> AuthenticateAsync
            (AuthenticationReq request, CancellationToken cancellationToken)
        {
            using var socket = new ClientWebSocket();
            await socket.ConnectAsync(_webSocketUri, cancellationToken);
            return await WebSocketRequest<AuthenticationReq, AuthenticationResp>(request, socket);
        }

        public async Task<DeviceDataResp> GetDeviceDataAsync
            (DeviceDataReq request, CancellationToken cancellationToken)
        {
            using var socket = new ClientWebSocket();
            await socket.ConnectAsync(_webSocketUri, cancellationToken);
            return await WebSocketRequest<DeviceDataReq, DeviceDataResp>(request, socket, 10000);
        }

        private async Task<TResponse> WebSocketRequest<TRequest, TResponse>
            (TRequest request, WebSocket socket, int reciveBufferSize = 2048)
        {
            var requestBytes = JsonSerializer.SerializeToUtf8Bytes(request);
            var reciveBytes = new byte[reciveBufferSize];

            await socket.SendAsync(requestBytes, WebSocketMessageType.Text, true, CancellationToken.None);
            var receiveResult = await socket.ReceiveAsync(reciveBytes, CancellationToken.None);


            return JsonSerializer.Deserialize<TResponse>(reciveBytes[..receiveResult.Count]);
        }
    }
}