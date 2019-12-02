using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VegaIoTApi.Data;
using VegaIoTWebService.Data.Models;

namespace VegaIoTApi.Repositories
{
    public class TemperatureDeviceDataRepository : ITemperatureDeviceDataRepository
    {
        private readonly VegaApiDBContext _context;

        public TemperatureDeviceDataRepository(VegaApiDBContext context)
        {
            _context = context;
        }

        public Task<List<VegaTempDeviceData>> GetTempDeviceDatasAsync()
        {
            return _context.TempDeviceData.ToListAsync();
        }

        public Task<List<VegaTempDeviceData>?> GetTempDeviceDatasAsync(long deviceId)
        {
            if (!TempDeviceExists(deviceId))
            {
                return Task.FromResult<List<VegaTempDeviceData>?>(null);
            }
            return _context.TempDeviceData.Where(x => x.DeviceId == deviceId).ToListAsync();
        }

        public async Task<VegaTempDeviceData?> GetTempDeviceDataAsync(long id)
        {
            return await _context.TempDeviceData.FindAsync(id); ;
        }

        public async Task EditTempDeviceDataAsync(VegaTempDeviceData tempDeviceData)
        {
            _context.Entry(tempDeviceData).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<VegaTempDeviceData> AddTempDeviceDataAsync(VegaTempDeviceData tempDeviceData)
        {
            _context.TempDeviceData.Add(tempDeviceData);
            await _context.SaveChangesAsync();
            return tempDeviceData;
        }

        public async Task AddTempDeviceDataAsync(IEnumerable<VegaTempDeviceData> tempDeviceData)
        {
            await _context.TempDeviceData.AddRangeAsync(tempDeviceData);
            await _context.SaveChangesAsync();
        }

        public Task<DateTime> GetLastUpdateTime(long deviceId)
        {
            return (from dd in _context.TempDeviceData
                    where dd.DeviceId == deviceId
                    orderby dd.Uptime descending
                    select dd.Uptime).FirstOrDefaultAsync();
        }

        public async Task<VegaTempDeviceData?> DeleteVegaTempDeviceData(long id)
        {
            var vegaTempDevice = await _context.TempDeviceData.FindAsync(id);

            if (vegaTempDevice == null)
            {
                return null;
            }

            _context.TempDeviceData.Remove(vegaTempDevice);
            await _context.SaveChangesAsync();

            return vegaTempDevice;
        }

        public bool TempDeviceDataExists(long id)
        {
            return _context.TempDeviceData.Any(e => e.Id == id);
        }

        public bool TempDeviceExists(long deviceId)
        {
            return _context.TempDevices.Any(e => e.Id == deviceId);
        }
    }
}