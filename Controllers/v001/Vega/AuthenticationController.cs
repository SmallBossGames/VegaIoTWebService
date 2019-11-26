using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text.Json.Serialization;
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
        private readonly IVegaApiCommunicator _communiactor;

        public AuthenticationController(ILogger<AuthenticationController> logger, IVegaApiCommunicator communicator)
        {
            _communiactor = communicator;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]AuthenticationRequestModel requestModel)
        {
            var result = await _communiactor.AuthenticateAsync(requestModel);
            return Ok(result);
        }
    }
}
