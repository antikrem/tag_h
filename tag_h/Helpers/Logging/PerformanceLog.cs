using System;
using System.Diagnostics;

using Serilog;


namespace tag_h.Helpers
{
    public class PerformanceLog : IDisposable
    {
        private readonly Guid _id;
        private readonly Stopwatch _stopwatch;
        private readonly ILogger _logger;

        public PerformanceLog(ILogger logger, string task)
        {
            _id = Guid.NewGuid();

            _stopwatch = new();
            _stopwatch.Start();

            _logger = logger.ForContext("PerformanceLoggingId", _id);
            _logger.Debug("Starting performant task {Task}", task);
        }

        public void Dispose()
        {
            _logger.Debug("Starting performant task {TimeTaken}", _stopwatch.Elapsed);
        }
    }

    public static class LoggerPerfomanceExtensions
    {
        public static PerformanceLog LogPerformance(this ILogger logger, string task)
            => new(logger, task);
    }
}