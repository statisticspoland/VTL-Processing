namespace StatisticsPoland.VtlProcessing.Core.DataModelProviders
{
    using StatisticsPoland.VtlProcessing.Core.DataModelProviders.Infrastructure;
    using StatisticsPoland.VtlProcessing.Core.DataModelProviders.Infrastructure.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.DataModelProviders.Models;
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Models.Logical;
    using StatisticsPoland.VtlProcessing.Core.UserInterface;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// The <see cref="DataModelCollection"/> extensions.
    /// </summary>
    public static class DataModelCollectionExtensions
    {
        public static void AddJsonModel(this DataModelCollection collection, string filePath)
        {
            collection.AddModel(new DataModelJson(collection.DefaultNamespace, filePath));
        }

        public static void AddSqlServerModel(this DataModelCollection collection, string connectionString, Dictionary<string, string> mapping)
        {
            IDataModel dataModel = new DataModelSqlServer(
                (compName, compType, dataType) => { return new DataStructure(); },
                collection.DefaultNamespace,
                connectionString,
                mapping);

            collection.AddModel(dataModel);
        }

        public static void AddRegularModel(this DataModelCollection collection, Action<IRegularModelConfiguration> modelConfiguration, string namespaceName)
        {
            Dictionary<string, IDataStructure> dataStructures = new Dictionary<string, IDataStructure>();
            modelConfiguration(new RegularModelConfiguration(dataStructures));
            IDataModel dataModel = new DataModelRegular(collection.DefaultNamespace, namespaceName, dataStructures);
            collection.AddModel(dataModel);
        }
    }
}
