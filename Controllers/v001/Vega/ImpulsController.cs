using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using VegaIoTApi.AppServices;
using VegaIoTApi.AppServices.Models;
using VegaIoTWebService.Data.Models;

namespace VegaIoTApi.Controllers.v001.Vega
{
    [ApiController]
    [Route("api/v001/vega/[controller]")]
    public class ImpulsController : ControllerBase
    {
        private readonly ILogger<ImpulsController> _logger;
        private readonly IVegaApiCommunicator _communiactor;

        public ImpulsController(ILogger<ImpulsController> logger, IVegaApiCommunicator communicator)
        {
            _logger = logger;
            _communiactor = communicator;
        }

        [HttpGet("{id}")]
        public async Task<IEnumerable<VegaImpulsDeviceData>> GetAsync(string id)
        {
            return await _communiactor.
                GetImpulsDeviceDataAsync(id, 0, DateTimeOffset.FromUnixTimeMilliseconds(0)).
                ConfigureAwait(false);
        }
    }
}
