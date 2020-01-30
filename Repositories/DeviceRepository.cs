using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using VegaIoTApi.Data;
using VegaIoTApi.Data.Models;
using VegaIoTApi.Repositories.Interfaces;
using VegaIoTWebService.Data.Models;

namespace VegaIoTApi.Repositories
{
    public class DeviceRepository : IDeviceRepository
    {
        private readonly VegaApiDBContext _context;

        public DeviceRepository(VegaApiDBContext context)
        {
            _context = context;
        }

        public Task<List<VegaDevice>> GetDevicesAsync(CancellationToken token = default)
        {
            return _context.TempDevices.ToListAsync(token);
        }

        public Task<List<VegaDevice>> GetDevicesAsync(DeviceType deviceType, CancellationToken token = default)
        {
            return _context.TempDevices
                .Where(x => x.DeviceType == deviceType)
                .ToListAsync(token);
        }

        public async Task<VegaDevice?> GetDeviceAsync(long id, CancellationToken token = default)
        {
            var vegaTempDevice = await _context.TempDevices.FindAsync(new object[] { id }, token);
            return vegaTempDevice;
        }

        public async Task EditDeviceAsync(VegaDevice vegaTempDevice, CancellationToken token = default)
        {
            _context.Entry(vegaTempDevice).State = EntityState.Modified;
            await _context.SaveChangesAsync(token).ConfigureAwait(false);
        }

        public async Task<VegaDevice> AddDeviceAsync(VegaDevice tempDevice, CancellationToken token = default)
        {
            _context.TempDevices.Add(tempDevice);
            await _context.SaveChangesAsync(token).ConfigureAwait(false);
            return tempDevice;
        }

        public async Task<VegaDevice?> DeleteDeviceAsync(long id, CancellationToken token = default)
        {
            var vegaTempDevice = await _context.TempDevices.FindAsync(new object[] { id }, token);

            if (vegaTempDevice == null)
                return null;

            _context.TempDevices.Remove(vegaTempDevice);
            await _context.SaveChangesAsync(token).ConfigureAwait(false);

            return vegaTempDevice;
        }

        public bool DeviceExists(long id)
        {
            return _context.TempDevices.Any(e => e.Id == id);
        }
    }
}