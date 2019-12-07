using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using VegaIoTWebService.Data.Models;

namespace VegaIoTApi.Repositories
{
    public class MovingDeviceDataRepository : IMovingDeviceDataRepository
    {
        public Task AddVegaMovingDeviceDataAsync(IEnumerable<VegaMoveDeviceData> moveDeviceData, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<VegaMoveDeviceData> AddVegaMovingDeviceDataAsync(VegaMoveDeviceData moveDeviceData, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<VegaMoveDeviceData?> DeleteVegaMovingDeviceData(long id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task EditVegaDeviceDataAsync(VegaMoveDeviceData vegaMoveDeviceData, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<List<VegaMoveDeviceData>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<List<VegaMoveDeviceData>?> GetAllAsync(long deviceId, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<List<VegaMoveDeviceData>> GetCurrentAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<VegaMoveDeviceData?> GetCurrentAsync(long deviceId, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<VegaMoveDeviceData?> GetDataAsync(long id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<DateTimeOffset> GetLastUpdateTime(long deviceId, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public bool MoveDeviceDataExists(long id)
        {
            throw new NotImplementedException();
        }

        public bool MoveDeviceExists(long deviceId)
        {
            throw new NotImplementedException();
        }
    }
}