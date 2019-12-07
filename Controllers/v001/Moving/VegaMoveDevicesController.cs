using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VegaIoTWebService.Data.Models;
using VegaIoTApi.Repositories;
using Microsoft.EntityFrameworkCore;

namespace VegaIoTApi.Controllers.v001.Moving
{
    [Route("api/v001/moving/[controller]")]
    [ApiController]
    public class VegaMoveDevicesController : ControllerBase
    {
        readonly IMovingDeviceRepository _repository;

        public VegaMoveDevicesController(IMovingDeviceRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<VegaTempDevice>>> GetMoveDevice() => await _repository.GetMoveDevicesAsync();

        [HttpGet("{id}")]
        public async Task<ActionResult<VegaTempDevice>> GetVegaMoveDevice(long id)
        {
            var result = await _repository.GetMoveDeviceAsync(id);

            if (result == null) return NotFound();

            return result;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutVegaMoveDevice(long id, VegaTempDevice vegaTempDevice)
        {
            if (id != vegaTempDevice.Id) return BadRequest();

            try
            {
                await _repository.EditMoveDeviceAsync(vegaTempDevice);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_repository.MoveDeviceExists(id)) return NotFound();
                else throw;
            }
            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<VegaTempDevice>> PostVegaMoveDevice(VegaTempDevice vegaTempDevice)
        {
            try
            {
                await _repository.AddMoveDeviceAsync(vegaTempDevice);
            }
            catch (DbUpdateException)
            {
                if (!_repository.MoveDeviceExists(vegaTempDevice.Id)) return Conflict();
                else throw;
            }

            return CreatedAtAction("GetVegaTempDevice", new { id = vegaTempDevice.Id }, vegaTempDevice); // "GetVegaTempDevice" - пакеты от устройства одного типа, тут должно быть именно это?
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<VegaTempDevice?>> DeleteVegaMoveDevice(long id)
        {
            var result = await _repository.DeleteVegaMoveDevice(id);

            if (result == null) return NotFound();

            return result;
        }
    }
}