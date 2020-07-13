namespace StatisticsPoland.VtlProcessing.Core.Logging
{
    using Microsoft.Extensions.Logging;
    using System;
    using System.Collections.Generic;

    public sealed class ErrorCollector : ILogger
    {
        public ErrorCollector()
        {
            this.Errors = new List<Exception>();
        }

        public List<Exception> Errors { get; set; }

        public bool IsEnabled(LogLevel logLevel)
        {
            return logLevel == (LogLevel.Error | LogLevel.Critical);
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (!IsEnabled(logLevel)) return;

            this.Errors.Add(exception);
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }
    }
}
