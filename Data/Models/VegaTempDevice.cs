using System.ComponentModel.DataAnnotations;

namespace VegaIoTWebService.Data.Models
{
    public class VegaTempDevice
    {
        public VegaTempDevice()
        {
            Eui = null!;
            Name = null!;
        }

        public VegaTempDevice(string id, string eui, string name)
        {
            Eui = eui;
            Name = name;
        }

        public long Id { get; set; }
        public string Eui { get; set; }
        public string Name { get; set; }
    }
}