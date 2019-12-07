using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VegaIoTWebService.Data.Models;
using System.Threading;

namespace VegaIoTApi.Repositories
{
    public interface IMagnetDeviceRepository
    {
        Task<VegaTempDevice> AddMagnetDeviceAsync(VegaTempDevice vegaTempDevice, CancellationToken cancellationToken = default);
        Task<VegaTempDevice?> DeleteVegaMagnetDevice(long id, CancellationToken cancellationToken = default);
        Task EditMagnetDeviceAsync(VegaTempDevice vegaTempDevice, CancellationToken cancellationToken = default);
        Task<VegaTempDevice?> GetMagnetDeviceAsync(long id, CancellationToken cancellationToken = default);
        Task<List<VegaTempDevice>> GetMagnetDevicesAsync(CancellationToken cancellationToken = default);
        bool MagnetDevicesExists(long id);
    }
}