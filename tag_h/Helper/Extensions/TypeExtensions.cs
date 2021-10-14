using System;

namespace tag_h.Helper.Extensions
{
    static class TypeExtensions
    {
        public static bool IsSimple(this Type type)
        {
            return type.IsPrimitive
                || type.IsEnum
                || type.Equals(typeof(string))
                || type.Equals(typeof(decimal))
                || type.Equals(typeof(TimeSpan));
        }
    }
}
