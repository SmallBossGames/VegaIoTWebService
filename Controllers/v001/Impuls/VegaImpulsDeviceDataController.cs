﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VegaIoTApi.Repositories.Interfaces;
using VegaIoTWebService.Data.Models;

namespace VegaIoTApi.Controllers.v001.Impuls
{
    [Route("api/v001/impuls/[controller]")]
    [ApiController]
    public class VegaImpulsDeviceDataController : ControllerBase
    {
        readonly IImpulsDeviceDataRepository _repository;

        public VegaImpulsDeviceDataController(IImpulsDeviceDataRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("all")]
        public async Task<IEnumerable<VegaImpulsDeviceData>> GetAllAsync()
            => await _repository.GetAllAsync().ConfigureAwait(false);

        [HttpGet("all/{deviceId}")]
        public async Task<ActionResult<IEnumerable<VegaImpulsDeviceData>>> GetAllAsync
            (long deviceId, [FromQuery]int startIndex = 0, [FromQuery]int limit = int.MaxValue)
        {
            var result = await _repository.GetAllAsync(deviceId, startIndex, limit).ConfigureAwait(false);

            if (result == null) return NotFound();

            return Ok(result);
        }

        [HttpGet("all/delta/{deviceId}")]
        public async Task<ActionResult<IEnumerable<VegaImpulsDeviceData>>> GetAllDeltaAsync(long deviceId, [FromQuery]int days)
        {
            var result = await _repository
                .GetAllAsync(deviceId, DateTimeOffset.Now, DateTimeOffset.Now.AddDays(-days))
                .ConfigureAwait(false);

            if (result == null) return NotFound();

            VegaImpulsDeviceData[] modifiedData = new VegaImpulsDeviceData[result.Count() - 1];

            for (int i = 1; i < result.Count(); i++)
            {
                modifiedData[i - 1] = new VegaImpulsDeviceData();
                modifiedData[i - 1].Id = result.ElementAt(i).Id;
                modifiedData[i - 1].Device = result.ElementAt(i).Device;
                modifiedData[i - 1].DeviceId = result.ElementAt(i).DeviceId;
                modifiedData[i - 1].PackageType = result.ElementAt(i).PackageType;
                modifiedData[i - 1].BatteryLevel = result.ElementAt(i).BatteryLevel;
                modifiedData[i - 1].MainSettings = result.ElementAt(i).MainSettings;
                modifiedData[i - 1].AlarmExit = result.ElementAt(i).AlarmExit;
                modifiedData[i - 1].Uptime = result.ElementAt(i).Uptime;
                modifiedData[i - 1].Temperature = result.ElementAt(i).Temperature;
                modifiedData[i - 1].InputState1 = result.ElementAt(i).InputState1;
                modifiedData[i - 1].InputState2 = result.ElementAt(i).InputState2;
                modifiedData[i - 1].InputState3 = result.ElementAt(i - 1).InputState3 - result.ElementAt(i).InputState3;
                modifiedData[i - 1].InputState4 = result.ElementAt(i).InputState4;
            }

            return Ok(modifiedData);
        }

        [HttpGet("current/{deviceId}")]
        public async Task<ActionResult<VegaImpulsDeviceData>> GetCurrentAsync(long deviceId)
        {
            var vegaImpulsData = await _repository.GetCurrentAsync(deviceId).ConfigureAwait(false);

            if (vegaImpulsData == null) return NotFound();

            return vegaImpulsData;
        }

        [HttpGet("current")]
        public async Task<IEnumerable<VegaImpulsDeviceData>> GetCurrentAsync()
            => await _repository.GetCurrentAsync().ConfigureAwait(false);

        [HttpGet("{id}")]
        public async Task<ActionResult<VegaImpulsDeviceData>> GetAsync(long id)
        {
            var vegaImpulsDeviceData = await _repository.GetAsync(id).ConfigureAwait(false);

            if (vegaImpulsDeviceData == null)
                return NotFound();

            return vegaImpulsDeviceData;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutVegaImpulsDeviceData(long id, VegaImpulsDeviceData vegaImpulsDeviceData)
        {
            if (vegaImpulsDeviceData == null)
                throw new ArgumentNullException(nameof(vegaImpulsDeviceData));


            if (id != vegaImpulsDeviceData.Id || _repository.ImpulsDeviceDataExists(vegaImpulsDeviceData.DeviceId))
                return BadRequest();

            try
            {
                await _repository.EditAsync(vegaImpulsDeviceData).ConfigureAwait(false);
            }
            catch
            {
                if (!_repository.ImpulsDeviceDataExists(vegaImpulsDeviceData.DeviceId)) return NotFound();
                else throw;
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<VegaImpulsDeviceData>> PostVegaImpulsDeviceData(VegaImpulsDeviceData vegaImpulsDeviceData)
        {
            if (vegaImpulsDeviceData == null)
                throw new ArgumentNullException(nameof(vegaImpulsDeviceData));

            if (!_repository.DeviceExists(vegaImpulsDeviceData.DeviceId)) return BadRequest();

            await _repository.AddAsync(vegaImpulsDeviceData).ConfigureAwait(false);

            return CreatedAtAction("GetImpulsDeviceData", new { id = vegaImpulsDeviceData.Id }, vegaImpulsDeviceData);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<VegaImpulsDeviceData>> DeleteVegaImpulsDeviceData(long id)
        {
            var result = await _repository.DeleteAsync(id).ConfigureAwait(false);

            if (result == null) return NotFound();

            return result;
        }
    }
}