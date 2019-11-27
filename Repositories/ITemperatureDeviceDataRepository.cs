using System.Collections.Generic;
using System.Threading.Tasks;
using VegaIoTWebService.Data.Models;

namespace VegaIoTApi.Repositories
{
    public interface ITemperatureDeviceDataRepository
    {
        Task<VegaTempDeviceData> AddTempDeviceDataAsync(VegaTempDeviceData tempDeviceData);
        Task<VegaTempDeviceData?> DeleteVegaTempDeviceData(long id);
        Task EditTempDeviceDataAsync(VegaTempDeviceData tempDeviceData);
        Task<List<VegaTempDeviceData>> GetTempDeviceDatasAsync();
        Task<List<VegaTempDeviceData>?> GetTempDeviceDatasAsync(long deviceId);
        Task<VegaTempDeviceData?> GetTempDeviceDataAsync(long id);
        bool TempDeviceDataExists(long id);
        bool TempDeviceExists(long deviceId);
    }
}