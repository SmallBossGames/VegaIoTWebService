using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VegaIoTApi.Data;
using VegaIoTApi.Repositories.Interfaces;
using VegaIoTWebService.Data.Models;

namespace VegaIoTApi.Controllers.v001.Temperature
{
    [Route("api/v001/temperature/[controller]")]
    [ApiController]
    public class VegaTempDeviceDatasController : ControllerBase
    {
        private readonly ITemperatureDeviceDataRepository _repository;

        public VegaTempDeviceDatasController(ITemperatureDeviceDataRepository repository)
        {
            _repository = repository;
        }

        // GET: api/VegaTempDeviceDatas
        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<VegaTempDeviceData>>> GetAllAsync()
        {
            return await _repository.GetAllAsync().ConfigureAwait(false);
        }

        [HttpGet("all/{deviceId}")]
        public async Task<ActionResult<IEnumerable<VegaTempDeviceData>>> GetAllAsync(long deviceId)
        {
            var result = await _repository.GetAllAsync(deviceId).ConfigureAwait(false);

            if (result == null)
                return NotFound();

            return result;
        }

        // GET: api/VegaTempDeviceDatas/5
        [HttpGet("current/{deviceId}")]
        public async Task<ActionResult<VegaTempDeviceData>> GetCurrentAsync(long deviceId)
        {
            var vegaTempDeviceData = await _repository.GetCurrentAsync(deviceId).ConfigureAwait(false);

            if (vegaTempDeviceData == null)
                return NotFound();

            return vegaTempDeviceData;
        }

        [HttpGet("current")]
        public async Task<ActionResult<IEnumerable<VegaTempDeviceData>>> GetCurrentAsync()
        {
            return await _repository.GetCurrentAsync().ConfigureAwait(false);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<VegaTempDeviceData>> GetAsync(long id)
        {
            var vegaTempDeviceData = await _repository.GetDataAsync(id).ConfigureAwait(false);

            if (vegaTempDeviceData == null)
                return NotFound();

            return vegaTempDeviceData;
        }

        // PUT: api/VegaTempDeviceDatas/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVegaTempDeviceData(long id, VegaTempDeviceData tempDeviceData)
        {
            if (tempDeviceData is null)
            {
                throw new ArgumentNullException(nameof(tempDeviceData));
            }

            if (id != tempDeviceData.Id || !_repository.TempDeviceExists(tempDeviceData.DeviceId))
                return BadRequest();

            try
            {
                await _repository.EditTempDeviceDataAsync(tempDeviceData).ConfigureAwait(false);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_repository.TempDeviceDataExists(id))
                    return NotFound();
                else throw;
            }

            return NoContent();
        }

        // POST: api/VegaTempDeviceDatas
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<VegaTempDeviceData>> PostVegaTempDeviceData(VegaTempDeviceData tempDeviceData)
        {
            if (tempDeviceData is null)
            {
                throw new ArgumentNullException(nameof(tempDeviceData));
            }

            if (!_repository.TempDeviceExists(tempDeviceData.DeviceId))
                return BadRequest();

            await _repository.AddTempDeviceDataAsync(tempDeviceData).ConfigureAwait(false);

            return CreatedAtAction("GetTempDeviceData", new { id = tempDeviceData.Id }, tempDeviceData);
        }

        // DELETE: api/VegaTempDeviceDatas/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<VegaTempDeviceData>> DeleteVegaTempDeviceData(long id)
        {
            var result = await _repository.DeleteVegaTempDeviceData(id).ConfigureAwait(false);

            if (result == null)
                return NotFound();

            return result;
        }
    }
}