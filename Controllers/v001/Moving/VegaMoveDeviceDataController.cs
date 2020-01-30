using Microsoft.AspNetCore.Mvc;
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

        // GET: api/VegaTempDeviceDatas
        [HttpGet("all")]
        public Task<IEnumerable<VegaMoveDeviceData>> GetAllAsync()
        {
            return _repository.GetAllAsync();
        }

        [HttpGet("all/{deviceId}")]
        public async Task<ActionResult<IEnumerable<VegaMoveDeviceData>>> GetAllAsync
            (long deviceId, [FromQuery]int startIndex = 0, [FromQuery]int limit = int.MaxValue)
        {
            var result = await _repository.GetAllAsync(deviceId, startIndex, limit).ConfigureAwait(false);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        // GET: api/VegaTempDeviceDatas/5
        [HttpGet("current/{deviceId}")]
        public async Task<ActionResult<VegaMoveDeviceData>> GetCurrentAsync(long deviceId)
        {
            var vegaTempDeviceData = await _repository.GetCurrentAsync(deviceId).ConfigureAwait(false);

            if (vegaTempDeviceData == null)
                return NotFound();

            return vegaTempDeviceData;
        }

        [HttpGet("current")]
        public Task<IEnumerable<VegaMoveDeviceData>> GetCurrentAsync()
        {
            return _repository.GetCurrentAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<VegaMoveDeviceData>> GetAsync(long id)
        {
            var vegaTempDeviceData = await _repository
                .GetDataAsync(id)
                .ConfigureAwait(false);

            if (vegaTempDeviceData == null)
                return NotFound();

            return vegaTempDeviceData;
        }

        // PUT: api/VegaTempDeviceDatas/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVegaTempDeviceData(long id, VegaMoveDeviceData tempDeviceData)
        {
            if (tempDeviceData is null)
            {
                return BadRequest();
            }

            if (id != tempDeviceData.Id || !_repository.DeviceExists(tempDeviceData.DeviceId))
                return BadRequest();

            try
            {
                await _repository.EditVegaDeviceDataAsync(tempDeviceData).ConfigureAwait(false);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_repository.MoveDeviceDataExists(id))
                    return NotFound();
                else throw;
            }

            return NoContent();
        }

        // POST: api/VegaTempDeviceDatas
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<VegaMoveDeviceData>> PostVegaTempDeviceData(VegaMoveDeviceData tempDeviceData)
        {
            if (tempDeviceData is null)
            {
                throw new ArgumentNullException(nameof(tempDeviceData));
            }

            if (!_repository.DeviceExists(tempDeviceData.DeviceId))
                return BadRequest();

            await _repository
                .AddVegaMovingDeviceDataAsync(tempDeviceData)
                .ConfigureAwait(false);

            return CreatedAtAction("GetTempDeviceData", new { id = tempDeviceData.Id }, tempDeviceData);
        }

        // DELETE: api/VegaTempDeviceDatas/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<VegaMoveDeviceData>> DeleteVegaTempDeviceData(long id)
        {
            var result = await _repository
                .DeleteVegaMovingDeviceData(id)
                .ConfigureAwait(false);

            if (result == null)
                return NotFound();

            return result;
        }
    }
}