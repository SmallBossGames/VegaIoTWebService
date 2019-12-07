using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using VegaIoTApi.Data;
using VegaIoTWebService.Data.Models;

namespace VegaIoTApi.Repositories
{
    public class MagnetDeviceRepository : IMagnetDeviceRepository
    {
        readonly VegaApiDBContext _context;

        public MagnetDeviceRepository(VegaApiDBContext context)
        {
            _context = context;
        }
        public async Task<VegaTempDevice> AddMagnetDeviceAsync(VegaTempDevice vegaTempDevice, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<VegaTempDevice?> DeleteVegaMagnetDevice(long id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task EditMagnetDeviceAsync(VegaTempDevice vegaTempDevice, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<VegaTempDevice?> GetMagnetDeviceAsync(long id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<List<VegaTempDevice>> GetMagnetDevicesAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public bool MagnetDevicesExists(long id)
        {
            throw new NotImplementedException();
        }
    }
}