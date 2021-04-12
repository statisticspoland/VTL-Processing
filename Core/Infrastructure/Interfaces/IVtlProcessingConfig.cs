namespace StatisticsPoland.VtlProcessing.Core.Infrastructure.Interfaces
{
    /// <summary>
    /// The configuration of a VTL 2.0 translator interface.
    /// </summary>
    public interface IVtlProcessingConfig
    {
        /// <summary>
        /// Gets the data models aggregator.
        /// </summary>
        IDataModelAggregator DataModels { get; }

        /// <summary>
        /// Gets the environment names mapper.
        /// </summary>
        IEnvironmentMapper EnvironmentMapper { get; }
    }
}