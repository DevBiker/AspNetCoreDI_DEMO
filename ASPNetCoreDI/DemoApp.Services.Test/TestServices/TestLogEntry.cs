using System;
using Microsoft.Extensions.Logging;

namespace DemoApp.Services.Test.TestServices
{
    public abstract class TestLogEntry
    {
        public LogLevel LogLevel { get; set; }
        public EventId EventId { get; set; }

        public Exception Exception { get; set; }

        public string Output { get; set; }

    }

    public class TestLogEntry<TState> : TestLogEntry
    {
        public TState State { get; set; }

        public override string ToString()
        {
            return Exception == null ?
                $"Log: {LogLevel}, EventID: ID={EventId.Id};Name={EventId.Name}, Output={Output}" :
                $"Log: {LogLevel}, EventID: ID={EventId.Id};Name={EventId.Name}, ExceptionType={Exception.GetType().Name}, Output={Output}";
        }
    }
}