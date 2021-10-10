using Serilog;
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
        private readonly ILogger _logger;
        private readonly IServiceLocator _serviceProvider;

        public TaskFactory(ILogger logger, IServiceLocator serviceLocator)
        {
            _logger = logger;
            _serviceProvider = serviceLocator;
        }

        public T CreateTask<T>() where T : ITask
        {
            var constructor = typeof(T)
                .GetConstructors()
                .First();

            try
            {
                var parameters = constructor
                .GetParameters()
                .Select(parameter => _serviceProvider.Resolve(parameter.ParameterType))
                .ToArray();

                return (T)constructor.Invoke(parameters);
            }
            catch (ServiceNotFoundException e)
            {
                _logger
                    .ForContext("Parameters", constructor.GetParameters())
                    .Error(e, "Unabled to generate {Type} task", typeof(T).GetType().Name);
                throw;
            }
            
        }
    }
}
