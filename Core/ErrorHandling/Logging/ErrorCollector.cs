namespace StatisticsPoland.VtlProcessing.Core.ErrorHandling.Logging
{
    using Microsoft.Extensions.Logging;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Performs logging and collecting errors.
    /// </summary>
    public class ErrorCollector : ILogger
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ErrorCollector"/> class.
        /// </summary>
        public ErrorCollector()
        {
            this.Errors = new List<Exception>();
        }

        /// <summary>
        /// Gets the errors list.
        /// </summary>
        public List<Exception> Errors { get; }

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
