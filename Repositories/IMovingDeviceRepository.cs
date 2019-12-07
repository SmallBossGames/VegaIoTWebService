using System.Collections.Generic;
using System.Threading.Tasks;
using VegaIoTWebService.Data.Models;
using System.Threading;

namespace VegaIoTApi.Repositories
{
    public interface IMovingDeviceRepository
    {
        Task<VegaTempDevice> AddMoveDeviceAsync(VegaTempDevice vegaTempDevice, CancellationToken cancellationToken = default);
        Task<VegaTempDevice?> DeleteVegaMoveDevice(long id, CancellationToken cancellationToken = default);
        Task EditMoveDeviceAsync(VegaTempDevice vegaTempDevice, CancellationToken cancellationToken = default);
        Task<VegaTempDevice?> GetMoveDeviceAsync(long id, CancellationToken cancellationToken = default);
        Task<List<VegaTempDevice>> GetMoveDevicesAsync(CancellationToken cancellationToken = default);
        bool MoveDeviceExists(long id);
    }
}