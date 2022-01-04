using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

using EphemeralEx.Injection;
using Serilog;


namespace tag_h.Core.Tasks
{
    [Injectable]
    public interface ITaskRunner
    {
        public Task Execute<ConfiguredTask, TConfiguration>(TConfiguration config)
            where ConfiguredTask : ITask<TConfiguration>;
    }

    public class BackgroundTaskRunner : ITaskRunner
    {
        private readonly ILogger _logger;
        private readonly IServiceLocator _serviceProvider;

        public BackgroundTaskRunner(ILogger logger, IServiceLocator serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        public async Task Execute<TConfiguredTask, TConfiguration>(TConfiguration config)
            where TConfiguredTask : ITask<TConfiguration>
        {
            var task = CreateTask<TConfiguredTask, TConfiguration>();
            await DecoratedBackgroundTask(task, config);
        }

        private async Task DecoratedBackgroundTask<TConfiguredTask, TConfiguration>(TConfiguredTask task, TConfiguration config)
            where TConfiguredTask : ITask<TConfiguration>
        {
            var stopWatch = Stopwatch.StartNew();
            _logger.Information("Starting task: {TaskName}", task.Name);

            await task.Run(config);

            stopWatch.Stop();
            _logger.Information("Completed in: {Time}", stopWatch.Elapsed);
        }

        private TConfiguredTask CreateTask<TConfiguredTask, TConfiguration>()
            where TConfiguredTask : ITask<TConfiguration>
        {
            var constructor = typeof(TConfiguredTask)
                .GetConstructors()
                .First();

            try
            {
                var parameters = constructor
                .GetParameters()
                .Select(parameter => _serviceProvider.Resolve(parameter.ParameterType))
                .ToArray();

                return (TConfiguredTask)constructor.Invoke(parameters);
            }
            catch (ServiceNotFoundException e)
            {
                _logger
                    .ForContext("Parameters", constructor.GetParameters())
                    .Error(e, "Unabled to generate {Type} task", typeof(TConfiguredTask).GetType().Name);
                throw;
            }

        }
    }
}