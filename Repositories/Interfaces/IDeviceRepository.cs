using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using VegaIoTWebService.Data.Models;

namespace VegaIoTApi.Repositories.Interfaces
{
    public interface IDeviceRepository
    {
        Task<VegaDevice> AddDeviceAsync(VegaDevice tempDevice, CancellationToken token = default);
        Task<VegaDevice?> DeleteDeviceAsync(long id, CancellationToken token = default);
        Task EditDeviceAsync(VegaDevice VegaDevice, CancellationToken token = default);
        Task<VegaDevice?> GetDeviceAsync(long id, CancellationToken token = default);
        Task<List<VegaDevice>> GetDevicesAsync(CancellationToken token = default);
        bool DeviceExists(long id);
    }
}