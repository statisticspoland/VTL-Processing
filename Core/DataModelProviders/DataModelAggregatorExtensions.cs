namespace StatisticsPoland.VtlProcessing.Core.DataModelProviders
{
    using StatisticsPoland.VtlProcessing.Core.DataModelProviders.Infrastructure;
    using StatisticsPoland.VtlProcessing.Core.DataModelProviders.Infrastructure.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.DataModelProviders.Models;
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
            aggregator.DataModels.Add(new DataModelJson(aggregator, filePath));
        }

        public static void AddSqlServerModel(this IDataModelAggregator aggregator, string connectionString)
        {
            IDataModel dataModel = new DataModelSqlServer(
                aggregator,
                (compName, compType, dataType) => { return new DataStructure(); },
                connectionString,
                aggregator.EnvironmentMapper);

            aggregator.DataModels.Add(dataModel);
        }

        public static void AddRegularModel(this IDataModelAggregator aggregator, Action<IRegularModelConfiguration> modelConfiguration, string namespaceName)
        {
            Dictionary<string, IDataStructure> dataStructures = new Dictionary<string, IDataStructure>();
            modelConfiguration(new RegularModelConfiguration(dataStructures));
            IDataModel dataModel = new DataModelRegular(aggregator, namespaceName, dataStructures);
            aggregator.DataModels.Add(dataModel);
        }
    }
}
