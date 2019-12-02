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
    }
}