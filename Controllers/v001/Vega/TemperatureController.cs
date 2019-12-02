using System;
using System.Collections.Generic;
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
    public class TemperatureController : ControllerBase
    {
        private readonly ILogger<TemperatureController> _logger;
        private readonly IVegaApiCommunicator _communiactor;

        public TemperatureController(ILogger<TemperatureController> logger, IVegaApiCommunicator communicator)
        {
            _communiactor = communicator;
            _logger = logger;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<DeviceTDModel>>> GetAsync(string id)
        {
            var request = new DeviceDataReq()
            {
                DevEui = id,
                Select = new DeviceDataReq.SelectModel()
                {
                    Direction = "UPLINK",
                }
            };

            var result = await _communiactor.GetDeviceDataAsync(request, CancellationToken.None);

            var list = new List<DeviceTDModel>();

            foreach (var a in result.DataList)
            {
                if (a.Type == "UNCONF_UP" && a.Data.Length >= 26 && a.Data[0] == '0' && a.Data[1] == '1')
                {
                    var processed = DeviceTDModel.Parse(a.Data);
                    var temperature = processed.Temperature / 10.0;
                    list.Add(processed);
                }
            }

            return Ok(list);
        }
    }
}