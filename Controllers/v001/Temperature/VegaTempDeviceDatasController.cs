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
    [Route("api/[controller]")]
    [ApiController]
    public class VegaTempDeviceDatasController : ControllerBase
    {
        private readonly ITemperatureDeviceDataRepository _repository;

        public VegaTempDeviceDatasController(ITemperatureDeviceDataRepository repository)
        {
            _repository = repository;
        }

        // GET: api/VegaTempDeviceDatas
        [HttpGet]
        [Route("[action]")]
        public async Task<ActionResult<IEnumerable<VegaTempDeviceData>>> GetDeviceDatasAsync()
        {
            return await _repository.GetTempDeviceDatasAsync();
        }

        [Route("[action]")]
        [HttpGet("{deviceId}")]
        public async Task<ActionResult<IEnumerable<VegaTempDeviceData>>> GetDeviceDatasAsync(long deviceId)
        {
            var result = await _repository.GetTempDeviceDatasAsync(deviceId);
            if(result == null)
            {
                return NotFound();
            }

            return result;
        }

        // GET: api/VegaTempDeviceDatas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<VegaTempDeviceData>> GetTempDeviceData(long id)
        {
            var vegaTempDeviceData = await _repository.GetTempDeviceDataAsync(id);

            if (vegaTempDeviceData == null)
            {
                return NotFound();
            }

            return vegaTempDeviceData;
        }

        // PUT: api/VegaTempDeviceDatas/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVegaTempDeviceData(long id, VegaTempDeviceData tempDeviceData)
        {
            if (id != tempDeviceData.Id)
            {
                return BadRequest();
            }


            try
            {
                await _repository.EditTempDeviceDataAsync(tempDeviceData);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_repository.TempDeviceDataExists(id))
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

        // POST: api/VegaTempDeviceDatas
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<VegaTempDeviceData>> PostVegaTempDeviceData(VegaTempDeviceData vegaTempDeviceData)
        {
            await _repository.AddTempDeviceDataAsync(vegaTempDeviceData);

            return CreatedAtAction("GetVegaTempDeviceData", new { id = vegaTempDeviceData.Id }, vegaTempDeviceData);
        }

        // DELETE: api/VegaTempDeviceDatas/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<VegaTempDeviceData>> DeleteVegaTempDeviceData(long id)
        {
            var result = await _repository.DeleteVegaTempDeviceData(id);

            if (result == null)
            {
                return NotFound();
            }

            return result;
        }
    }
}
