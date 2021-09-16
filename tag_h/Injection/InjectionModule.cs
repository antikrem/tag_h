using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.Extensions.DependencyInjection;

using tag_h.Core.Helper.Extensions;


namespace tag_h.Injection
{
    class MultipleInjectionPointsFoundException : Exception { };

    static class InjectionModule
    {
        public static void AddRegisteredInjections(this IServiceCollection services)
        {
            GetInjectionDefinitions()
                .ForEach(
                        definition => services.Add(CreateServiceDescription(definition))
                    );
        }

        private static IEnumerable<(Type service, Type implementation)> GetInjectionDefinitions()
        {
            return GetInjectableInterfaces()
                .Select(parent => (parent, GetImplementation(parent)));
        }

        private static ServiceDescriptor CreateServiceDescription((Type service, Type implementation) definition)
        {
            return new ServiceDescriptor(definition.service, definition.implementation, ServiceLifetime.Singleton);
        }

        private static Type GetImplementation(Type serviceEntry)
        {

            var implementations = ReflectionHelper
                .AllTypes
                .Where(implementation => serviceEntry.IsAssignableFrom(implementation) && implementation != serviceEntry);

            if (implementations.Count() != 1) throw new MultipleInjectionPointsFoundException();

            return implementations.First();
        }

        private static IEnumerable<Type> GetInjectableInterfaces()
        {
            return ReflectionHelper
                .AllTypes
                .Where(type => type.IsDefined(typeof(Injectable), false));
        }
    }
}
