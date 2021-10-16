using System;
using System.Collections.Generic;
using System.Linq;

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

        //TODO: tests
        public static IEnumerable<T> ToEnumerable<T>(this T element) 
            => new List<T> { element };

        //TODO: tests
        public static bool None<T>(this IEnumerable<T> sequence)
            => !sequence.Any();

        //TODO: tests
        public static T ChainCall<T, S>(this T target, IEnumerable<S> sequence, Func<T, S, T> action)
            => sequence.Any()
                ? action(target, sequence.First())
                    .ChainCall(sequence.Skip(1), action)
                : target;
    }
}
