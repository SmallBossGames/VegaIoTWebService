using System;
using System.Globalization;

namespace VegaIoTApi.AppServices.Models
{
    public class DeviceMS0101Model // датчик движения
    {
        public byte PackageType { get; set; }
        public byte BatteryLevel { get; set; }
        public byte MainSettings { get; set; }
        public short Temperature { get; set; }
        public byte SendReason { get; set; }
        public uint UpTime { get; set; }

        public static DeviceMS0101Model Parse(string source)
        {
            const int byteSize = 10;
            const int charSapnSize = byteSize * 2;

            if (source.Length < charSapnSize) throw new FormatException("Length is too small!");

            DeviceMS0101Model device = new DeviceMS0101Model();

            device.PackageType = byte.Parse(source[0..2], NumberStyles.HexNumber);
            device.BatteryLevel = byte.Parse(source[2..4], NumberStyles.HexNumber);
            device.MainSettings = byte.Parse(source[4..6], NumberStyles.HexNumber);

            Span<byte> convertedSourceTemp = stackalloc byte[2];
            for (int i = 0; i < convertedSourceTemp.Length; i++)
            {
                var tempSpan = source.AsSpan(i * 2 + 6, 2);
                convertedSourceTemp[i] = byte.Parse(tempSpan, NumberStyles.HexNumber);
            }

            device.Temperature = BitConverter.ToInt16(convertedSourceTemp[0..2]);
            device.SendReason = byte.Parse(source[10..12], NumberStyles.HexNumber);

            Span<byte> convertedSourceTime = stackalloc byte[4];
            for (int i = 0; i < convertedSourceTime.Length; i++)
            {
                var timeSpan = source.AsSpan(i * 2 + 12, 2);
                convertedSourceTime[i] = byte.Parse(timeSpan, NumberStyles.HexNumber);
            }

            device.UpTime = BitConverter.ToUInt32(convertedSourceTime[0..4]);

            return device;
        }
    }
}