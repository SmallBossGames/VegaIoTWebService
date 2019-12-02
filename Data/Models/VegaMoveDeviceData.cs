using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Globalization;

namespace VegaIoTWebService.Data.Models
{
    public class VegaMoveDeviceData // датчик движения
    {
        public long Id { get; set; }
        public VegaTempDevice? Device { get; set; }
        public long DeviceId { get; set; }
        public byte PackageType { get; set; }
        public short BatteryLevel { get; set; }
        public byte MainSettings { get; set; }
        public double Temperature { get; set; }
        public byte SendReason { get; set; }
        public DateTime UpTime { get; set; }

        public static VegaMoveDeviceData Parse(string source)
        {
            const int byteSize = 10;
            const int charSpanSize = byteSize * 2;

            if (source.Length < charSpanSize) throw new FormatException("Length is too small!");

            VegaMoveDeviceData device = new VegaMoveDeviceData();

            device.PackageType = byte.Parse(source[0..2], NumberStyles.HexNumber);
            device.BatteryLevel = byte.Parse(source[2..4], NumberStyles.HexNumber);
            device.MainSettings = byte.Parse(source[4..6], NumberStyles.HexNumber);

            Span<byte> convertedSourceTemp = stackalloc byte[2];
            convertedSourceTemp = Utilits.GetSpan(convertedSourceTemp, source[6..10]);
            device.Temperature = BitConverter.ToInt16(convertedSourceTemp[0..2]) / 10.0;

            device.SendReason = byte.Parse(source[10..12], NumberStyles.HexNumber);

            Span<byte> convertedSourceTime = stackalloc byte[4];
            convertedSourceTime = Utilits.GetSpan(convertedSourceTime, source[12..20]);
            device.UpTime = Utilits.GetDateTime(convertedSourceTime);
            //device.UpTime = new DateTime(1970, 1, 1).AddSeconds(BitConverter.ToUInt32(convertedSourceTime[0..4]));

            return device;
        }
    }
}