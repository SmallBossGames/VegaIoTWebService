using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VegaIoTApi.Repositories.Interfaces;
using VegaIoTWebService.Data.Models;

namespace VegaIoTApi.Controllers.v001.Magnet
{
    [Route("api/v001/magnet/[controller]")]
    [ApiController]
    public class VegaMagnetDeviceDataController : ControllerBase
    {
        readonly IMagnetDeviceDataRepository _repository;

        public VegaMagnetDeviceDataController(IMagnetDeviceDataRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<VegaMagnetDeviceData>>> GetAllAsync()
            => await _repository.GetAllAsync().ConfigureAwait(false);

        [HttpGet("all/{deviceId}")]
        public async Task<ActionResult<IEnumerable<VegaMagnetDeviceData>>> GetAllAsync(long deviceId)
        {
            var result = await _repository.GetAllAsync(deviceId).ConfigureAwait(false);

            if (result == null) return NotFound();

            return result;
        }

        [HttpGet("current/{deviceId}")]
        public async Task<ActionResult<VegaMagnetDeviceData>> GetCurrentAsync(long deviceId)
        {
            var result = await _repository.GetCurrentAsync(deviceId).ConfigureAwait(false);

            if (result == null) return NotFound();

            return result;
        }

        [HttpGet("current")]
        public async Task<ActionResult<IEnumerable<VegaMagnetDeviceData>>> GetCurrentAsync() 
            => await _repository.GetCurrentAsync().ConfigureAwait(false);

        [HttpGet("{id}")]
        public async Task<ActionResult<VegaMagnetDeviceData>> GetAsync(long id)
        {
            var result = await _repository.GetDataAsync(id).ConfigureAwait(false);

            if (result == null) return NotFound();

            return result;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutVegaMagnetDeviceData(long id, VegaMagnetDeviceData vegaMagnetDeviceData)
        {
            if (vegaMagnetDeviceData is null)
            {
                throw new ArgumentNullException(nameof(vegaMagnetDeviceData));
            }

            if (id != vegaMagnetDeviceData.Id || !_repository.MagnetDeviceExists(vegaMagnetDeviceData.Id)) 
                return BadRequest();
            

            try
            {
                await _repository.EditVegaDeviceDataAsync(vegaMagnetDeviceData).ConfigureAwait(false);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_repository.MagnetDeviceDataExists(id)) return NotFound();
                else throw;
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<VegaMagnetDeviceData>> PostVegaMagnetDeviceData(VegaMagnetDeviceData vegaMagnetDeviceData)
        {
            if (vegaMagnetDeviceData is null)
            {
                throw new ArgumentNullException(nameof(vegaMagnetDeviceData));
            }

            if (!_repository.MagnetDeviceExists(vegaMagnetDeviceData.Id)) return BadRequest();

            await _repository.AddVegaMagnetDeviceDataAsync(vegaMagnetDeviceData).ConfigureAwait(false);

            return CreatedAtAction("GetMagnetDeviceData", new { id = vegaMagnetDeviceData.Id }, vegaMagnetDeviceData);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<VegaMagnetDeviceData>> DeleteVegaMagnetDeviceData(long id)
        {
            var result = await _repository.DeleteVegaMagnetDeviceData(id).ConfigureAwait(false);

            if (result == null) return NotFound();

            return result;
        }
    }
}