namespace StatisticsPoland.VtlProcessing.Core.Logging
{
    using Microsoft.Extensions.Logging;
    using System.Collections.Generic;
    using System.Linq;

    public sealed class ErrorCollectorProvider : ILoggerProvider
    {
        public ErrorCollectorProvider()
        {
            this.ErrorCollectors = new List<ErrorCollector>();
        }

        public List<ErrorCollector> ErrorCollectors { get; set; }

        public IEnumerable<TResult> GetOfType<TResult>()
        {
            return this.ErrorCollectors.SelectMany(s => s.Errors).ToList().OfType<TResult>();
        }

        public ILogger CreateLogger(string categoryName)
        {
            ErrorCollector errorCounter = new ErrorCollector();
            this.ErrorCollectors.Add(errorCounter);

            return errorCounter;
        }

        public void Dispose()
        {
        }
    }
}
