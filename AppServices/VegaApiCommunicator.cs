using System;
using System.Net.WebSockets;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using VegaIoTWebService.AppServices.Models;

namespace VegaIoTApi.AppServices 
{
    public class VegaApiCommunicator : IVegaApiCommunicator
    {
        private readonly Uri _webSocketUri;
        public VegaApiCommunicator() {}

        public VegaApiCommunicator(Uri webSocketUri) 
        {
            _webSocketUri = webSocketUri;
        }
        public Task<AuthenticationResponseModel> AuthenticateAsync(AuthenticationRequestModel request)
        {
            return WebSocketRequest<AuthenticationResponseModel, AuthenticationRequestModel>(request);
        }

        public async Task<TResponse> WebSocketRequest<TResponse, TRequest>(TRequest request, int reciveBufferSize = 2048)
        {
            using var socket = new ClientWebSocket();
            await socket.ConnectAsync(_webSocketUri, CancellationToken.None);

            var requestBytes = JsonSerializer.SerializeToUtf8Bytes(request);

            await socket.SendAsync(requestBytes, WebSocketMessageType.Text, true, CancellationToken.None);

            var reciveBytes = new byte[reciveBufferSize];

            var reciveResult = await socket.ReceiveAsync(reciveBytes, CancellationToken.None);

            return JsonSerializer.Deserialize<TResponse>(reciveBytes[..reciveResult.Count]);
        }
    }
}