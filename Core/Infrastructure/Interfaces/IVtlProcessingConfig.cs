namespace StatisticsPoland.VtlProcessing.Core.Infrastructure.Interfaces
{
    /// <summary>
    /// The configuration of a VTL 2.0 translator interface.
    /// </summary>
    public interface IVtlProcessingConfig
    {
        /// <summary>
        /// Gets or sets the default data model namespace.
        /// </summary>
        string DefaultNamespace { get; set; }

        /// <summary>
        /// Gets the data models aggregator.
        /// </summary>
        IDataModelAggregator DataModels { get; }
    }
}