using System;
using System.Globalization;

namespace VegaIoTWebService.Data.Models
{
    public class Utilits
    {
        public static Span<byte> GetSpan(Span<byte> span, string source)
        {
            for (int i = 0; i < span.Length; i++)
                span[i] = byte.Parse(source.AsSpan(i * 2, 2), NumberStyles.HexNumber);

            return span;
        }

        public static DateTime GetDateTime(Span<byte> convertedSource)
        {
            return new DateTime(1970, 1, 1).AddSeconds(BitConverter.ToUInt32(convertedSource[0..4]));
        }
    }
}