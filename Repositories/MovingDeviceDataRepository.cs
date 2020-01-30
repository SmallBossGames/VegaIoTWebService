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
    public class MovingDeviceDataRepository : IMovingDeviceDataRepository
    {
        readonly VegaApiDBContext _context;

        public MovingDeviceDataRepository(VegaApiDBContext context)
        {
            _context = context;
        }
        public async Task AddVegaMovingDeviceDataAsync(IEnumerable<VegaMoveDeviceData> moveDeviceData, CancellationToken cancellationToken = default)
        {
            await _context.MoveDeviceDatas.AddRangeAsync(moveDeviceData, cancellationToken).ConfigureAwait(false);
            await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }

        public async Task<VegaMoveDeviceData> AddVegaMovingDeviceDataAsync(VegaMoveDeviceData moveDeviceData, CancellationToken cancellationToken = default)
        {
            _context.MoveDeviceDatas.Add(moveDeviceData);
            await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            return moveDeviceData;
        }

        public async Task<VegaMoveDeviceData?> DeleteVegaMovingDeviceData(long id, CancellationToken cancellationToken = default)
        {
            var deviceData = await _context.MoveDeviceDatas
                .FindAsync(new object[] { id }, cancellationToken);

            if (deviceData == null)
                return null;

            _context.MoveDeviceDatas.Remove(deviceData);
            
            await _context
                .SaveChangesAsync(cancellationToken)
                .ConfigureAwait(false);

            return deviceData;
        }

        public async Task EditVegaDeviceDataAsync(VegaMoveDeviceData vegaMoveDeviceData, CancellationToken cancellationToken = default)
        {
            _context.Entry(vegaMoveDeviceData).State = EntityState.Modified;
            await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }

        public async Task<IEnumerable<VegaMoveDeviceData>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context.MoveDeviceDatas
                .AsNoTracking()
                .ToListAsync(cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<IEnumerable<VegaMoveDeviceData>?> GetAllAsync(long deviceId, CancellationToken cancellationToken = default)
        {
            if(!DeviceExists(deviceId))
            {
                return null;
            }

            var result = await _context.MoveDeviceDatas
                .Where(x => x.DeviceId == deviceId)
                .OrderBy(x => x.Uptime)
                .AsNoTracking()
                .ToArrayAsync(cancellationToken)
                .ConfigureAwait(false);

            return result;
        }

        public async Task<IEnumerable<VegaMoveDeviceData>?> GetAllAsync(long deviceId, int startIndex, int limit, CancellationToken cancellationToken = default)
        {
            if (!DeviceExists(deviceId))
            {
                return null;
            }

            var query = from data in _context.MoveDeviceDatas
                        where data.DeviceId == deviceId
                        orderby data.Uptime descending
                        select data;

            return await query
                .AsNoTracking()
                .Skip(startIndex)
                .Take(limit)
                .ToListAsync(cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<IEnumerable<VegaMoveDeviceData>> GetCurrentAsync(CancellationToken cancellationToken = default)
        {
            var dataIds = (from data in _context.MoveDeviceDatas
                           group data by data.DeviceId into gData
                           select new { gData.Key, date = gData.Max(x => x.Uptime) })
                           .AsNoTracking();

            Expression<Func<VegaMoveDeviceData, bool>> predict = x => false;

            foreach (var item in dataIds)
            {
                Expression<Func<VegaMoveDeviceData, bool>> lambdaNew =
                    x => x.Uptime == item.date && x.DeviceId == item.Key;

                var lambdaBody = ExpressionReplacer
                    .ReplaceParameter(lambdaNew.Body, lambdaNew.Parameters[0], predict.Parameters[0]);

                var body = Expression.OrElse(predict.Body, lambdaBody);

                predict = Expression.Lambda<Func<VegaMoveDeviceData, bool>>(body, predict.Parameters);
            }

            return await _context.MoveDeviceDatas
                .Where(predict)
                .AsNoTracking()
                .ToListAsync(cancellationToken)
                .ConfigureAwait(false);
        }

        public Task<VegaMoveDeviceData?> GetCurrentAsync(long deviceId, CancellationToken cancellationToken = default)
        {
            return (from data in _context.MoveDeviceDatas
                    where data.DeviceId == deviceId
                    orderby data.Uptime descending
                    select data).FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<VegaMoveDeviceData?> GetDataAsync(long id, CancellationToken cancellationToken = default)
        {
            return await _context.MoveDeviceDatas
                .FindAsync(new object[] { id }, cancellationToken)
                .ConfigureAwait(false);
        }

        public Task<DateTimeOffset> GetLastUpdateTime(long deviceId, CancellationToken cancellationToken = default)
        {
            return (from dd in _context.MoveDeviceDatas
                    where dd.DeviceId == deviceId
                    orderby dd.Uptime descending
                    select dd.Uptime).FirstOrDefaultAsync(cancellationToken);
        }

        public bool MoveDeviceDataExists(long id)
        {
            return _context.MoveDeviceDatas.Any(e => e.Id == id);
        }

        public bool DeviceExists(long deviceId)
        {
            return _context.TempDevices.Any(e => e.Id == deviceId);
        }
    }
}