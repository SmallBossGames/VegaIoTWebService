using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using VegaIoTWebService.Data.Models;

namespace VegaIoTApi.Repositories.Interfaces
{
    public interface ITemperatureDeviceDataRepository
    {
        Task AddTempDeviceDataAsync(IEnumerable<VegaTempDeviceData> tempDeviceData, CancellationToken cancellationToken = default);
        Task<VegaTempDeviceData> AddTempDeviceDataAsync(VegaTempDeviceData tempDeviceData, CancellationToken cancellationToken = default);
        Task<VegaTempDeviceData?> DeleteVegaTempDeviceData(long id, CancellationToken cancellationToken = default);
        Task EditTempDeviceDataAsync(VegaTempDeviceData tempDeviceData, CancellationToken cancellationToken = default);
        Task<List<VegaTempDeviceData>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<List<VegaTempDeviceData>?> GetAllAsync(long deviceId, CancellationToken cancellationToken = default);
        Task<List<VegaTempDeviceData>> GetCurrentAsync(CancellationToken cancellationToken = default);
        Task<VegaTempDeviceData?> GetCurrentAsync(long deviceId, CancellationToken cancellationToken = default);
        Task<VegaTempDeviceData?> GetDataAsync(long id, CancellationToken cancellationToken = default);
        Task<DateTimeOffset> GetLastUpdateTime(long deviceId, CancellationToken cancellationToken = default);
        bool TempDeviceDataExists(long id);
        bool TempDeviceExists(long deviceId);
    }
}