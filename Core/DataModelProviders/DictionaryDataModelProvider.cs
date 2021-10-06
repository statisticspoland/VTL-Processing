namespace StatisticsPoland.VtlProcessing.Core.DataModelProviders
{
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// The regular VTL 2.0 data model.
    /// </summary>
    public class DictionaryDataModelProvider : DataModelProviderBase
    {
        private readonly Dictionary<string, IDataStructure> dataStructures;

        /// <summary>
        /// Initializes a new instance of the <see cref="DictionaryDataModelProvider"/> class.
        /// </summary>
        /// <param name="rootModel">The root data model.</param>
        /// <param name="namespaceName">The namespace name.</param>
        /// <param name="dataStructures">The dictionary of structures.</param>
        public DictionaryDataModelProvider(IDataModelProvider rootModel, string namespaceName, Dictionary<string, IDataStructure> dataStructures)
            : base(rootModel)
        {
            this.Namespace = namespaceName;
            this.dataStructures = dataStructures;
        }

        public override IDataStructure GetDatasetStructure(string datasetName)
        {
            string @namespace;
            this.SplitDatasetName(datasetName, out @namespace, out datasetName);

            if (@namespace == this.Namespace && this.dataStructures.ContainsKey(datasetName))
                return this.dataStructures[datasetName];

            return null;
        }
    }
}
