namespace Core.App.ErrorCounting
{
    using Microsoft.Extensions.Logging;
    using System.Collections.Generic;

    public sealed class ErrorCounterProvider : ILoggerProvider
    {
        public ErrorCounterProvider()
        {
            this.ErrorCounters = new List<ErrorCounter>();
        }

        public List<ErrorCounter> ErrorCounters { get; set; }

        public ILogger CreateLogger(string categoryName)
        {
            ErrorCounter errorCounter = new ErrorCounter();
            this.ErrorCounters.Add(errorCounter);

            return errorCounter;
        }

        public void Dispose()
        {
        }
    }
}
