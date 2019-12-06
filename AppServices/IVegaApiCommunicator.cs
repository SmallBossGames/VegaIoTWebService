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
            (AuthenticationReq request, CancellationToken cancellationToken = default);
        Task<DeviceDataResp> GetDeviceDataAsync
            (DeviceDataReq request, CancellationToken cancellationToken = default);
        Task<LinkedList<VegaTempDeviceData>> GetTemperatureDeviceDatasAsync
            (string eui, long deviceId, DateTimeOffset from, CancellationToken cancellationToken = default);
        Task<LinkedList<VegaMoveDeviceData>> GetMoveDeviceDataAsync
            (string eui, long deviceId, DateTimeOffset from, CancellationToken cancellationToken = default);
        Task<LinkedList<VegaMagnetDeviceData>> GetMagnetDeviceDataAsync
            (string eui, long deviceId, DateTimeOffset from, CancellationToken cancellationToken = default);
        Task<LinkedList<VegaImpulsDeviceData>> GetImpulsDeviceDataAsync
            (string eui, long deviceId, DateTimeOffset from, CancellationToken cancellationToken = default);
    }
}