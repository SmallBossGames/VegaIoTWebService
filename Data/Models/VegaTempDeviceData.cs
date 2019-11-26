using System;
using System.ComponentModel.DataAnnotations;

namespace VegaIoTWebService.Data.Models
{
    public class VegaTempDeviceData
    {
        public VegaTempDeviceData()
        {
            Id = null!;
            Device = null!;
            DeviceId = null!;
        }

        public VegaTempDeviceData(string id, VegaTempDevice device, string deviceId, short batteryLevel, bool pushTheLimit, DateTime uptime, short temperature, short lowLimit, short highLimit)
        {
            Id = id;
            Device = device;
            DeviceId = deviceId;
            BatteryLevel = batteryLevel;
            PushTheLimit = pushTheLimit;
            Uptime = uptime;
            Temperature = temperature;
            LowLimit = lowLimit;
            HighLimit = highLimit;
        }

        public string Id { get; set; }

        public VegaTempDevice Device { get; set; }

        public string DeviceId { get; set; }

        public short BatteryLevel { get; set; }

        public bool PushTheLimit { get; set; }

        public DateTime Uptime { get; set; }

        public short Temperature { get; set; }

        public short LowLimit { get; set; }

        public short HighLimit { get; set; }
    }
}