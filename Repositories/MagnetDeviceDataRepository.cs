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
    public class MagnetDeviceDataRepository : IMagnetDeviceDataRepository
    {
        readonly VegaApiDBContext _context;

        public MagnetDeviceDataRepository(VegaApiDBContext context)
        {
            _context = context;
        }
        public Task AddVegaMagnetDeviceDataAsync(IEnumerable<VegaMagnetDeviceData> moveDeviceData, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<VegaMagnetDeviceData> AddVegaMagnetDeviceDataAsync(VegaMagnetDeviceData moveDeviceData, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<VegaMagnetDeviceData?> DeleteVegaMagnetDeviceData(long id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task EditVegaDeviceDataAsync(VegaMagnetDeviceData vegaMoveDeviceData, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<List<VegaMagnetDeviceData>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<List<VegaMagnetDeviceData>?> GetAllAsync(long deviceId, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<List<VegaMagnetDeviceData>> GetCurrentAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<VegaMagnetDeviceData?> GetCurrentAsync(long deviceId, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<VegaMagnetDeviceData?> GetDataAsync(long id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<DateTimeOffset> GetLastUpdateTime(long deviceId, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public bool MagnetDeviceDataExists(long id)
        {
            throw new NotImplementedException();
        }

        public bool MagnetDeviceExists(long deviceId)
        {
            throw new NotImplementedException();
        }
    }
}