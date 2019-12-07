﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VegaIoTApi.Repositories.Interfaces;
using VegaIoTWebService.Data.Models;

namespace VegaIoTApi.Controllers.v001.Moving
{
    [Route("api/v001/moving/[controller]")]
    [ApiController]
    public class VegaMoveDeviceDataController : ControllerBase
    {
        readonly IMovingDeviceDataRepository _repository;

        public VegaMoveDeviceDataController(IMovingDeviceDataRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<VegaMoveDeviceData>>> GetAllAsync() => 
            await _repository.GetAllAsync().ConfigureAwait(false);

        [HttpGet("all/{deviceId}")]
        public async Task<ActionResult<IEnumerable<VegaMoveDeviceData>>> GetAllAsync(long deviceId)
        {
            var result = await _repository.GetAllAsync(deviceId).ConfigureAwait(false);

            if (result == null) return NotFound();

            return result;
        }

        [HttpGet("current/{deviceId}")]
        public async Task<ActionResult<VegaMoveDeviceData>> GetCurrentAsync(long deviceId)
        {
            var result = await _repository.GetCurrentAsync(deviceId).ConfigureAwait(false);

            if (result == null) return NotFound();

            return result;
        }

        [HttpGet("current")]
        public async Task<ActionResult<IEnumerable<VegaMoveDeviceData>>> GetCurrentAsync() 
            => await _repository.GetCurrentAsync().ConfigureAwait(false);

        [HttpGet("{id}")]
        public async Task<ActionResult<VegaMoveDeviceData>> GetAsync(long id)
        {
            var result = await _repository.GetDataAsync(id).ConfigureAwait(false);

            if (result == null) return NotFound();

            return result;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutVegaMoveDeviceData(long id, VegaMoveDeviceData vegaMoveDeviceData)
        {
            if (vegaMoveDeviceData is null)
            {
                throw new ArgumentNullException(nameof(vegaMoveDeviceData));
            }

            if (id != vegaMoveDeviceData.Id || !_repository.MoveDeviceExists(vegaMoveDeviceData.Id)) 
                return BadRequest();
            
            try
            {
                await _repository.EditVegaDeviceDataAsync(vegaMoveDeviceData).ConfigureAwait(false);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_repository.MoveDeviceDataExists(id)) return NotFound();
                else throw;
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<VegaMoveDeviceData>> PostVegaMoveDeviceData(VegaMoveDeviceData vegaMoveDeviceData)
        {
            if (vegaMoveDeviceData is null)
            {
                throw new ArgumentNullException(nameof(vegaMoveDeviceData));
            }

            if (!_repository.MoveDeviceExists(vegaMoveDeviceData.Id)) return BadRequest();

            await _repository.AddVegaMovingDeviceDataAsync(vegaMoveDeviceData).ConfigureAwait(false);

            return CreatedAtAction("GetMoveDeviceData", new { id = vegaMoveDeviceData.Id }, vegaMoveDeviceData);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<VegaMoveDeviceData>> DeleteVegaMoveDeviceData(long id)
        {
            var result = await _repository.DeleteVegaMovingDeviceData(id).ConfigureAwait(false);

            if (result == null) return NotFound();

            return result;
        }
    }
}