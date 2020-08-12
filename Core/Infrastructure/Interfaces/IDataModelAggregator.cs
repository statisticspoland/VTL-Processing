namespace StatisticsPoland.VtlProcessing.Core.Infrastructure.Interfaces
{
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;
    using System.Collections.Generic;

    /// <summary>
    /// The aggregator of data models interface.
    /// </summary>
    internal interface IDataModelAggregator : IDataModel
    {
        /// <summary>
        /// Gets the data model collection.
        /// </summary>
        ICollection<IDataModel> DataModels { get; }
    }
}
