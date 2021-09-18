#nullable enable
using System;

namespace tag_h.Injection
{
    public class ServiceNotFoundException : Exception
    {
        public ServiceNotFoundException(Type type)
            : base($"Service of type {type.Name} count not be resolved")
        { }
    }

    [Injectable]
    public interface IServiceLocator
    {
        T Resolve<T>();

        object Resolve(Type type);
    }

    public class ServiceLocator : IServiceLocator
    {
        private readonly IServiceProvider _serviceProvider;

        public ServiceLocator(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public T Resolve<T>() => (T)Resolve(typeof(T));

        public object Resolve(Type type)
                => _serviceProvider.GetService(type) ?? throw new ServiceNotFoundException(type);
    }
}
