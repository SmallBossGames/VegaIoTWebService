using System;
using System.ComponentModel.DataAnnotations;

namespace VegaIoTWebService.Data.Models
{
    public class VegaTempDeviceData
    {
        public VegaTempDeviceData()
        {
            Device = null!;
        }

        public long Id { get; set; }

        public VegaTempDevice Device { get; set; }

        public long DeviceId { get; set; }

        public short BatteryLevel { get; set; }

        public bool PushTheLimit { get; set; }

        public DateTime Uptime { get; set; }

        public short Temperature { get; set; }

        public short LowLimit { get; set; }

        public short HighLimit { get; set; }
    }
}