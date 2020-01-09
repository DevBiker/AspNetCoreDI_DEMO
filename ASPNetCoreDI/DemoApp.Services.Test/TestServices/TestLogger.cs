using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Microsoft.Extensions.Logging;
using NUnit.Framework.Internal;

namespace DemoApp.Services.Test.TestServices
{
    public class TestLogger : Microsoft.Extensions.Logging.ILogger
    {
        private readonly TextWriter _outputTextWriter;
        public TestLogger()
        {

        }

        public TestLogger(TextWriter outputWriter)
        {
            _outputTextWriter = outputWriter;

        }

        public List<TestLogEntry> LogEntries { get; } = new List<TestLogEntry>();

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            var entry = new TestLogEntry<TState>
            {
                LogLevel = logLevel,
                EventId = eventId,
                State = state,
                Exception = exception,
                Output = formatter.Invoke(state, exception)
            };

            LogEntries.Add(entry);
            Debug.WriteLine(entry.ToString());
            if (_outputTextWriter != null)
            {
                _outputTextWriter.WriteLine(entry.ToString());
            }
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            throw new NotImplementedException();
        }
    }

    public class TestLogger<T> : TestLogger, ILogger<T>
    {
        public TestLogger()
        {

        }
        public TestLogger(TextWriter outputWriter) : base(outputWriter)
        {


        }

    }
}