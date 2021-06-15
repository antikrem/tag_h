using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Threading;

using tag_h.Core.Injection;
using tag_h.Core.Persistence;


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

        private readonly IHImageRepository _imageRepository;

        public TaskRunner(IHImageRepository imageRepository)
        {
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
                Console.WriteLine($"TASK: Starting task: {task.TaskName}");

                task.Execute(_imageRepository);

                stopWatch.Stop();
                Console.WriteLine($"TASK: Completed in {stopWatch.Elapsed}");
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
