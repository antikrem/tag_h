using System.Collections.Concurrent;
using System.Diagnostics;
using System.Threading;

using EphemeralEx.Injection;
using Serilog;

using tag_h.Core.Persistence;
using tag_h.Core.TagRetriever;


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
        private readonly ITagRepository _tagRepository;
        private readonly IImageHasher _imageHasher;
        private readonly IAutoTagger _autoTagger;

        public TaskRunner(ILogger logger, IHImageRepository imageRepository, ITagRepository tagRepository, IImageHasher imageHasher, IAutoTagger autoTagger)
        {
            _logger = logger;

            _imageRepository = imageRepository;
            _tagRepository = tagRepository;
            _imageHasher = imageHasher;
            _autoTagger = autoTagger;

            _taskHandler = new Thread(ExecuteHandling);
            _taskHandler.Start();
        }

        void ExecuteHandling()
        {
            ITask task;
            while ((task = _taskQueue.Take()) != null)
            {
                Stopwatch stopWatch = Stopwatch.StartNew();
                _logger.Information("Starting task: {TaskName}", task.TaskName);

                task.Execute(_imageRepository, _tagRepository, _imageHasher, _autoTagger);

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
