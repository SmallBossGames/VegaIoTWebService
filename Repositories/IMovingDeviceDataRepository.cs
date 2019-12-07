using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using VegaIoTWebService.Data.Models;

namespace VegaIoTApi.Repositories
{
    public interface IMovingDeviceDataRepository
    {
        Task AddVegaMovingDeviceDataAsync(IEnumerable<VegaMoveDeviceData> moveDeviceData, CancellationToken cancellationToken = default);
        Task<VegaMoveDeviceData> AddVegaMovingDeviceDataAsync(VegaMoveDeviceData moveDeviceData, CancellationToken cancellationToken = default);
        Task<VegaMoveDeviceData?> DeleteVegaMovingDeviceData(long id, CancellationToken cancellationToken = default);
        Task EditVegaDeviceDataAsync(VegaMoveDeviceData vegaMoveDeviceData, CancellationToken cancellationToken = default);
        Task<List<VegaMoveDeviceData>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<List<VegaMoveDeviceData>?> GetAllAsync(long deviceId, CancellationToken cancellationToken = default);
        Task<List<VegaMoveDeviceData>> GetCurrentAsync(CancellationToken cancellationToken = default);
        Task<VegaMoveDeviceData?> GetCurrentAsync(long deviceId, CancellationToken cancellationToken = default);
        Task<VegaMoveDeviceData?> GetDataAsync(long id, CancellationToken cancellationToken = default);
        Task<DateTimeOffset> GetLastUpdateTime(long deviceId, CancellationToken cancellationToken = default);
        bool MoveDeviceDataExists(long id);
        bool MoveDeviceExists(long deviceId);
    }
}
