using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tag_h.Helper.Extensions
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
