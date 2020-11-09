namespace StatisticsPoland.VtlProcessing.Core.ErrorHandling.Logging
{
    using Microsoft.Extensions.Logging;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure;
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
            this.Warnings = new List<Exception>();
        }

        /// <summary>
        /// Gets the error list.
        /// </summary>
        public List<Exception> Errors { get; }

        /// <summary>
        /// Gets the warning list.
        /// </summary>
        public List<Exception> Warnings { get; }

        public bool IsEnabled(LogLevel logLevel)
        {
            return logLevel.In(LogLevel.Error, LogLevel.Critical, LogLevel.Warning);
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (!IsEnabled(logLevel)) return;

            if (logLevel == LogLevel.Warning) this.Warnings.Add(exception);
            else this.Errors.Add(exception);
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }
    }
}
