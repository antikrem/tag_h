using System;
using System.Collections.Generic;
using System.Linq;

namespace tag_h.Injection
{
    internal static class ReflectionHelper
    {
        public static IEnumerable<Type> AllTypes =>
            AppDomain
                .CurrentDomain
                .GetAssemblies()
                .SelectMany(assembly => assembly.GetTypes());
    }
}