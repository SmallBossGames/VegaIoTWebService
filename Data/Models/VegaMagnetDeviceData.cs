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
        public VegaTempDevice? Device { get; set; }
        public long DeviceId { get; set; }
        public byte PackageType { get; set; }
        public byte BatteryLevel { get; set; }
        public byte AlwaysZero { get; set; }
        public short Temperature { get; set; }
        public byte SendReason { get; set; }
        public byte InputState { get; set; }
        public DateTime UpTime { get; set; }

        public static VegaMagnetDeviceData Parse(string source)
        {
            const int byteSize = 11;
            const int charSpanSize = byteSize * 2;

            if (source.Length < charSpanSize) throw new FormatException("Length is too small!");

            VegaMagnetDeviceData device = new VegaMagnetDeviceData();

            device.PackageType = byte.Parse(source[0..2], NumberStyles.HexNumber);
            device.BatteryLevel = byte.Parse(source[2..4], NumberStyles.HexNumber);
            device.AlwaysZero = byte.Parse(source[4..6], NumberStyles.HexNumber);

            Span<byte> convertedSourceTemp = stackalloc byte[2];
            convertedSourceTemp = Utilits.GetSpan(convertedSourceTemp, source[6..10]);

            device.SendReason = byte.Parse(source[10..12], NumberStyles.HexNumber);
            device.InputState = byte.Parse(source[12..14], NumberStyles.HexNumber);

            Span<byte> convertedSourceTime = stackalloc byte[4];
            convertedSourceTime = Utilits.GetSpan(convertedSourceTime, source[14..22]);

            device.UpTime = Utilits.GetDateTime(convertedSourceTime);
            //device.UpTime = new DateTime(1970, 1, 1).AddSeconds(BitConverter.ToUInt32(convertedSourceTime[0..4]));

            return device;
        }
    }
}
