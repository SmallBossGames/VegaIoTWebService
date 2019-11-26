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
        private readonly ClientWebSocket socket;

        public VegaApiCommunicator(Uri webSocketUri) 
        {
            _webSocketUri = webSocketUri;
            socket = new ClientWebSocket();
        }
        public async Task<AuthenticationResponseModel> AuthenticateAsync(AuthenticationRequestModel request)
        {
            if(socket.State != WebSocketState.Open)
                await socket.ConnectAsync(_webSocketUri, CancellationToken.None);

            var semaphore = new SemaphoreSlim(1,1);

            return await WebSocketRequest<AuthenticationResponseModel, AuthenticationRequestModel>(request, socket, semaphore);
        }

        private async Task<TResponse> WebSocketRequest<TResponse, TRequest>
            (TRequest request, WebSocket socket, SemaphoreSlim semaphore, int reciveBufferSize = 2048)
        {
            var requestBytes = JsonSerializer.SerializeToUtf8Bytes(request);
            var reciveBytes = new byte[reciveBufferSize];

            WebSocketReceiveResult receiveResult;
            await semaphore.WaitAsync();
            try
            {
                await socket.SendAsync(requestBytes, WebSocketMessageType.Text, true, CancellationToken.None);
                receiveResult = await socket.ReceiveAsync(reciveBytes, CancellationToken.None);
            }
            finally
            {
                semaphore.Release();
            }
            
            return JsonSerializer.Deserialize<TResponse>(reciveBytes[..receiveResult.Count]);
        }
    }
}