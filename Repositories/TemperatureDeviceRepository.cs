using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public Task<List<VegaTempDevice>> GetTempDevicesAsync()
        {
            return _context.TempDevices.ToListAsync();
        }

        public async Task<VegaTempDevice?> GetTempDeviceAsync(long id)
        {
            var vegaTempDevice = await _context.TempDevices.FindAsync(id);
            return vegaTempDevice;
        }

        public async Task EditTempDeviceAsync(VegaTempDevice vegaTempDevice)
        {
            _context.Entry(vegaTempDevice).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<VegaTempDevice> AddTempDeviceAsync(VegaTempDevice tempDevice)
        {
            _context.TempDevices.Add(tempDevice);
            await _context.SaveChangesAsync();
            return tempDevice;
        }

        public async Task<VegaTempDevice?> DeleteVegaTempDevice(long id)
        {
            var vegaTempDevice = await _context.TempDevices.FindAsync(id);

            if (vegaTempDevice == null)
            {
                return null;
            }

            _context.TempDevices.Remove(vegaTempDevice);
            await _context.SaveChangesAsync();

            return vegaTempDevice;
        }

        public bool TempDeviceExists(long id)
        {
            return _context.TempDevices.Any(e => e.Id == id);
        }
    }
}