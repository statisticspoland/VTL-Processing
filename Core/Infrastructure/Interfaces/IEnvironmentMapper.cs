namespace StatisticsPoland.VtlProcessing.Core.Infrastructure.Interfaces
{
    using System.Collections.Generic;

    /// <summary>
    /// Interface of an environment names mapper.
    /// </summary>
    public interface IEnvironmentMapper
    {
        /// <summary>
        /// Gets or sets the mapping dictionary.
        /// </summary>
        Dictionary<string, string> Mapping { get; set; }

        /// <summary>
        /// Gets the default namespace of a data models aggregator being used.
        /// </summary>
        string DefaultNamespace { get; }

        /// <summary>
        /// Gets or sets the default target prefix used when mapping dataset name has not been found in mapping dictionary keys.
        /// </summary>
        string DefaultTargetPrefix { get; set; }

        /// <summary>
        /// Maps a dataset name between a data model and a physical environment.
        /// </summary>
        /// <param name="datasetName">The logical name of the dataset in a data model.</param>
        /// <returns>The physical name of a database structure (i.e. table) in a target environment.</returns>
        string Map(string datasetName);
    }
}
