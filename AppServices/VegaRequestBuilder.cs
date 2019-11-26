using System;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using VegaIoTApi.AppServices.Models;

namespace VegaIoTApi.AppServices
{
    public class VegaRequestBuilder
    {
        private readonly Uri connectionUri;
        private readonly List<Func<WebSocket, CancellationToken, Task>> actions;

        public VegaRequestBuilder(Uri connectionUri)
        {
            this.connectionUri = connectionUri;

            actions = new List<Func<WebSocket, CancellationToken, Task>>();
        }

        public VegaRequestBuilder AddAuthentication(string user, string password, Action<AuthenticationResp>? action)
        {
            const int responceBufferSice = 10000;
            var authenticationReuest = new AuthenticationReq()
            {
                Login = user,
                Password = password
            };

            Func<WebSocket, CancellationToken, Task> newAction = async (socket, token) =>
            {
                var result = await WebSocketRequest<AuthenticationReq, AuthenticationResp>
                    (authenticationReuest, socket, token, responceBufferSice);
                action?.Invoke(result);
            };

            actions.Add(newAction);
            return this;
        }

        public VegaRequest Build()
            => new VegaRequest(connectionUri, actions.ToArray());

        private static async Task<TResponse> WebSocketRequest<TRequest, TResponse>
            (TRequest request, WebSocket socket, CancellationToken cancellationToken, int reciveBufferSize)
        {
            var requestBytes = JsonSerializer.SerializeToUtf8Bytes(request);
            var reciveBytes = new byte[reciveBufferSize];

            await socket.SendAsync(requestBytes, WebSocketMessageType.Text, true, CancellationToken.None);
            var receiveResult = await socket.ReceiveAsync(reciveBytes, CancellationToken.None);

            return JsonSerializer.Deserialize<TResponse>(reciveBytes[..receiveResult.Count]);
        }
    }
}