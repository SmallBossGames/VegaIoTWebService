using System.Threading;
using System.Threading.Tasks;

namespace VegaIoTWebService.Data
{
    public interface IVegaWebSocketApiContext
    {
        Task<TResponse> Request<TRequest, TResponse>(TRequest request, CancellationToken cancellationToken, int reciveBufferSize = 4096);
    }
}