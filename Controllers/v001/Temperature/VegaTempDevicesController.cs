using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VegaIoTApi.Data;
using VegaIoTApi.Repositories;
using VegaIoTWebService.Data.Models;

namespace VegaIoTApi.Controllers.v001.Temperature
{
    [Route("api/v001/temperature/[controller]")]
    [ApiController]
    public class VegaTempDevicesController : ControllerBase
    {
        private readonly ITemperatureDeviceRepository _repository;

        public VegaTempDevicesController(ITemperatureDeviceRepository repository)
        {
            _repository = repository;
        }

        // GET: api/VegaTempDevices
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VegaTempDevice>>> GetTempDevices()
        {
            return await _repository.GetTempDevicesAsync();
        }

        // GET: api/VegaTempDevices/5
        [HttpGet("{id}")]
        public async Task<ActionResult<VegaTempDevice>> GetVegaTempDevice(string id)
        {
            var vegaTempDevice = await _repository.GetTempDeviceAsync(id);

            if (vegaTempDevice == null)
            {
                return NotFound();
            }

            return vegaTempDevice;
        }

        // PUT: api/VegaTempDevices/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVegaTempDevice(string id, VegaTempDevice vegaTempDevice)
        {
            if (id != vegaTempDevice.Id)
            {
                return BadRequest();
            }


            try
            {
                await _repository.EditTempDeviceAsync(vegaTempDevice);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_repository.TempDeviceExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/VegaTempDevices
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<VegaTempDevice>> PostVegaTempDevice(VegaTempDevice vegaTempDevice)
        {
            try
            {
                await _repository.AddTempDeviceAsync(vegaTempDevice);
            }
            catch (DbUpdateException)
            {
                if (_repository.TempDeviceExists(vegaTempDevice.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetVegaTempDevice", new { id = vegaTempDevice.Id }, vegaTempDevice);
        }

        // DELETE: api/VegaTempDevices/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<VegaTempDevice?>> DeleteVegaTempDevice(string id)
        {
            var result = await _repository.DeleteVegaTempDevice(id);
            if(result == null)
            {
                return NotFound();
            }

            return result;
        }
    }
}
