using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using VegaIoTApi.Data.Models.Interfaces;

namespace VegaIoTWebService.Data.Models
{
    public class VegaTempDeviceData : IDeviceData // ������ �����������
    {
        public long Id { get; set; }
        public VegaDevice? Device { get; set; }
        public long DeviceId { get; set; }
        public short BatteryLevel { get; set; }
        public bool PushTheLimit { get; set; }
        public DateTimeOffset Uptime { get; set; }
        public double Temperature { get; set; }
        public short LowLimit { get; set; }
        public short HighLimit { get; set; }

        public static VegaTempDeviceData Parse(string source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            const int byteSize = 13;
            const int charSpanSize = byteSize * 2;

            if (source.Length < charSpanSize)
                throw new FormatException("Length is too small");

            var temp = new VegaTempDeviceData();

            temp.BatteryLevel = byte.Parse(source[2..4], NumberStyles.HexNumber);
            temp.PushTheLimit = byte.Parse(source[4..6], NumberStyles.HexNumber) != 0;

            var convertedSource = Utilits.ConvertHexString(stackalloc byte[6], source[6..18]);

            temp.Uptime = DateTimeOffset.FromUnixTimeSeconds(BitConverter.ToInt32(convertedSource[0..4]));
            temp.Temperature = BitConverter.ToInt16(convertedSource[4..6]) / 10.0;
            temp.LowLimit = sbyte.Parse(source[18..20], NumberStyles.HexNumber);
            temp.HighLimit = sbyte.Parse(source[20..22], NumberStyles.HexNumber);

            return temp;
        }
    }
}