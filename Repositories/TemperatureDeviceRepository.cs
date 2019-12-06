using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using VegaIoTApi.Data;
using VegaIoTWebService.Data.Models;

namespace VegaIoTApi.Repositories
{
    public class TemperatureDeviceRepository : ITemperatureDeviceRepository
    {
        private readonly VegaApiDBContext _context;

        public TemperatureDeviceRepository(VegaApiDBContext context)
        {
            _context = context;
        }

        public Task<List<VegaTempDevice>> GetTempDevicesAsync(CancellationToken token = default)
        {
            return _context.TempDevices.ToListAsync(token);
        }

        public async Task<VegaTempDevice?> GetTempDeviceAsync(long id, CancellationToken token = default)
        {
            var vegaTempDevice = await _context.TempDevices.FindAsync(new object[] { id }, token);
            return vegaTempDevice;
        }

        public async Task EditTempDeviceAsync(VegaTempDevice vegaTempDevice, CancellationToken token = default)
        {
            _context.Entry(vegaTempDevice).State = EntityState.Modified;
            await _context.SaveChangesAsync(token);
        }

        public async Task<VegaTempDevice> AddTempDeviceAsync(VegaTempDevice tempDevice, CancellationToken token = default)
        {
            _context.TempDevices.Add(tempDevice);
            await _context.SaveChangesAsync(token);
            return tempDevice;
        }

        public async Task<VegaTempDevice?> DeleteVegaTempDevice(long id, CancellationToken token = default)
        {
            var vegaTempDevice = await _context.TempDevices.FindAsync(new object[] { id }, token);

            if (vegaTempDevice == null)
            {
                return null;
            }

            _context.TempDevices.Remove(vegaTempDevice);
            await _context.SaveChangesAsync(token);

            return vegaTempDevice;
        }

        public bool TempDeviceExists(long id)
        {
            return _context.TempDevices.Any(e => e.Id == id);
        }
    }
}