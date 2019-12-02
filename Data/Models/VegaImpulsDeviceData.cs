using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Globalization;

namespace VegaIoTWebService.Data.Models
{
    public class VegaImpulsDeviceData // счётчик импульсов
    {
        public long Id { get; set; }
        public VegaTempDevice? Device { get; set; }
        public long DeviceId { get; set; }
        public byte PackageType { get; set; }
        public short BatteryLevel { get; set; }
        public byte MainSettings { get; set; }
        public DateTime UpTime { get; set; }
        public short Temperature { get; set; }
        public byte InputState_1 { get; set; }
        public byte InputState_2 { get; set; }
        public byte InputState_3 { get; set; }
        public byte InputState_4 { get; set; }

        public VegaImpulsDeviceData Parse(string source)
        {
            const int byteSize = 24;
            const int charSpanSize = byteSize * 2;

            if (source.Length < charSpanSize) throw new FormatException("Length is too small!");

            VegaImpulsDeviceData device = new VegaImpulsDeviceData();

            device.PackageType = byte.Parse(source[0..2], NumberStyles.HexNumber);
            device.BatteryLevel = byte.Parse(source[2..4], NumberStyles.HexNumber);
            device.MainSettings = byte.Parse(source[4..6], NumberStyles.HexNumber);

            Span<byte> convertedSourceTime = stackalloc byte[4];
            convertedSourceTime = Utilits.GetSpan(convertedSourceTime, source[6..14]);
            device.UpTime = Utilits.GetDateTime(convertedSourceTime);

            device.Temperature = byte.Parse(source[14..16], NumberStyles.HexNumber);

            Span<byte> convertedInputState_1 = stackalloc byte[4];
            convertedInputState_1 = Utilits.GetSpan(convertedInputState_1, source[16..24]);
            Span<byte> convertedInputState_2 = stackalloc byte[4];
            convertedInputState_2 = Utilits.GetSpan(convertedInputState_2, source[24..32]);
            Span<byte> convertedInputState_3 = stackalloc byte[4];
            convertedInputState_3 = Utilits.GetSpan(convertedInputState_3, source[32..40]);
            Span<byte> convertedInputState_4 = stackalloc byte[4];
            convertedInputState_4 = Utilits.GetSpan(convertedInputState_4, source[40..48]);

            return device;
        }
    }
}