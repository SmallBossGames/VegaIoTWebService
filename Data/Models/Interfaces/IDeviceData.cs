using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VegaIoTApi.Data.Models.Interfaces
{
    public interface IDeviceData
    {
        public long Id { get; set; }
        public long DeviceId { get; set; }
        public short BatteryLevel { get; set; }
        public DateTimeOffset Uptime { get; set; }
    }
}
