using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using VegaIoTApi.Data;
using VegaIoTApi.Repositories.Interfaces;
using VegaIoTWebService.Data.Models;

namespace VegaIoTApi.Repositories
{
    public class MovingDeviceDataRepository : IMovingDeviceDataRepository
    {
        readonly VegaApiDBContext _context;

        public MovingDeviceDataRepository(VegaApiDBContext context)
        {
            _context = context;
        }
        public async Task AddVegaMovingDeviceDataAsync(IEnumerable<VegaMoveDeviceData> moveDeviceData, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<VegaMoveDeviceData> AddVegaMovingDeviceDataAsync(VegaMoveDeviceData moveDeviceData, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<VegaMoveDeviceData?> DeleteVegaMovingDeviceData(long id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task EditVegaDeviceDataAsync(VegaMoveDeviceData vegaMoveDeviceData, CancellationToken cancellationToken = default)
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

        public async Task<VegaMoveDeviceData?> GetDataAsync(long id, CancellationToken cancellationToken = default)
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