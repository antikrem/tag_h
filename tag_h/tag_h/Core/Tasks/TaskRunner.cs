using System.Collections.Concurrent;
using System.Diagnostics;
using System.Threading;

using Serilog;

using tag_h.Core.Persistence;
using tag_h.Injection;

namespace tag_h.Core.Tasks
{
    [Injectable]

    public interface ITaskRunner : IStopOnDejection
    {
        void Submit(ITask task);
    }

    public class TaskRunner : ITaskRunner
    {
        private readonly BlockingCollection<ITask> _taskQueue
                = new BlockingCollection<ITask>(new ConcurrentQueue<ITask>());

        private readonly Thread _taskHandler;
        private readonly ILogger _logger;
        private readonly IHImageRepository _imageRepository;

        public TaskRunner(ILogger logger, IHImageRepository imageRepository)
        {
            _logger = logger;
            _imageRepository = imageRepository;
            _taskHandler = new Thread(ExecuteHandling);
            _taskHandler.Start();
        }

        void ExecuteHandling()
        {
            ITask task;
            while ((task = _taskQueue.Take()) != null)
            {
                Stopwatch stopWatch = Stopwatch.StartNew();
                _logger.Information("Starting task: {Task Name}", task.TaskName);

                task.Execute(_imageRepository);

                stopWatch.Stop();
                _logger.Information("Completed in: {Time}", stopWatch.Elapsed);
            }
        }

        public void Submit(ITask task)
        {
            _taskQueue.Add(task);
        }

        public void Stop()
        {
            _taskQueue.Add(null);
            _taskHandler.Join();
        }
    }
}
