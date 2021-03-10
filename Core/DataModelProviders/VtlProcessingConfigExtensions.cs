﻿namespace StatisticsPoland.VtlProcessing.Core.DataModelProviders
{
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Models.Logical;
    using StatisticsPoland.VtlProcessing.Core.DataModelProviders.Infrastructure;
    using StatisticsPoland.VtlProcessing.Core.DataModelProviders.Infrastructure.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.DataModelProviders.Models;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// The <see cref="IVtlProcessingConfig"/> extensions.
    /// </summary>
    public static class VtlProcessingConfigExtensions
    {
        /// <summary>
        /// Adds a data model from a JSON file to the translator configuration.
        /// </summary>
        /// <param name="config">The translator configuration.</param>
        /// <param name="filePath">The JSON file path.</param>
        public static IVtlProcessingConfig AddJsonModel(this IVtlProcessingConfig config, string filePath)
        {
            IDataModel dataModel = new DataModelJson(config.DefaultNamespace, filePath);
            return config.AddModel(dataModel);
        }

        /// <summary>
        /// Adds a data model from a SQL Server to the translator configuration.
        /// </summary>
        /// <param name="config">The translator configuration.</param>
        /// <param name="connectionString">The SQL Server connection string.</param>
        /// <param name="mapping">The dictionary of mapped names.</param>
        public static IVtlProcessingConfig AddSqlServerModel(this IVtlProcessingConfig config, string connectionString, Dictionary<string, string> mapping)
        {
            IDataModel dataModel = new DataModelSqlServer(
                (compName, compType, dataType) => { return new DataStructure(); },
                config.DefaultNamespace,
                connectionString, 
                mapping);

            return config.AddModel(dataModel);
        }

        /// <summary>
        /// Adds a data model defined by an user to the translator configuration.
        /// </summary>
        /// <param name="config">The translator configuration.</param>
        /// <param name="modelConfiguration">The configuration of the data model.</param>
        public static IVtlProcessingConfig AddRegularModel(this IVtlProcessingConfig config, Action<IRegularModelConfiguration> modelConfiguration)
        {
            Dictionary<string, IDataStructure> dataStructures = new Dictionary<string, IDataStructure>();
            modelConfiguration(new RegularModelConfiguration(dataStructures));
            IDataModel dataModel = new DataModelRegular(config.DefaultNamespace, dataStructures);
            return config.AddModel(dataModel);
        }
    }
}