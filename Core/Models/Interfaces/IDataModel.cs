﻿namespace StatisticsPoland.VtlProcessing.Core.Models.Interfaces
{
    /// <summary>
    /// The VTL 2.0 data model Interface.
    /// </summary>
    public interface IDataModel
    {
        /// <summary>
        /// Gets the default namespace name.
        /// </summary>
        string DefaultNamespace { get; }

        /// <summary>
        /// Gets the dataset structure.
        /// </summary>
        /// <param name="datasetName">The name of the dataset.</param>
        /// <returns>The dataset structure.</returns>
        IDataStructure GetDatasetStructure(string datasetName);
    }
}
