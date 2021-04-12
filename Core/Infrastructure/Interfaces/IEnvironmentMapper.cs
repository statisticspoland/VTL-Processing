namespace StatisticsPoland.VtlProcessing.Core.Infrastructure.Interfaces
{
    using System.Collections.Generic;

    /// <summary>
    /// Interface of an environment names mapper.
    /// </summary>
    public interface IEnvironmentMapper
    {
        /// <summary>
        /// Gets or sets the mapping.
        /// </summary>
        Dictionary<string, string> Mapping { get; set; }

        /// <summary>
        /// Maps a dataset name between a data model and a physical environment.
        /// </summary>
        /// <param name="datasetName">The logical name of the dataset in the data model.</param>
        /// <returns>The physical name of a database structure (i.e. table) in a target environment.</returns>
        string Map(string datasetName);
    }
}
