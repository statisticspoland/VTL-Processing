namespace StatisticsPoland.VtlProcessing.Core.DataModelProviders.Models
{
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;
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
        /// <param name="defaultNamespace">Default namespace name.</param>
        /// <param name="dataStructures">Dictionary of structures.</param>
        public DataModelRegular(string defaultNamespace, Dictionary<string, IDataStructure> dataStructures)
        {
            this.DefaultNamespace = defaultNamespace;
            this.dataStructures = dataStructures;
        }

        public string DefaultNamespace { get; }

        public IDataStructure GetDatasetStructure(string datasetName)
        {
            if (this.dataStructures.ContainsKey(datasetName))
                return this.dataStructures[datasetName];
            else if (this.dataStructures.ContainsKey($@"{this.DefaultNamespace}\{datasetName}"))
                return this.dataStructures[$@"{this.DefaultNamespace}\{datasetName}"];
            return null;
        }
    }
}
