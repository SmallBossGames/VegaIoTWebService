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
    public class TemperatureDeviceDataRepository : DeviceDataRepositoryBase<VegaTempDeviceData>, ITemperatureDeviceDataRepository
    {
        public TemperatureDeviceDataRepository(VegaApiDBContext context)
            :base(context.TempDeviceData, context)
        {
        }
    }
}