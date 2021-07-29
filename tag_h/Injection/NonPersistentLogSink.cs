using System.Collections;
using System.Collections.Generic;

using Serilog.Core;
using Serilog.Events;

namespace tag_h.Injection
{
    public interface INonPersistentLogSinks : IEnumerable<LogEvent> 
    {
        public IReadOnlyList<LogEvent> GetLogs();
    }

    public class NonPersistentLogSinks : ILogEventSink, INonPersistentLogSinks
{
        readonly List<LogEvent> _nonPersistentLogStore = new();

        public void Emit(LogEvent logEvent)
        {
            _nonPersistentLogStore.Add(logEvent);            
        }

        public IReadOnlyList<LogEvent> GetLogs()
        {
            return _nonPersistentLogStore;
        }

        public IEnumerator<LogEvent> GetEnumerator()
        {
            return _nonPersistentLogStore.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _nonPersistentLogStore.GetEnumerator();
        }
    }
}