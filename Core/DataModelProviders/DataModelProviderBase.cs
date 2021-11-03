namespace StatisticsPoland.VtlProcessing.Core.DataModelProviders
{
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;
    using System;

    /// <summary>
    /// The base for data model representations.
    /// </summary>
    public abstract class DataModelProviderBase : IDataModelProvider
    {
        private readonly IDataModelProvider _rootModel;

        /// <summary>
        /// Initializes a new instance of the <see cref="DataModelProviderBase"/> class.
        /// </summary>
        /// <param name="rootModel">The root data model.</param>
        public DataModelProviderBase(IDataModelProvider rootModel)
        {
            this._rootModel = rootModel;
        }

        public string DefaultNamespace => this._rootModel.DefaultNamespace;

        /// <summary>
        /// Gets or sets the namespace name.
        /// </summary>
        public string Namespace { get; set; }

        public virtual IDataStructure GetDatasetStructure(string datasetName)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Splits a full dataset name to a namespace name and a dataset name.
        /// </summary>
        /// <param name="fullDatasetName">The full dataset name.</param>
        /// <param name="namespaceName">The namespace name.</param>
        /// <param name="datasetName">The dataset name.</param>
        protected void SplitDatasetName(string fullDatasetName, out string namespaceName, out string datasetName)
        {
            string[] split = fullDatasetName.Split(@"\");
            switch (split.Length)
            {
                case 1:
                    namespaceName = this.DefaultNamespace;
                    datasetName = split[0];
                    break;
                case 2:
                    namespaceName = split[0];
                    datasetName = split[1];
                    break;
                default: throw new ArgumentOutOfRangeException("datasetName", $"Invalid DataSet identifier: {fullDatasetName}");
            }
        }
    }
}
