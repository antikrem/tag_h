using System;

namespace tagh.Core.Helper.Extensions
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
    }
}
