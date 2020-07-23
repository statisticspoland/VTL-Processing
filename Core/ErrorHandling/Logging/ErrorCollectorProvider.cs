namespace StatisticsPoland.VtlProcessing.Core.ErrorHandling.Logging
{
    using Microsoft.Extensions.Logging;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Creates and stores instances of <see cref="ErrorCollector"/> - <see cref="ILogger"/> representation.
    /// </summary>
    public class ErrorCollectorProvider : ILoggerProvider
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ErrorCollectorProvider"/> class.
        /// </summary>
        public ErrorCollectorProvider()
        {
            this.ErrorCollectors = new List<ErrorCollector>();
        }

        /// <summary>
        /// Gets the list of <see cref="ErrorCollector"/> instances.
        /// </summary>
        public List<ErrorCollector> ErrorCollectors { get; }

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
