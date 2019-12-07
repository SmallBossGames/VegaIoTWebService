using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using VegaIoTApi.Data;
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

        public Task<List<VegaTempDevice>> GetDevicesAsync(CancellationToken token = default)
        {
            return _context.TempDevices.ToListAsync(token);
        }

        public async Task<VegaTempDevice?> GetDeviceAsync(long id, CancellationToken token = default)
        {
            var vegaTempDevice = await _context.TempDevices.FindAsync(new object[] { id }, token);
            return vegaTempDevice;
        }

        public async Task EditDeviceAsync(VegaTempDevice vegaTempDevice, CancellationToken token = default)
        {
            _context.Entry(vegaTempDevice).State = EntityState.Modified;
            await _context.SaveChangesAsync(token);
        }

        public async Task<VegaTempDevice> AddDeviceAsync(VegaTempDevice tempDevice, CancellationToken token = default)
        {
            _context.TempDevices.Add(tempDevice);
            await _context.SaveChangesAsync(token);
            return tempDevice;
        }

        public async Task<VegaTempDevice?> DeleteDeviceAsync(long id, CancellationToken token = default)
        {
            var vegaTempDevice = await _context.TempDevices.FindAsync(new object[] { id }, token);

            if (vegaTempDevice == null)
                return null;

            _context.TempDevices.Remove(vegaTempDevice);
            await _context.SaveChangesAsync(token);

            return vegaTempDevice;
        }

        public bool DeviceExists(long id)
        {
            return _context.TempDevices.Any(e => e.Id == id);
        }
    }
}