using System;
using System.Net.WebSockets;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using VegaIoTApi.AppServices.Models;

namespace VegaIoTWebService.Data
{
    public class VegaWebSocketApiContext : IDisposable, IVegaWebSocketApiContext
    {
        private struct PingModel
        {
            [JsonPropertyName("cmd")]
            public string Cmd { get; set; }
        }

        private struct TokenAuthReq
        {
            public string Cmd => "token_auth_req";
            public string Token { get; set; }
        }

        private struct TokenAuthResp
        {
            public string Cmd { get; set; }
            public bool Status { get; set; }
            [JsonPropertyName("err_string")]
            public string? ErrorString { get; set; }
            public string Token { get; set; }
        }


        private readonly ClientWebSocket socket;
        private readonly Uri connectionUri;
        private readonly string user;
        private readonly string password;
        private string connectionToken;

        public VegaWebSocketApiContext(
            Uri connectionUri,
            string user,
            string password)
        {
            this.socket = new ClientWebSocket();
            this.connectionUri = connectionUri;
            this.user = user;
            this.password = password;
            connectionToken = string.Empty;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public async Task<TResponse> Request<TRequest, TResponse>(
            TRequest request,
            CancellationToken cancellationToken,
            int reciveBufferSize = 4096)
        {
            await Connection(cancellationToken).ConfigureAwait(false);

            return await WebSocketRequest<TRequest, TResponse>
                (request, cancellationToken, reciveBufferSize).ConfigureAwait(false);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
                socket.Dispose();
        }

        private Task Connection(CancellationToken cancellationToken)
        {
            return socket.State switch
            {
                WebSocketState.Aborted => throw new NotImplementedException(),
                WebSocketState.Closed => ClosedBehaveur(cancellationToken),
                WebSocketState.CloseReceived => throw new NotImplementedException(),
                WebSocketState.CloseSent => throw new NotImplementedException(),
                WebSocketState.Connecting => throw new NotImplementedException(),
                WebSocketState.None => ClosedBehaveur(cancellationToken),
                WebSocketState.Open => OpenBehaveur(cancellationToken),
                _ => throw new NotImplementedException(),
            };
        }

        private async Task OpenBehaveur(CancellationToken cancellationToken)
        {
            if (!await PingAsync(cancellationToken).ConfigureAwait(false))
            {
                socket.Abort();
                await socket.ConnectAsync(connectionUri, cancellationToken)
                    .ConfigureAwait(false);
                if (socket.State != WebSocketState.Open || !await PingAsync(cancellationToken).ConfigureAwait(false))
                    throw new InvalidOperationException();
            }

            if (await FastConnectionRecover(cancellationToken).ConfigureAwait(false))
                return;

            if (await SetupConnection(cancellationToken).ConfigureAwait(false))
                return;

            throw new InvalidOperationException();
        }

        private async Task ClosedBehaveur(CancellationToken cancellationToken)
        {
            await socket.ConnectAsync(connectionUri, cancellationToken).ConfigureAwait(false);

            if (socket.State != WebSocketState.Open || !await PingAsync(cancellationToken).ConfigureAwait(false))
                throw new InvalidOperationException();

            if (await SetupConnection(cancellationToken).ConfigureAwait(false))
                return;

            throw new InvalidOperationException();
        }

        private async Task<bool> SetupConnection(CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(user) || string.IsNullOrEmpty(password))
                return false;

            var request = new AuthenticationReq()
            {
                Login = user,
                Password = password,
            };

            var responce = await WebSocketRequest<AuthenticationReq, AuthenticationResp>
                (request, cancellationToken).ConfigureAwait(false);

            if (responce.Cmd == "auth_resp" && responce.Status == true && !string.IsNullOrEmpty(responce.Token))
            {
                connectionToken = responce.Token;
                return true;
            }

            return false;
        }

        private async Task<bool> PingAsync(CancellationToken cancellationToken)
        {
            try
            {
                var pingReq = new PingModel() { Cmd = "ping_req" };
                var result = await WebSocketRequest<PingModel, PingModel>(pingReq, cancellationToken)
                    .ConfigureAwait(false);

                return result.Cmd == "ping_resp";
            }
            catch (InvalidOperationException)
            {
                return false;
            }
        }

        private async Task<bool> FastConnectionRecover(CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(connectionToken))
                return false;

            var request = new TokenAuthReq()
            {
                Token = connectionToken
            };

            var result = await WebSocketRequest<TokenAuthReq, TokenAuthResp>
                (request, cancellationToken).ConfigureAwait(false);

            if (result.Status == true && result.Cmd == "token_auth_resp")
            {
                connectionToken = result.Token;
                return true;
            }

            return false;
        }

        private async Task<TResponse> WebSocketRequest<TRequest, TResponse>(
            TRequest request,
            CancellationToken cancellationToken,
            int reciveBufferSize = 2048)
        {
            var requestBytes = JsonSerializer.SerializeToUtf8Bytes(request);
            var reciveBytes = new byte[reciveBufferSize];

            await socket.SendAsync(requestBytes, WebSocketMessageType.Text, true, cancellationToken)
                .ConfigureAwait(false);
           
            var receiveResult = await socket.ReceiveAsync(reciveBytes, cancellationToken)
                .ConfigureAwait(false);

            return JsonSerializer.Deserialize<TResponse>(reciveBytes[..receiveResult.Count]);
        }
    }
}