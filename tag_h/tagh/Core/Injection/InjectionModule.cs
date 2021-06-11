using System;
using System.Collections.Generic;
using System.Linq;

namespace tagh.Core.Injection
{
    class MultipleInjectionPointsFoundException : Exception { };

    static class InjectionModule
    {
        private static IEnumerable<Type> AllTypes =>
            AppDomain
                .CurrentDomain
                .GetAssemblies()
                .SelectMany(assembly => assembly.GetTypes());

        private static IEnumerable<Type> GetInjectableInterfaces()
        {
            return AllTypes.Where(type => type.IsDefined(typeof(Injectable), false));
        }

        private static Type GetImplementation(Type serviceEntry)
        {
            var implementations = AllTypes.Where(implementationd => serviceEntry.IsAssignableFrom(implementationd) && implementationd != serviceEntry);

            if (implementations.Count() != 1) throw new MultipleInjectionPointsFoundException();

            return implementations.First();
        }

        public static IEnumerable<(Type, Type)> GetInjectionDefinitions()
        {
            return GetInjectableInterfaces()
                .Select(parent => (parent, GetImplementation(parent)));
        }
    }
}
