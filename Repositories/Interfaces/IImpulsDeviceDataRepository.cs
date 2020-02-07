using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using VegaIoTWebService.Data.Models;

namespace VegaIoTApi.Repositories.Interfaces
{
    interface IImpulsDeviceDataRepository:IDeviceDataRepository<VegaImpulsDeviceData>
    {
    }
}
