using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using VegaIoTWebService.Data.Models;

namespace VegaIoTApi.Repositories
{
    public interface IMagnetDeviceDataRepository
    {
        Task AddVegaMagnetDeviceDataAsync(IEnumerable<VegaMagnetDeviceData> moveDeviceData, CancellationToken cancellationToken = default);
        Task<VegaMagnetDeviceData> AddVegaMagnetDeviceDataAsync(VegaMagnetDeviceData moveDeviceData, CancellationToken cancellationToken = default);
        Task<VegaMagnetDeviceData?> DeleteVegaMagnetDeviceData(long id, CancellationToken cancellationToken = default);
        Task EditVegaDeviceDataAsync(VegaMagnetDeviceData vegaMoveDeviceData, CancellationToken cancellationToken = default);
        Task<List<VegaMagnetDeviceData>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<List<VegaMagnetDeviceData>?> GetAllAsync(long deviceId, CancellationToken cancellationToken = default);
        Task<List<VegaMagnetDeviceData>> GetCurrentAsync(CancellationToken cancellationToken = default);
        Task<VegaMagnetDeviceData?> GetCurrentAsync(long deviceId, CancellationToken cancellationToken = default);
        Task<VegaMagnetDeviceData?> GetDataAsync(long id, CancellationToken cancellationToken = default);
        Task<DateTimeOffset> GetLastUpdateTime(long deviceId, CancellationToken cancellationToken = default);
        bool MagnetDeviceDataExists(long id);
        bool MagnetDeviceExists(long deviceId);
    }
}