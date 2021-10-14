using System;


namespace tag_h.Helper.Extensions
{
    static class ValueExtensions
    {
        public static string ToHexString(this ulong value)
        {
            return string.Format("0x{0:X}", value);
        }

        public static ulong ToHexULong(this string value)
        {
            return Convert.ToUInt64(value, 16);
        }

        public static string ToHexString(this byte[] value)
        {
            return BitConverter.ToString(value)
                .Replace("-", "")
                .ToLower();
        }
    }
}
