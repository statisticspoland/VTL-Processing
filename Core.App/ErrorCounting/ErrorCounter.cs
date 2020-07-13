using Microsoft.Extensions.Logging;
using System;

namespace Core.App.ErrorCounting
{
    public sealed class ErrorCounter : ILogger
    {
        public ErrorCounter()
        {
            this.Count = 0;
        }

        public int Count { get; set; }

        public bool IsEnabled(LogLevel logLevel)
        {
            return logLevel == (LogLevel.Error | LogLevel.Critical);
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (!IsEnabled(logLevel)) return;

            this.Count++;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }
    }
}
