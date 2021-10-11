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
        public static void AddJsonModel(this IDataModelAggregator aggregator, string filePath)
        {
            aggregator.DataModelsCollection.Add(new JsonDataModelProvider(aggregator, filePath));
        }

        public static void AddSqlServerModel(this IDataModelAggregator aggregator, string connectionString)
        {
            IDataModelProvider dataModel = new SqlServerDataModelProvider(
                aggregator,
                (compName, compType, dataType) => { return new DataStructure(); },
                connectionString,
                aggregator.EnvironmentMapper);

            aggregator.DataModelsCollection.Add(dataModel);
        }

        public static void AddDictionaryModel(this IDataModelAggregator aggregator, Action<IDictionaryModelConfiguration> modelConfiguration, string namespaceName)
        {
            Dictionary<string, IDataStructure> dataStructures = new Dictionary<string, IDataStructure>();
            modelConfiguration(new DictionaryModelConfiguration(dataStructures));
            IDataModelProvider dataModel = new DictionaryDataModelProvider(aggregator, namespaceName, dataStructures);
            aggregator.DataModelsCollection.Add(dataModel);
        }

        public static void AddSdmxModel(this IDataModelAggregator aggregator, string url, string namespaceName)
        {
            IDataModelProvider dataModel = new SdmxDataModelProvider(aggregator, namespaceName, url);
            aggregator.DataModelsCollection.Add(dataModel);
        }
    }
}
