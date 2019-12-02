using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Globalization;

namespace VegaIoTApi.AppServices.Models
{
    public class DeviceMC0101Model // магнитоконтактный датчик
    {
        public byte PackageType { get; set; }
        public byte BatteryLevel { get; set; }
        public byte AlwaysZero { get; set; }
        public short Tempterature { get; set; }
        public byte SendReason { get; set; }
        public byte InputState { get; set; }
        public uint UpTime { get; set; }

        public static DeviceMC0101Model Parse(string source)
        {
            const int byteSize = 11;
            const int charSapnSize = byteSize * 2;

            if (source.Length < charSapnSize) throw new FormatException("Length is too small!");

            DeviceMC0101Model device = new DeviceMC0101Model();

            // to do

            return device;
        }
    }
}