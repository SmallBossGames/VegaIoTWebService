using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace VegaIoTWebService.Data.Models
{
    public class VegaTempDeviceData
    {
        public long Id { get; set; }

        public VegaTempDevice? Device { get; set; }

        public long DeviceId { get; set; }

        public short BatteryLevel { get; set; }

        public bool PushTheLimit { get; set; }

        public DateTime Uptime { get; set; }

        public short Temperature { get; set; }

        public short LowLimit { get; set; }

        public short HighLimit { get; set; }

        public static VegaTempDeviceData Parse(string source)
        {
            const int byteSize = 13;
            const int charSapnSize = byteSize * 2;

            if (source.Length < charSapnSize)
                throw new FormatException("Length is too small");

            var temp = new VegaTempDeviceData();

            temp.BatteryLevel = byte.Parse(source[2..4], NumberStyles.HexNumber);
            temp.PushTheLimit = byte.Parse(source[4..6], NumberStyles.HexNumber) != 0;

            Span<byte> convertedSource = stackalloc byte[6];
            for (var i = 0; i < convertedSource.Length; i++)
            {
                var tempSpan = source.AsSpan(i * 2 + 6, 2);
                convertedSource[i] = byte.Parse(tempSpan, NumberStyles.HexNumber);
            }

            temp.Uptime = new DateTime(1970, 1, 1)
                .AddSeconds(BitConverter.ToUInt32(convertedSource[0..4]));

            temp.Temperature = BitConverter.ToInt16(convertedSource[4..6]);

            temp.LowLimit = sbyte.Parse(source[18..20], NumberStyles.HexNumber);
            temp.HighLimit = sbyte.Parse(source[20..22], NumberStyles.HexNumber);

            return temp;
        }
    }
}