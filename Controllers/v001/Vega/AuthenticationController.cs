using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using VegaIoTApi.AppServices;
using VegaIoTApi.AppServices.Models;

namespace VegaIoTApi.Controllers.Version1.Vega
{
    [ApiController]
    [Route("api/v001/vega/[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly ILogger<AuthenticationController> _logger;
        private readonly IVegaApiCommunicator _communicator;

        public AuthenticationController(ILogger<AuthenticationController> logger, IVegaApiCommunicator communicator)
        {
            _communicator = communicator;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]AuthenticationReq requestModel)
        {
            var result = await _communicator
                .AuthenticateAsync(requestModel, CancellationToken.None).ConfigureAwait(false);

            return Ok(result);
        }
    }
}