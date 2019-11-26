using System.Threading;
using System.Threading.Tasks;
using VegaIoTApi.AppServices.Models;

namespace VegaIoTApi.AppServices
{
    public interface IVegaApiCommunicator
    {
        Task<AuthenticationResp> AuthenticateAsync
            (AuthenticationReq request, CancellationToken cancellationToken);
        Task<DeviceDataResp> GetDeviceDataAsync
            (DeviceDataReq request, CancellationToken cancellationToken);
        //VegaRequestBuilder BuildRequest();
    }
}