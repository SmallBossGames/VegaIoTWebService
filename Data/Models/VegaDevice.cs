using System.ComponentModel.DataAnnotations;
using VegaIoTApi.Data.Models;

namespace VegaIoTWebService.Data.Models
{
    public class VegaDevice
    {
        public long Id { get; set; }
        public string Eui { get; set; } = null!;
        public string Name { get; set; } = null!;
        public DeviceType DeviceType { get; set; }
    }
}