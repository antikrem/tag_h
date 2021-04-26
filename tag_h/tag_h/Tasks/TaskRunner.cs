using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using tag_h.Persistence;

namespace tag_h.Tasks
{
    class TaskRunner
    {
        private readonly BlockingCollection<ITask> _taskQueue
                = new BlockingCollection<ITask>(new ConcurrentQueue<ITask>());

        private readonly Thread _taskHandler;

        private readonly ImageDatabase _database;

        public TaskRunner(ImageDatabase database)
        {
            _database = database;

            _taskHandler = new Thread(this.ExecuteHandling);
            _taskHandler.Start();
        }

        void ExecuteHandling()
        {
            ITask task;
            while ((task = _taskQueue.Take()) != null)
            {
                Stopwatch stopWatch = Stopwatch.StartNew();

                task.Execute(_database);

                stopWatch.Stop();
                Console.WriteLine($"Taken time: {stopWatch.Elapsed}");
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
