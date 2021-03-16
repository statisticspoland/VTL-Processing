namespace StatisticsPoland.VtlProcessing.Core.Infrastructure.Interfaces
{
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;
    using System.Collections.Generic;

    /// <summary>
    /// Interface of an aggregator of data models.
    /// </summary>
    public interface IDataModelAggregator : IDataModel
    {
        /// <summary>
        /// Gets the default namespace name.
        /// </summary>
        new string DefaultNamespace { get; set; }

        /// <summary>
        /// Gets or sets the data model collection.
        /// </summary>
        ICollection<IDataModel> DataModels { get; set; }
    }
}
