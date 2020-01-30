using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using VegaIoTApi.AppServices;
using VegaIoTApi.AppServices.Models;
using VegaIoTApi.Controllers.Version1.Vega;
using VegaIoTWebService.Data.Models;

namespace VegaIoTApi.Controllers.v001.Vega
{
    [ApiController]
    [Route("api/v001/vega/[controller]")]
    public class MoveController : ControllerBase
    {
        private readonly ILogger<TemperatureController> _logger;
        private readonly IVegaApiCommunicator _communiactor;

        public MoveController(ILogger<TemperatureController> logger, IVegaApiCommunicator communicator)
        {
            _communiactor = communicator;
            _logger = logger;
        }

        [HttpGet("{id}")]
        public async Task<IEnumerable<VegaMoveDeviceData>> GetAsync(string id)
        {
            return await _communiactor
                .GetMoveDeviceDataAsync(id, 0, DateTimeOffset.FromUnixTimeMilliseconds(0))
                .ConfigureAwait(false);
        }

    }
}