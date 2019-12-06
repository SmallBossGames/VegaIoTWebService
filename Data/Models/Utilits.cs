using System;
using System.Globalization;

namespace VegaIoTWebService.Data.Models
{
    public static class Utilits
    {
        public static Span<byte> ConvertHexString(Span<byte> buffer, string source)
        {
            for (int i = 0; i < buffer.Length; i++)
                buffer[i] = byte.Parse(source.AsSpan(i * 2, 2), NumberStyles.HexNumber);

            return buffer;
        }
    }
}