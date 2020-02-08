using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using VegaIoTApi.Data.Models.Interfaces;

namespace VegaIoTApi.Repositories.Interfaces
{
    public interface IDeviceDataRepository<TDeviceData> where TDeviceData : IDeviceData
    {
        Task AddAsync(IEnumerable<TDeviceData> impulsDeviceData, CancellationToken cancellationToken = default);
        Task<TDeviceData> AddAsync(TDeviceData impulsDeviceData, CancellationToken cancellationToken = default);
        Task<TDeviceData> DeleteAsync(long id, CancellationToken cancellationToken = default);
        Task<TDeviceData> GetAsync(long id, CancellationToken cancellationToken = default);
        Task EditAsync(TDeviceData vegaImpulsDeviceData, CancellationToken cancellationToken = default);
        Task<IEnumerable<TDeviceData>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<IEnumerable<TDeviceData>?> GetAllAsync(long deviceId, CancellationToken cancellationToken = default);
        Task<IEnumerable<TDeviceData>?> GetAllAsync(long deviceId, int startIndex, int limit, CancellationToken cancellationToken = default);
        Task<IEnumerable<TDeviceData>?> GetAllAsync(long deviceId, DateTimeOffset startTime, DateTimeOffset endTime, CancellationToken cancellationToken = default);
        Task<IEnumerable<TDeviceData>> GetCurrentAsync(CancellationToken cancellationToken = default);
        Task<TDeviceData> GetCurrentAsync(long deviceId, CancellationToken cancellationToken = default);
        Task<DateTimeOffset> GetUptimeAsync(long deviceId, CancellationToken cancellationToken = default);
        bool ImpulsDeviceDataExists(long id);
        bool DeviceExists(long deviceId);
    }
}
