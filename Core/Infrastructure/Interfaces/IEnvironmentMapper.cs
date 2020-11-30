using System.Collections.Generic;

namespace StatisticsPoland.VtlProcessing.Core.Infrastructure.Interfaces
{
    /// <summary>
    /// Interface of an environment names mapper.
    /// </summary>
    public interface IEnvironmentMapper
    {
        Dictionary<string, string> Mapping { get; }

        /// <summary>
        /// Maps a dataset name between a data model and a physical environment.
        /// </summary>
        /// <param name="datasetName">The logical name of the dataset in the data model.</param>
        /// <returns>The physical name of a database structure (i.e. table) in a target environment.</returns>
        string Map(string datasetName);
    }
}
