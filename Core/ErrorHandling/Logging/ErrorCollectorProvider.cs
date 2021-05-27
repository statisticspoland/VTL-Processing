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

        /// <summary>
        /// Gets errors of a given type from <see cref="ErrorCollector"/> instances.
        /// </summary>
        /// <typeparam name="TResult">The type of errors.</typeparam>
        /// <returns>The error enumerator.</returns>
        public IEnumerable<TResult> GetErrorsOfType<TResult>()
        {
            return this.ErrorCollectors.SelectMany(s => s.Errors).ToList().OfType<TResult>();
        }

        public ILogger CreateLogger(string categoryName)
        {
            ErrorCollector errorCollector = new ErrorCollector();
            this.ErrorCollectors.Add(errorCollector);

            return errorCollector;
        }

        public void Dispose()
        {
            // nothing to dispose of
        }
    }
}
