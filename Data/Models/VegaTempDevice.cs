using System.ComponentModel.DataAnnotations;

namespace VegaIoTWebService.Data.Models
{
    public class VegaTempDevice
    {
        public long Id { get; set; }
        public string Eui { get; set; } = null!;
        public string Name { get; set; } = null!;
    }
}