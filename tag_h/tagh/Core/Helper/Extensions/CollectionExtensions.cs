using System;
using System.Collections.Generic;

namespace tagh.Core.Helper.Extensions
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
    }
}
