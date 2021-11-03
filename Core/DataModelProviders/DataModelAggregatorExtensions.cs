namespace StatisticsPoland.VtlProcessing.Core.DataModelProviders
{
    using StatisticsPoland.VtlProcessing.Core.DataModelProviders.Infrastructure;
    using StatisticsPoland.VtlProcessing.Core.DataModelProviders.Infrastructure.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Models.Logical;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// The <see cref="IDataModelAggregator"/> extensions.
    /// </summary>
    public static class DataModelAggregatorExtensions
    {
        /// <summary>
        /// Adds a JSON model to the data models aggregator.
        /// </summary>
        /// <param name="aggregator">The data models aggregator.</param>
        /// <param name="filePath">The file path of JSON file.</param>
        public static void AddJsonModel(this IDataModelAggregator aggregator, string filePath)
        {
            aggregator.DataModelsCollection.Add(new JsonDataModelProvider(aggregator, filePath));
        }

        /// <summary>
        /// Add a TSQL model to the data models aggregator.
        /// </summary>
        /// <param name="aggregator">The data models aggregator.</param>
        /// <param name="connectionString">The TSQL database connection string.</param>
        public static void AddSqlServerModel(this IDataModelAggregator aggregator, string connectionString)
        {
            IDataModelProvider dataModel = new SqlServerDataModelProvider(
                aggregator,
                (compName, compType, dataType) => { return new DataStructure(); },
                connectionString,
                aggregator.EnvironmentMapper);

            aggregator.DataModelsCollection.Add(dataModel);
        }

        /// <summary>
        /// Adds a dictionary data model the the data models aggregator.
        /// </summary>
        /// <param name="aggregator">The data models aggregator.</param>
        /// <param name="modelConfiguration">The dictionary data model configuration.</param>
        /// <param name="namespaceName">The namespace name of data model structures.</param>
        public static void AddDictionaryModel(this IDataModelAggregator aggregator, Action<IDictionaryModelConfiguration> modelConfiguration, string namespaceName)
        {
            Dictionary<string, IDataStructure> dataStructures = new Dictionary<string, IDataStructure>();
            modelConfiguration(new DictionaryModelConfiguration(dataStructures));
            IDataModelProvider dataModel = new DictionaryDataModelProvider(aggregator, namespaceName, dataStructures);
            aggregator.DataModelsCollection.Add(dataModel);
        }

        /// <summary>
        /// Adds a SDMX data model to the data models aggregator.
        /// </summary>
        /// <param name="aggregator">The data models aggregator.</param>
        /// <param name="url">The SDMX model url address.</param>
        /// <param name="namespaceName">The namespace name of data model structures.</param>
        public static void AddSdmxModel(this IDataModelAggregator aggregator, string url, string namespaceName)
        {
            IDataModelProvider dataModel = new SdmxDataModelProvider(aggregator, namespaceName, url);
            aggregator.DataModelsCollection.Add(dataModel);
        }
    }
}
