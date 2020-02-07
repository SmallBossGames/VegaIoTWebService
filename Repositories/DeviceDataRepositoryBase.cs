using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using VegaIoTApi.Data;
using VegaIoTApi.Data.Models.Interfaces;
using VegaIoTApi.Repositories.Interfaces;
using VegaIoTApi.Repositories.Utilities;

namespace VegaIoTApi.Repositories
{
    public class DeviceDataRepositoryBase<T> : IDeviceDataRepository<T> where T: class, IDeviceData
    {
        private readonly DbSet<T> _dataSet;
        private VegaApiDBContext _context;

        protected DeviceDataRepositoryBase(DbSet<T> dataSet, VegaApiDBContext context)
        {
            _dataSet = dataSet ?? throw new ArgumentNullException(nameof(dataSet));
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task AddAsync(IEnumerable<T> impulsDeviceData, CancellationToken cancellationToken = default)
        {
            await _dataSet.AddRangeAsync(impulsDeviceData, cancellationToken).ConfigureAwait(false);
            await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }

        public async Task<T> AddAsync(T impulsDeviceData, CancellationToken cancellationToken = default)
        {
            _dataSet.Add(impulsDeviceData);
            await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            return impulsDeviceData;
        }

        public async Task<T> DeleteAsync(long id, CancellationToken cancellationToken = default)
        {
            var deviceData = await _dataSet
                .FindAsync(new object[] { id }, cancellationToken);

            if (deviceData == null)
                return null;

            _dataSet.Remove(deviceData);

            await _context
                .SaveChangesAsync(cancellationToken)
                .ConfigureAwait(false);

            return deviceData;
        }

        public bool DeviceExists(long deviceId)
        {
            return _context.TempDevices.Any(e => e.Id == deviceId);
        }

        public async Task EditAsync(T vegaImpulsDeviceData, CancellationToken cancellationToken = default)
        {
            _context.Entry(vegaImpulsDeviceData).State = EntityState.Modified;
            await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }

        public async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _dataSet
               .AsNoTracking()
               .ToListAsync(cancellationToken)
               .ConfigureAwait(false);
        }

        public async Task<IEnumerable<T>?> GetAllAsync(long deviceId, CancellationToken cancellationToken = default)
        {
            if (!DeviceExists(deviceId))
            {
                return null;
            }

            var result = await _dataSet
                .Where(x => x.DeviceId == deviceId)
                .OrderBy(x => x.Uptime)
                .AsNoTracking()
                .ToArrayAsync(cancellationToken)
                .ConfigureAwait(false);

            return result;
        }

        public async Task<IEnumerable<T>?> GetAllAsync(long deviceId, int startIndex, int limit, CancellationToken cancellationToken = default)
        {
            if (!DeviceExists(deviceId))
            {
                return null;
            }

            var query = from data in _dataSet
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

        public async Task<T> GetAsync(long id, CancellationToken cancellationToken = default)
        {
            return await _dataSet
                .FindAsync(new object[] { id }, cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<IEnumerable<T>> GetCurrentAsync(CancellationToken cancellationToken = default)
        {
            var dataIds = (from data in _dataSet
                           group data by data.DeviceId into gData
                           select new { gData.Key, date = gData.Max(x => x.Uptime) })
                            .AsNoTracking();

            Expression<Func<T, bool>> predict = x => false;

            foreach (var item in dataIds)
            {
                Expression<Func<T, bool>> lambdaNew =
                    x => x.Uptime == item.date && x.DeviceId == item.Key;

                var lambdaBody = ExpressionReplacer
                    .ReplaceParameter(lambdaNew.Body, lambdaNew.Parameters[0], predict.Parameters[0]);

                var body = Expression.OrElse(predict.Body, lambdaBody);

                predict = Expression.Lambda<Func<T, bool>>(body, predict.Parameters);
            }

            return await _dataSet
                .Where(predict)
                .AsNoTracking()
                .ToListAsync(cancellationToken)
                .ConfigureAwait(false);
        }

        public Task<T> GetCurrentAsync(long deviceId, CancellationToken cancellationToken = default)
        {
            return (from data in _dataSet
                    where data.DeviceId == deviceId
                    orderby data.Uptime descending
                    select data).FirstOrDefaultAsync(cancellationToken);
        }

        public Task<DateTimeOffset> GetUptimeAsync(long deviceId, CancellationToken cancellationToken = default)
        {
            return (from dd in _dataSet
                    where dd.DeviceId == deviceId
                    orderby dd.Uptime descending
                    select dd.Uptime).FirstOrDefaultAsync(cancellationToken);
        }

        public bool ImpulsDeviceDataExists(long id)
        {
            return _dataSet.Any(e => e.Id == id);
        }
    }
}
