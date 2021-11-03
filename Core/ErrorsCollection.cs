namespace StatisticsPoland.VtlProcessing.Core
{
    using StatisticsPoland.VtlProcessing.Core.ErrorHandling.Logging;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Collection of translation errors.
    /// </summary>
    public class ErrorsCollection
    {
        private readonly ErrorCollectorProvider _errorProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="ErrorsCollection"/> class.
        /// </summary>
        /// <param name="errorProvider">The collector of error logs..</param>
        public ErrorsCollection(ErrorCollectorProvider errorProvider)
        {
            this._errorProvider = errorProvider;
        }

        /// <summary>
        /// Gets the count of translation errors.
        /// </summary>
        public int Count { get => this._errorProvider.ErrorCollectors.Sum(errCollector => errCollector.Errors.Count); }

        /// <summary>
        /// Gets the collection of translation errors.
        /// </summary>
        /// <returns>The collection of translation errors.</returns>
        public IEnumerable<Exception> GetErrors()
        {
            return this._errorProvider.ErrorCollectors.SelectMany(errors => errors.Errors);
        }

        /// <summary>
        /// Gets the collection of translation errors of a specific type.
        /// </summary>
        /// <typeparam name="TResult">The type of errors to get.</typeparam>
        /// <returns>The collection of translation errors.</returns>
        public IEnumerable<TResult> GetErrors<TResult>()
        {
            return this._errorProvider.ErrorCollectors.SelectMany(errors => errors.Errors).OfType<TResult>();
        }
    }
}
