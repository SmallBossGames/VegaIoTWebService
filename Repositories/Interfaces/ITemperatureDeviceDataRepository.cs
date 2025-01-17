﻿using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using VegaIoTWebService.Data.Models;

namespace VegaIoTApi.Repositories.Interfaces
{
    public interface ITemperatureDeviceDataRepository: IDeviceDataRepository<VegaTempDeviceData>
    {

    }
}