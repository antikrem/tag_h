using System;
using System.Collections.Generic;


namespace tag_h.Core.Helper.Extensions
{
    static class CollectionExtensions
    {
        public static void ForEach<T>(this IEnumerable<T> sequence, Action<T> action)
        {
            foreach (T t in sequence)
            {
                action(t);
            }
        }

        public static string Join(this IEnumerable<string> sequence, string seperator)
        {
            return string.Join(seperator, sequence);
        }
    }
}
