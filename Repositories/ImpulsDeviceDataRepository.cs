using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using VegaIoTApi.Data;
using VegaIoTApi.Repositories.Interfaces;
using VegaIoTWebService.Data.Models;

namespace VegaIoTApi.Repositories
{
    public class ImpulsDeviceDataRepository : DeviceDataRepositoryBase<VegaImpulsDeviceData>, IImpulsDeviceDataRepository
    {
        public ImpulsDeviceDataRepository(VegaApiDBContext context) 
            : base(context.ImpulsDeviceDatas, context)
        {
            if (context is null)
            {
                throw new ArgumentNullException(nameof(context));                
            }
        }
    }
}