namespace StatisticsPoland.VtlProcessing.Core.DataModelProviders.Models
{
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// The regular VTL 2.0 data model.
    /// </summary>
    public class DataModelRegular : IDataModel
    {
        private Dictionary<string, IDataStructure> dataStructures;

        /// <summary>
        /// Initializes a new instance of the <see cref="DataModelRegular"/> class.
        /// </summary>
        public DataModelRegular()
        {
            this.dataStructures = new Dictionary<string, IDataStructure>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DataModelRegular"/> class.
        /// </summary>
        /// <param name="defaultNamespace">The default namespace name.</param>
        /// <param name="namespaceName">The namespace name.</param>
        /// <param name="dataStructures">The dictionary of structures.</param>
        public DataModelRegular(string defaultNamespace, string namespaceName, Dictionary<string, IDataStructure> dataStructures)
        {
            this.DefaultNamespace = defaultNamespace;
            this.Namespace = namespaceName;
            this.dataStructures = dataStructures;
        }

        public string DefaultNamespace { get; }

        /// <summary>
        /// Gets or sets the namespace name.
        /// </summary>
        public string Namespace { get; set; }

        public IDataStructure GetDatasetStructure(string datasetName)
        {
            string[] split = datasetName.Split(@"\");
            switch (split.Length)
            {
                case 1:
                    if (this.DefaultNamespace != this.Namespace) return null;
                    break;
                case 2:
                    if (split[0] != this.Namespace) return null;
                    datasetName = split[1];
                    break;
                default: throw new Exception($"Invalid DataSet identifier: {datasetName}");
            }

            if (this.dataStructures.ContainsKey(datasetName))
                return this.dataStructures[datasetName];

            return null;
        }
    }
}
