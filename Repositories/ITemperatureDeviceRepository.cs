using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using VegaIoTWebService.Data.Models;

namespace VegaIoTApi.Repositories
{
    public interface ITemperatureDeviceRepository
    {
        Task<VegaTempDevice> AddTempDeviceAsync(VegaTempDevice tempDevice, CancellationToken token = default);
        Task<VegaTempDevice?> DeleteVegaTempDevice(long id, CancellationToken token = default);
        Task EditTempDeviceAsync(VegaTempDevice vegaTempDevice, CancellationToken token = default);
        Task<VegaTempDevice?> GetTempDeviceAsync(long id, CancellationToken token = default);
        Task<List<VegaTempDevice>> GetTempDevicesAsync(CancellationToken token = default);
        bool TempDeviceExists(long id);
    }
}