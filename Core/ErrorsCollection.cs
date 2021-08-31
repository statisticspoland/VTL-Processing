namespace StatisticsPoland.VtlProcessing.Core
{
    using StatisticsPoland.VtlProcessing.Core.ErrorHandling.Logging;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class ErrorsCollection
    {
        private readonly ErrorCollectorProvider _errorProvider;

        public ErrorsCollection(ErrorCollectorProvider errorProvider)
        {
            this._errorProvider = errorProvider;
        }

        public int Count { get => this._errorProvider.ErrorCollectors.Sum(errCollector => errCollector.Errors.Count); }

        public IEnumerable<Exception> GetErrors()
        {
            return this._errorProvider.ErrorCollectors.SelectMany(errors => errors.Errors);
        }

        public IEnumerable<TResult> GetErrors<TResult>()
        {
            return this._errorProvider.ErrorCollectors.SelectMany(errors => errors.Errors).OfType<TResult>();
        }
    }
}
