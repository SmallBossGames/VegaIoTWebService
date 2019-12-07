using System;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;

namespace VegaIoTApi.AppServices
{
    public class VegaRequest
    {
        private readonly Uri connectionUri;
        private readonly Func<WebSocket, CancellationToken, Task>[] actions;

        public VegaRequest(Uri connectionUri, Func<WebSocket, CancellationToken, Task>[] actions)
        {
            this.connectionUri = connectionUri;
            this.actions = actions;
        }

        public async Task RunAsync(CancellationToken token)
        {
            using var socket = new ClientWebSocket();
            await socket.ConnectAsync(connectionUri, token).ConfigureAwait(false);

            foreach (var action in actions)
            {
                if (action != null)
                    await action(socket, token).ConfigureAwait(false);
            }
        }

        public Task RunAsync()
            => RunAsync(CancellationToken.None);
    }
}