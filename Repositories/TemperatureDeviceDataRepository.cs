using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using VegaIoTApi.Data;
using VegaIoTApi.Repositories.Interfaces;
using VegaIoTApi.Repositories.Utilities;
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

        public Task<List<VegaTempDeviceData>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return _context.TempDeviceData.ToListAsync(cancellationToken);
        }

        public Task<List<VegaTempDeviceData>?> GetAllAsync(long deviceId, CancellationToken cancellationToken = default)
        {
            if (!TempDeviceExists(deviceId))
            {
                return Task.FromResult<List<VegaTempDeviceData>?>(null);
            }
            return _context.TempDeviceData
                .Where(x => x.DeviceId == deviceId)
                .OrderBy(x => x.Uptime)
                .ToListAsync(cancellationToken);
        }

        public Task<List<VegaTempDeviceData>?> GetAllAsync(long deviceId, int startIndex, int limit, CancellationToken cancellationToken = default)
        {
            if (!TempDeviceExists(deviceId))
            {
                return Task.FromResult<List<VegaTempDeviceData>?>(null);
            }

            var query = from data in _context.TempDeviceData
                        where data.DeviceId == deviceId
                        orderby data.Uptime descending
                        select data;

            return query
                .Skip(startIndex)
                .Take(limit)
                .ToListAsync(cancellationToken);
        }

        public Task<List<VegaTempDeviceData>> GetCurrentAsync(CancellationToken token = default)
        {
            var dataIds = (from data in _context.TempDeviceData
                           group data by data.DeviceId into gData
                           select new { gData.Key, date = gData.Max(x => x.Uptime) })
                           .AsNoTracking();

            Expression<Func<VegaTempDeviceData, bool>> predict = x => false;

            foreach (var item in dataIds)
            {
                Expression<Func<VegaTempDeviceData, bool>> lambdaNew =
                    x => x.Uptime == item.date && x.DeviceId == item.Key;

                var lambdaBody = ExpressionReplacer
                    .ReplaceParameter(lambdaNew.Body, lambdaNew.Parameters[0], predict.Parameters[0]);

                var body = Expression.OrElse(predict.Body, lambdaBody);

                predict = Expression.Lambda<Func<VegaTempDeviceData, bool>>(body, predict.Parameters);
            }

            return _context.TempDeviceData.Where(predict).AsNoTracking().ToListAsync(token);
        }

        public Task<VegaTempDeviceData?> GetCurrentAsync(long deviceId, CancellationToken cancellationToken = default)
        {
            return (from data in _context.TempDeviceData
                    where data.DeviceId == deviceId
                    orderby data.Uptime descending
                    select data).FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<VegaTempDeviceData?> GetDataAsync(long id, CancellationToken cancellationToken = default)
        {
            return await _context.TempDeviceData.FindAsync(new object[] { id }, cancellationToken);
        }

        public async Task EditTempDeviceDataAsync(VegaTempDeviceData tempDeviceData, CancellationToken cancellationToken = default)
        {
            _context.Entry(tempDeviceData).State = EntityState.Modified;
            await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }

        public async Task<VegaTempDeviceData> AddTempDeviceDataAsync
            (VegaTempDeviceData tempDeviceData, CancellationToken cancellationToken = default)
        {
            _context.TempDeviceData.Add(tempDeviceData);
            await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            return tempDeviceData;
        }

        public async Task AddTempDeviceDataAsync(IEnumerable<VegaTempDeviceData> tempDeviceData, CancellationToken cancellationToken = default)
        {
            await _context.TempDeviceData.AddRangeAsync(tempDeviceData, cancellationToken).ConfigureAwait(false);
            await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }

        public Task<DateTimeOffset> GetLastUpdateTime(long deviceId, CancellationToken cancellationToken = default)
        {
            return (from dd in _context.TempDeviceData
                    where dd.DeviceId == deviceId
                    orderby dd.Uptime descending
                    select dd.Uptime).FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<VegaTempDeviceData?> DeleteVegaTempDeviceData
            (long id, CancellationToken cancellationToken = default)
        {
            var vegaTempDevice = await _context.TempDeviceData.FindAsync(new object[] { id }, cancellationToken);

            if (vegaTempDevice == null)
                return null;

            _context.TempDeviceData.Remove(vegaTempDevice);
            await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

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