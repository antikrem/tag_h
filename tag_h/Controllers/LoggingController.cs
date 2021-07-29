using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Serilog.Events;

using tag_h.Injection;

namespace tag_h.Controllers
{

    public record LogEntry
    (
        DateTime PostTime,
        string Body, 
        IReadOnlyDictionary<string, LogEventPropertyValue> Properties
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
            return new LogEntry(logEvent.Timestamp.DateTime, logEvent.RenderMessage(), logEvent.Properties);
        }
    }
}
