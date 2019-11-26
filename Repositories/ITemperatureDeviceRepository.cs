using System.Collections.Generic;
using System.Threading.Tasks;
using VegaIoTWebService.Data.Models;

namespace VegaIoTApi.Repositories
{
    public interface ITemperatureDeviceRepository
    {
        Task<VegaTempDevice> AddTempDeviceAsync(VegaTempDevice tempDevice);
        Task<VegaTempDevice?> DeleteVegaTempDevice(string id);
        Task EditTempDeviceAsync(VegaTempDevice vegaTempDevice);
        Task<VegaTempDevice?> GetTempDeviceAsync(string id);
        Task<List<VegaTempDevice>> GetTempDevicesAsync();
        bool TempDeviceExists(string id);
    }
}