using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
