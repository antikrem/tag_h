using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Serilog.Events;

using tag_h.Injection;
using tag_h.Injection.Typing;


namespace tag_h.Controllers
{

    [UsedByClient]
    public record LogEntry
    (
        string Time,
        string Body, 
        IReadOnlyDictionary<string, string> Context
    );

    [ApiController]
    [Route("[controller]")]
    public class LoggingController : ControllerBase
    {

        private readonly INonPersistentLogSinks _logSink;

        public LoggingController(INonPersistentLogSinks logSink)
        {
            _logSink = logSink;
        }

        [HttpGet]
        [Route("[action]")]
        public IReadOnlyList<LogEntry> Get()
        {
            return _logSink
                .GetLogs()
                .Select(ConvertToEntry)
                .ToList();
        }

        static private LogEntry ConvertToEntry(LogEvent logEvent)
        {
            return new LogEntry(
                logEvent.Timestamp.DateTime.ToString(),
                logEvent.RenderMessage(),
                logEvent.Properties.ToDictionary(property => property.Key, property => property.Value.ToString())
            );
        }
    }
}
