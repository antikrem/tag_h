using System.Linq;

using tag_h.Injection.DI;


namespace tag_h.Core.Tasks
{
    [Injectable]
    public interface ITaskFactory
    {
        T CreateTask<T>() where T : ITask;
    }

    public class TaskFactory : ITaskFactory
    {
        private readonly IServiceLocator _serviceProvider;

        public TaskFactory(IServiceLocator serviceLocator)
        {
            _serviceProvider = serviceLocator;
        }

        public T CreateTask<T>() where T : ITask
        {
            var constructor = typeof(T)
                .GetConstructors()
                .First();

            var parameters = constructor
                .GetParameters()
                .Select(parameter => _serviceProvider.Resolve(parameter.ParameterType));

            return (T)constructor.Invoke(parameters.ToArray());
        }
    }
}
