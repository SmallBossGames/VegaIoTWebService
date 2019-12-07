using System;
using System.Globalization;

namespace VegaIoTApi.AppServices.Models
{
    public class DeviceTDModel // датчик температуры
    {
        public byte PackageType { get; set; }
        public byte BatteryLevel { get; set; }
        public byte PushTheLimit { get; set; }
        public uint Uptime { get; set; }
        public short Temperature { get; set; }
        public sbyte LowLimit { get; set; }
        public sbyte HighLimit { get; set; }
        public byte Reason { get; set; }
        public byte InputState { get; set; }

        public static DeviceTDModel Parse(string source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            const int byteSize = 13;
            const int charSapnSize = byteSize * 2;

            if (source.Length < charSapnSize)
                throw new FormatException("Length is too small");

            var temp = new DeviceTDModel();

            temp.PackageType = byte.Parse(source[0..2], NumberStyles.HexNumber);
            temp.BatteryLevel = byte.Parse(source[2..4], NumberStyles.HexNumber);
            temp.PushTheLimit = byte.Parse(source[4..6], NumberStyles.HexNumber);

            Span<byte> convertedSource = stackalloc byte[6];
            for (var i = 0; i < convertedSource.Length; i++)
            {
                var tempSpan = source.AsSpan(i * 2 + 6, 2);
                convertedSource[i] = byte.Parse(tempSpan, NumberStyles.HexNumber);
            }

            temp.Uptime = BitConverter.ToUInt32(convertedSource[0..4]);
            temp.Temperature = BitConverter.ToInt16(convertedSource[4..6]);

            temp.LowLimit = sbyte.Parse(source[18..20], NumberStyles.HexNumber);
            temp.HighLimit = sbyte.Parse(source[20..22], NumberStyles.HexNumber);
            temp.Reason = byte.Parse(source[22..24], NumberStyles.HexNumber);
            temp.InputState = byte.Parse(source[24..26], NumberStyles.HexNumber);

            return temp;
        }
    }
}