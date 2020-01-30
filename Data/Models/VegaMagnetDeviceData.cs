using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Globalization;

namespace VegaIoTWebService.Data.Models
{
    public class VegaMagnetDeviceData // магнитоконтактный датчик
    {
        public long Id { get; set; }
        public VegaDevice? Device { get; set; }
        public long DeviceId { get; set; }
        public byte PackageType { get; set; }
        public short BatteryLevel { get; set; }
        public byte AlwaysZero { get; set; }
        public double Temperature { get; set; }
        public byte SendReason { get; set; }
        public short InputState { get; set; }
        public DateTimeOffset UpTime { get; set; }

        public static VegaMagnetDeviceData Parse(string source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            const int byteSize = 11;
            const int charSpanSize = byteSize * 2;

            if (source.Length < charSpanSize) throw new FormatException("Length is too small!");

            VegaMagnetDeviceData device = new VegaMagnetDeviceData();

            device.PackageType = byte.Parse(source[0..2], NumberStyles.HexNumber);
            device.BatteryLevel = byte.Parse(source[2..4], NumberStyles.HexNumber);
            device.AlwaysZero = byte.Parse(source[4..6], NumberStyles.HexNumber);


            var convertedSourceTemp = Utilits.ConvertHexString(stackalloc byte[2], source[6..10]);
            device.Temperature = BitConverter.ToInt16(convertedSourceTemp) / 10.0;

            device.SendReason = byte.Parse(source[10..12], NumberStyles.HexNumber);
            device.InputState = byte.Parse(source[12..14], NumberStyles.HexNumber);

            var convertedSourceTime = Utilits.ConvertHexString(stackalloc byte[4], source[14..22]);
            device.UpTime = DateTimeOffset.FromUnixTimeSeconds(BitConverter.ToUInt32(convertedSourceTime[0..4]));

            return device;
        }
    }
}