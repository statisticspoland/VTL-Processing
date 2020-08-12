namespace StatisticsPoland.VtlProcessing.DataModel
{
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;
    using StatisticsPoland.VtlProcessing.DataModel.Infrastructure;
    using StatisticsPoland.VtlProcessing.DataModel.Infrastructure.Interfaces;
    using StatisticsPoland.VtlProcessing.DataModel.Models;
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
