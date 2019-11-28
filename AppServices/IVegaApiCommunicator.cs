using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using VegaIoTApi.AppServices.Models;
using VegaIoTWebService.Data.Models;

namespace VegaIoTApi.AppServices
{
    public interface IVegaApiCommunicator
    {
        Task<AuthenticationResp> AuthenticateAsync
            (AuthenticationReq request, CancellationToken cancellationToken);
        Task<DeviceDataResp> GetDeviceDataAsync
            (DeviceDataReq request, CancellationToken cancellationToken);
        Task<LinkedList<VegaTempDeviceData>> GetTemperatureDeviceDatasAsync(string eui, DateTime from);
    }
}