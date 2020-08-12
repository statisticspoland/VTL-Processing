namespace StatisticsPoland.VtlProcessing.Core.Infrastructure.Interfaces
{
    using Microsoft.Extensions.DependencyInjection;
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;

    /// <summary>
    /// The configuration of a VTL 2.0 translator interface.
    /// </summary>
    public interface IVtlProcessingConfig
    {
        /// <summary>
        /// Gets the service collection.
        /// </summary>
        IServiceCollection Services { get; }

        /// <summary>
        /// Gets or sets the default data model namespace.
        /// </summary>
        string DefaultNamespace { get; set; }

        /// <summary>
        /// Adds a data model to the translator's memory.
        /// </summary>
        /// <param name="dataModel">The data model.</param>
        /// <returns>The inscance of <see cref="IVtlProcessingConfig"/>.</returns>
        IVtlProcessingConfig AddModel(IDataModel dataModel);
    }
}