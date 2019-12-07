using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using VegaIoTWebService.Data.Models;

namespace VegaIoTApi.Repositories.Interfaces
{
    public interface IDeviceRepository
    {
        Task<VegaTempDevice> AddDeviceAsync(VegaTempDevice tempDevice, CancellationToken token = default);
        Task<VegaTempDevice?> DeleteDeviceAsync(long id, CancellationToken token = default);
        Task EditDeviceAsync(VegaTempDevice vegaTempDevice, CancellationToken token = default);
        Task<VegaTempDevice?> GetDeviceAsync(long id, CancellationToken token = default);
        Task<List<VegaTempDevice>> GetDevicesAsync(CancellationToken token = default);
        bool DeviceExists(long id);
    }
}