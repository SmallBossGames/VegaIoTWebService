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
        public byte BatteryLevel { get; set; }
        public byte MainSettingsBits { get; set; }
        public double Temperature { get; set; }
        public byte Reason { get; set; }
        public DateTimeOffset Uptime { get; set; }

        public static VegaMoveDeviceData Parse(string source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            const int byteSize = 10;
            const int charSpanSize = byteSize * 2;

            if (source.Length < charSpanSize)
                throw new FormatException("Length is too small");

            var temp = new VegaMoveDeviceData();

            temp.BatteryLevel = byte.Parse(source[2..4], NumberStyles.HexNumber);
            temp.MainSettingsBits = byte.Parse(source[4..6], NumberStyles.HexNumber);

            Span<byte> convertedSource = stackalloc byte[7];
            for (var i = 0; i < convertedSource.Length; i++)
            {
                var tempSpan = source.AsSpan(i * 2 + 6, 2);
                convertedSource[i] = byte.Parse(tempSpan, NumberStyles.HexNumber);
            }

            temp.Temperature = BitConverter.ToInt16(convertedSource[0..2]) / 10.0;
            temp.Reason = convertedSource[2];
            temp.Uptime = DateTimeOffset.FromUnixTimeSeconds(BitConverter.ToInt32(convertedSource[3..7]));

            return temp;
        }
    }
}