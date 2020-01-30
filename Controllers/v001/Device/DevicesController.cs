using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VegaIoTApi.Repositories.Interfaces;
using VegaIoTWebService.Data.Models;

namespace VegaIoTApi.Controllers.v001.Device
{
    [ApiController]
    [Route("api/v001/[controller]")]
    public class DevicesController : ControllerBase
    {
        private readonly IDeviceRepository _repository;

        public DevicesController(IDeviceRepository repository)
        {
            _repository = repository;
        }

        // GET: api/VegaTempDevices
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VegaDevice>>> GetDeviceAsync()
        {
            return await _repository.GetDevicesAsync().ConfigureAwait(false);
        }

        // GET: api/VegaTempDevices/5
        [HttpGet("{id}")]
        public async Task<ActionResult<VegaDevice>> GetDeviceAsync(long id)
        {
            var vegaTempDevice = await _repository.GetDeviceAsync(id).ConfigureAwait(false);

            if (vegaTempDevice == null) return NotFound();

            return vegaTempDevice;
        }

        // PUT: api/VegaTempDevices/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDeviceAsync(long id, VegaDevice vegaTempDevice)
        {
            if (vegaTempDevice is null)
            {
                throw new System.ArgumentNullException(nameof(vegaTempDevice));
            }

            if (id != vegaTempDevice.Id)
            {
                return BadRequest();
            }

            try
            {
                await _repository.EditDeviceAsync(vegaTempDevice).ConfigureAwait(false);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_repository.DeviceExists(id))
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
        public async Task<ActionResult<VegaDevice>> PostDeviceAsync(VegaDevice vegaTempDevice)
        {
            if (vegaTempDevice is null)
            {
                return BadRequest();
            }

            try
            {
                await _repository.AddDeviceAsync(vegaTempDevice).ConfigureAwait(false);
            }
            catch (DbUpdateException)
            {
                if (_repository.DeviceExists(vegaTempDevice.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetDevice", new { id = vegaTempDevice.Id }, vegaTempDevice);
        }

        // DELETE: api/VegaTempDevices/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<VegaDevice?>> DeleteDeviceAsync(long id)
        {
            var result = await _repository.DeleteDeviceAsync(id).ConfigureAwait(false);
            if (result == null)
            {
                return NotFound();
            }

            return result;
        }
    }
}