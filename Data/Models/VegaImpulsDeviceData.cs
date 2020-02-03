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
        public VegaDevice? Device { get; set; }
        public long DeviceId { get; set; }
        public byte PackageType { get; set; }
        public short BatteryLevel { get; set; }
        public byte MainSettings { get; set; }
        public byte AlarmExit { get; set; } // в случае тревоги передаётся другой тип пакета
        public DateTimeOffset UpTime { get; set; }
        public double Temperature { get; set; } // температура приходит без умножения на 10 по докам, как на самом деле не знаю
        public short InputState_1 { get; set; }
        public short InputState_2 { get; set; }
        public short InputState_3 { get; set; }
        public short InputState_4 { get; set; }

        public static VegaImpulsDeviceData Parse(string source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            const int byteSize = 24;
            const int charSpanSize = byteSize * 2;

            if (source.Length < charSpanSize) 
                throw new FormatException("Length is too small!");

            VegaImpulsDeviceData device = new VegaImpulsDeviceData();

            device.PackageType = byte.Parse(source[0..2], NumberStyles.HexNumber);
            device.BatteryLevel = byte.Parse(source[2..4], NumberStyles.HexNumber);
            device.MainSettings = byte.Parse(source[4..6], NumberStyles.HexNumber);

            var convertedSourceTime = Utilits.ConvertHexString(stackalloc byte[4], source[6..14]);
            device.UpTime = DateTimeOffset.FromUnixTimeSeconds(BitConverter.ToUInt32(convertedSourceTime[0..4]));

            device.Temperature = byte.Parse(source[14..16], NumberStyles.HexNumber);

            Span<byte> convertedInputState_1 = stackalloc byte[4];
            convertedInputState_1 = Utilits.ConvertHexString(convertedInputState_1, source[16..24]);
            device.InputState_1 = BitConverter.ToInt16(convertedInputState_1[0..4]);

            Span<byte> convertedInputState_2 = stackalloc byte[4];
            convertedInputState_2 = Utilits.ConvertHexString(convertedInputState_2, source[24..32]);
            device.InputState_2 = BitConverter.ToInt16(convertedInputState_2[0..4]);

            Span<byte> convertedInputState_3 = stackalloc byte[4];
            convertedInputState_3 = Utilits.ConvertHexString(convertedInputState_3, source[32..40]);
            device.InputState_3 = BitConverter.ToInt16(convertedInputState_3[0..4]);

            Span<byte> convertedInputState_4 = stackalloc byte[4];
            convertedInputState_4 = Utilits.ConvertHexString(convertedInputState_4, source[40..48]);
            device.InputState_4 = BitConverter.ToInt16(convertedInputState_4[0..4]);

            return device;
        }

        /*public static VegaImpulsDeviceData ParseAlarm(string source) // не нужен         
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            const int byteSize = 24;
            const int charSpanSize = byteSize * 2;

            if (source.Length < charSpanSize) throw new FormatException("Length is too small!");

            VegaImpulsDeviceData device = new VegaImpulsDeviceData();

            device.PackageType = byte.Parse(source[0..2], NumberStyles.HexNumber);
            device.BatteryLevel = byte.Parse(source[2..4], NumberStyles.HexNumber);
            device.MainSettings = byte.Parse(source[4..6], NumberStyles.HexNumber);
            device.AlarmExit = byte.Parse(source[6..8], NumberStyles.HexNumber);

            Span<byte> convertedSourceTime = stackalloc byte[4];
            convertedSourceTime = Utilits.ConvertHexString(convertedSourceTime, source[8..16]);
            device.UpTime = DateTimeOffset.FromUnixTimeSeconds(BitConverter.ToUInt32(convertedSourceTime[0..4]));

            Span<byte> convertedInputState_1 = stackalloc byte[4];
            convertedInputState_1 = Utilits.ConvertHexString(convertedInputState_1, source[16..24]);
            device.InputState_1 = BitConverter.ToInt16(convertedInputState_1[0..4]);

            Span<byte> convertedInputState_2 = stackalloc byte[4];
            convertedInputState_2 = Utilits.ConvertHexString(convertedInputState_2, source[24..32]);
            device.InputState_2 = BitConverter.ToInt16(convertedInputState_2[0..4]);

            Span<byte> convertedInputState_3 = stackalloc byte[4];
            convertedInputState_3 = Utilits.ConvertHexString(convertedInputState_3, source[32..40]);
            device.InputState_3 = BitConverter.ToInt16(convertedInputState_3[0..4]);

            Span<byte> convertedInputState_4 = stackalloc byte[4];
            convertedInputState_4 = Utilits.ConvertHexString(convertedInputState_4, source[40..48]);
            device.InputState_4 = BitConverter.ToInt16(convertedInputState_4[0..4]);

            return device;
        }*/
    }
}