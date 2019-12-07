using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using VegaIoTApi.Data;
using VegaIoTWebService.Data.Models;

namespace VegaIoTApi.Repositories
{
    public class MovingDeviceRepository : IMovingDeviceRepository
    {
        readonly VegaApiDBContext _context;

        public MovingDeviceRepository(VegaApiDBContext context)
        {
            _context = context;
        }
        public async Task<VegaTempDevice> AddMoveDeviceAsync(VegaTempDevice vegaTempDevice, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<VegaTempDevice?> DeleteVegaMoveDevice(long id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task EditMoveDeviceAsync(VegaTempDevice vegaTempDevice, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<VegaTempDevice?> GetMoveDeviceAsync(long id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<List<VegaTempDevice>> GetMoveDevicesAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public bool MoveDeviceExists(long id)
        {
            throw new NotImplementedException();
        }
    }
}